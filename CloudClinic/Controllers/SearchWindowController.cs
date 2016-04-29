using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using CloudClinic.Models;

namespace CloudClinic.Controllers
{
    public class SearchWindowController : Controller
    {
        public ActionResult SearchWindow()
        {
            return PartialView();
        }

        public ActionResult Autocomplete(string term)
        {
            Searcher searcher = new Searcher();
            var searchResults = searcher.GetSearchResults(term);
            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSearchResults([DataSourceRequest]DataSourceRequest request, string term)
        {
            Searcher searcher = new Searcher();
            IQueryable<Pasien> searchResults = searcher.GetSearchResults(term);
            DataSourceResult result = searchResults.ToDataSourceResult(request);
            return Json(result);
        }
    }
}