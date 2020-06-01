using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
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

    public interface IItemRepositry: IRepository<Item>
    {

    }


}
