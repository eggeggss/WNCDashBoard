using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BLL
{
    public interface IDashboardService
    {
        Users CheckUser(string empno);
        IEnumerable<GroupItem> GetToolBarByNo(string empno);
        IEnumerable<zp_get_not_exists_group_Result> HideZone(string empno);
        Item GetReportInfo(int id_item);
        Group GetZoneGroup(int id_item);
        bool UpdateZone(Item item);
        void InsertZone(Item item);

        void DeleteZone(Item item);

        void SavePanel(PostPanelObj panelobj);
    }

    public class DashboardService : IDashboardService
    {
        private readonly IUsersRepository _user;
        private readonly IUsersGroupRelRepository _usergroup;
        private readonly IGroupRepository _group;
        private readonly IItemGroupRelRepository _itemgrouprel;
        private readonly IItemRepository _itemrepo;
        private readonly IExtentRepository _extent;
        private readonly ITabTemplateRepository _itab;
        public DashboardService(
            IItemRepository item,
            IUsersRepository user,
            IUsersGroupRelRepository usergroup,
            IGroupRepository group,
            IItemGroupRelRepository itemrel,
            ITabTemplateRepository itab,
            IExtentRepository extent)
        {
            _user = user;
            _usergroup = usergroup;
            _group = group;
            _itemgrouprel = itemrel;
            _itemrepo = item;
            _itab = itab;
            _extent = extent;
        }

        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        /// <param name="empno"></param>
        /// <returns></returns>
        public Users CheckUser(string empno)
        {
            var user = _user.Get(empno);
            return user;
        }

        /// <summary>
        /// 取得使用者群組
        /// </summary>
        /// <param name="empno"></param>
        /// <returns></returns>
        private IEnumerable<Group> GetUserGroups(string empno)
        {
            var users = _user.GetAll();
            var user = users.Where(e => e.empno.Equals(empno));
            var id_user = user.FirstOrDefault().id_user;
            var groupsRel = _usergroup
                .GetAll()
                .Where(e => e.id_user == id_user
                 && e.stat_void == 0).ToList();

            var groups = _group.GetAll();

            var userGroup = from k in groupsRel
                            join j in groups on k.id_group equals j.id_group
                            select j;

            return userGroup;
        }


        /// <summary>
        /// 取得群組的 ToolBar
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        private IEnumerable<GroupItem> GetToolBarByGroup(IEnumerable<Group> groups)
        {
            //取得那些itemgroup
            var q = from k in groups
                    join j in _itemgrouprel.GetAll() on k.id_group equals j.id_group
                    select new { id_item = j.id_item, id_group = j.id_group, group_name = k.group_name };

            var items = _itemrepo.GetAll();
            IList<GroupItem> groupitems = new List<GroupItem>();
            foreach (var data in q)
            {
                if (!groupitems.Any(e => e.groupname == data.group_name))
                {
                    groupitems.Add(new GroupItem
                    {
                        groupname = data.group_name,
                        items = new List<Item>() {
                            items.FirstOrDefault(e => e.id_item == data.id_item)
                        },
                    });
                }
                else
                {
                    var itemdata = groupitems.FirstOrDefault(e => e.groupname == data.group_name);

                    if (itemdata != null)
                    {
                        itemdata.items.Add(items.FirstOrDefault(e => e.id_item == data.id_item));
                    }

                }

            }

            foreach (var item in groupitems)
            {
                foreach (var data in item.items)
                {
                    if (data == null)
                        continue;
                    if (data.size == 0)
                    {
                        data.x = 4;
                        data.y = 4;
                    }
                    else if (data.size == 1)
                    {
                        data.x = 2;
                        data.y = 2;

                    }
                    else
                    {
                        data.x = 8;
                        data.y = 2;
                    }
                }
            }
            ////取得那些item
            //var item = from k in q
            //           join j in _itemrepo.GetAll() on k.id_item equals j.id_item
            //           select new GroupItem { groupname = k.group_name, item = j };

            return groupitems;
        }


        /// <summary>
        /// 根據群組取得使用者可以看到的Item
        /// </summary>
        /// <param name="empno"></param>
        /// <returns></returns>
        public IEnumerable<GroupItem> GetToolBarByNo(string empno)
        {
            var groups = GetUserGroups(empno);

            return GetToolBarByGroup(groups);
        }

        /// <summary>
        /// 當群組切換時隱藏不應該看到的zone
        /// </summary>
        /// <param name="empno"></param>
        /// <returns></returns>
        public IEnumerable<zp_get_not_exists_group_Result> HideZone(string empno)
        {
            return _extent.GetHiddenClass(empno);
        }

        /// <summary>
        /// 取得Zone的群組
        /// </summary>
        /// <param name="id_item"></param>
        /// <returns></returns>
        public Group GetZoneGroup(int id_item)
        {
            var groups = from q in _itemgrouprel.GetAll().Where(e => e.id_item == id_item && e.stat_void == 0)
                    join j in _group.GetAll() on q.id_group equals j.id_group
                    select j;

            return groups.FirstOrDefault();
        }


        /// <summary>
        /// 取得Zone資訊
        /// </summary>
        /// <param name="id_item"></param>
        /// <returns></returns>
        public Item GetReportInfo(int id_item)
        {
           return _itemrepo.GetAll().Where(e => e.id_item == id_item).FirstOrDefault();
        }


        /// <summary>
        /// 更新Zone
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateZone(Item item)
        {
           return _itemrepo.Update(item);
        }

        /// <summary>
        /// Insert Zone
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void InsertZone(Item item)
        {

            Item newitem = new Item
            {
                dt_create = DateTime.Now,
                icon_url = item.icon_url,
                report_url = item.report_url,
                item_name = item.item_name,
                id_group=item.id_group,
                size = item.size,
                stat_void = 0,
            };

            var group=_group.GetAll().Where(e => e.id_group == Convert.ToInt32(item.id_group) && e.stat_void == 0).FirstOrDefault(); ;


            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var id_item = _itemrepo.InsertReturnId(item);

                var rel = _itemgrouprel
                    .GetAll()
                    .Where(e => e.id_item == id_item
                    && e.id_group == group.id_group
                    && e.stat_void == 0).FirstOrDefault();

                if (rel != null)
                {
                    _itemgrouprel.Delete(rel);
                }

                _itemgrouprel.Insert(new ItemGroupRel { id_group = group.id_group, id_item = id_item, stat_void = 0 });

                scope.Complete();
            }     
        }

        public void DeleteZone(Item item)
        {
            _itemrepo.Delete(item);
        }

        public void SavePanel(PostPanelObj panelobj)
        {
            var tabitem = _itab.GetAll().Where(e => e.tab_name == panelobj.panelname && e.stat_void == 0).FirstOrDefault();
            var group=_group.GetAll().Where(e => e.group_name == panelobj.groupname && e.stat_void == 0).FirstOrDefault();
            //存在就更新不存在就新增
            if (tabitem == null)
            {
                _itab.Insert(new TabTemplate {
                    dt_create=DateTime.Now,
                    id_group=group.id_group,
                    htmltemplateD=panelobj.htmld,
                    htmltemplateM=panelobj.htmlm,
                    tab_name=panelobj.panelname,
                    stat_void=0
                });
            }else
            {
                tabitem.dt_update = DateTime.Now;
                tabitem.htmltemplateD = panelobj.htmld;
                tabitem.htmltemplateM = panelobj.htmlm;
                tabitem.tab_name = panelobj.panelname;
                _itab.Update(tabitem);
            }
        }
    }

    public class GroupItem
    {
        public String groupname { get; set; }

        public IList<Item> items { get; set; }
    }


}
