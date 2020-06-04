using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Dashboard.Controllers
{
    public class user
    {
        public string empno { get; set; }
        public string password { get; set; }
    }

    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private IDashboardService _dashboard;
        public HomeController(IDashboardService dashboard)
        {
            
            _dashboard = dashboard;

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult PostLogin(user user)
        {

            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            var empno = "20000390";
            var tools=_dashboard.GetToolBarByNo(empno);
            ViewBag.Name = _dashboard.CheckUser(empno).empname;
            ViewBag.Tools = tools;

            //var data=_dashboard.HideZone(empno);
            var hiddnes= _dashboard.HideZone(empno).Select(e=>e.group_name).ToArray();
            StringBuilder ssb = new StringBuilder();
            //gen css
            foreach(var  hiddenclass in hiddnes)
            {
                ssb.Append("."+ hiddenclass + "{ display:none !important; }");
            }
            ViewBag.HiddenStype = ssb.ToString();



            return View();
        }



        [HttpGet]
        [Route("GenReport/{id}")]
        public ActionResult GenReport(string id)
        {
            ViewBag.id = id;

            var replaceid=id.Replace("arp_", "");

            int id_item = 0;
            bool ok = Int32.TryParse(replaceid, out id_item);

            var item=_dashboard.GetReportInfo(id_item);

            if (item == null) {
                return new HttpStatusCodeResult(404,"The Object is Not Found");
            }

            return PartialView("_GenReport", item);

        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}