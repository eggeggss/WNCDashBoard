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
    [RoutePrefix("Admin")]
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


        [HttpGet]
        [Route("GetZoneItem/{id_item}")]
        public ActionResult GetZoneItem(int? id_item)
        {
            var item = _dashboard.GetReportInfo((int)id_item);

            List<SelectListItem> sizeList = new List<SelectListItem>();

            sizeList.Add(new SelectListItem { Text = "4x4", Value = "0" });
            sizeList.Add(new SelectListItem { Text = "2x2", Value = "1" });
            sizeList.Add(new SelectListItem { Text = "2x4", Value = "2" });
            ViewBag.sizeList = sizeList;

            return PartialView("_GetZoneItem", item);
        }

        [HttpPost]
        public ActionResult UpdateZone([System.Web.Http.FromBody] Item item)
        {
            try
            {


                return RedirectToAction("Index");
                //_bll.UpdateWhoTrans(data.empno, data.orders);

                //return new HttpStatusCodeResult(200);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400);
            }

        }

    }
}