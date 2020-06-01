using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IDashboardService { }
     
    public class DashboardService: IDashboardService
    {
        private IUsersRepository _user;
        public DashboardService(IUsersRepository user)
        {
            _user = user;
           var users=_user.GetAll();

        }
    }
}
