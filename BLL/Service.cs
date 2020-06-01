using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDashboardService {
        Users CheckUser(string empno);
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

            var user= _user.Get(empno);
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

        private void GetUserToolBar(IEnumerable<Group> groups)
        {
            //取得那些itemgroup
            var q = from k in groups
                    join j in _itemgrouprel.GetAll() on k.id_group equals j.id_group
                    select j;

            //取得那些item
            var item = from k in q
                       join j in _itemrepo.GetAll() on k.id_item equals j.id_item
                       select j;
        }



    }

    

}
