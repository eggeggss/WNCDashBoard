using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class AdminController : Controller
    {
        private IDashboardService _dashboard;
        public AdminController(IDashboardService dashboard)
        {
            _dashboard = dashboard;
        }
        // GET: Admin
        public ActionResult Index()
        {
            var empno = "20000390";

            ViewBag.Name=_dashboard.CheckUser(empno).empname;
            var tools = _dashboard.GetToolBarByNo(empno);

            ViewBag.Tools = tools;

            //var data=_dashboard.HideZone(empno);
            var hiddnes = _dashboard.HideZone(empno).Select(e => e.group_name).ToArray();
            StringBuilder ssb = new StringBuilder();
            //gen css
            foreach (var hiddenclass in hiddnes)
            {
                ssb.Append("." + hiddenclass + "{ display:none !important; }");
            }
            ViewBag.HiddenStype = ssb.ToString();

            return View();
        }
    }
}