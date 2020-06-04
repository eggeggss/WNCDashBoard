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

            var group= _dashboard.GetZoneGroup((int)id_item);

            ViewBag.GroupName = group.group_name;

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

                var zone=_dashboard.GetReportInfo(item.id_item);

                zone.item_name = item.item_name;
                zone.icon_url = item.icon_url;
                zone.report_url = item.report_url;
                zone.size = item.size;
                zone.dt_update = DateTime.Now;
                zone.stat_void = 0;

                _dashboard.UpdateZone(zone);

                return RedirectToAction("Index");
               
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400);
            }

        }



        [HttpGet]
        [Route("GetAddZoneItem")]
        public ActionResult GetAddZoneItem()
        {
            List<SelectListItem> groupList = new List<SelectListItem>();

            groupList.Add(new SelectListItem { Text = "MFG", Value = "1" });
            groupList.Add(new SelectListItem { Text = "ACC", Value = "2" });
            groupList.Add(new SelectListItem { Text = "DIS", Value = "3" });
            ViewBag.groupList = groupList;

            List<SelectListItem> sizeList = new List<SelectListItem>();

            sizeList.Add(new SelectListItem { Text = "4x4", Value = "0" });
            sizeList.Add(new SelectListItem { Text = "2x2", Value = "1" });
            sizeList.Add(new SelectListItem { Text = "2x4", Value = "2" });
            ViewBag.sizeList = sizeList;
            
            Item item = new Item();

            return PartialView("_GetAddZoneItem", item);
        }

        [HttpPost]
        public ActionResult AddZone([System.Web.Http.FromBody] Item item)
        {
            _dashboard.InsertZone(item);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("DeleteItem/{id_item}")]
        public ActionResult DeleteItem(int? id_item)
        {
            var item=_dashboard.GetReportInfo((int)id_item);
            _dashboard.DeleteZone(item);
            return RedirectToAction("Index");
        }

    }
}