using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public interface IExtentRepository
    {
        IEnumerable<zp_get_not_exists_group_Result> GetHiddenClass(string empno);
    }

    public interface IUsersRepository : IRepository<Users>
    {

    }

    public interface IUsersGroupRelRepository : IRepository<UsersGroupsRel>
    {

    }

    public interface ITabTemplateRepository : IRepository<TabTemplate>
    {

    }

    public interface IGroupRepository : IRepository<Group>
    {
      
    }

    public interface IItemGroupRelRepository: IRepository<ItemGroupRel>
    {

    }

    public interface IItemRepository : IRepository<Item>
    {
        int InsertReturnId(Item t);
    }


}
