using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using CloudClinic.Models;
using Kendo.Mvc.Extensions;

namespace CloudClinic.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
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
            ViewBag.Message = "Kontak untuk halaman Web ini.";

            return View();
        }

        

        //public JsonResult Default(LookupFilter filter)
        //{
        //    return GetData(new PeopleLookup(), filter);
        //}

        //private JsonResult GetData(AbstractLookup lookup, LookupFilter filter)
        //{
        //    lookup.CurrentFilter = filter;

        //    return Json(lookup.GetData());
        //}
    }
}