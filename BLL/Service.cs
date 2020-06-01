using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDashboardService
    {
        Users CheckUser(string empno);
        IEnumerable<GroupItem> GetToolBarByNo(string empno);
    }

    public class DashboardService : IDashboardService
    {
        private IUsersRepository _user;
        private IUsersGroupRelRepository _usergroup;
        private IGroupRepository _group;
        private IItemGroupRelRepository _itemgrouprel;
        private IItemRepository _itemrepo;

        public DashboardService(
            IItemRepository item,
            IUsersRepository user,
            IUsersGroupRelRepository usergroup,
            IGroupRepository group,
            IItemGroupRelRepository itemrel)
        {
            _user = user;
            _usergroup = usergroup;
            _group = group;
            _itemgrouprel = itemrel;
            _itemrepo = item;
        }

        public Users CheckUser(string empno)
        {

            var user = _user.Get(empno);
            return user;
        }

        private IEnumerable<Group> GetUserGroups(string empno)
        {
            var id_user = _user.Get(empno).id_user;

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


    }

    public class GroupItem
    {
        public String groupname { get; set; }

        public IList<Item> items { get; set; }
    }


}
