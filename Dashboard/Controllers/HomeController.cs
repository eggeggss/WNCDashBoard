using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private IDashboardService _dashboard;
        public HomeController(IDashboardService dashboard)
        {
           // _dashboard = dashboard;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
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