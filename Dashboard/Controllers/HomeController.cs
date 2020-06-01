using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
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