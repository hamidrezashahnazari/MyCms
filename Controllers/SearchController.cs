using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;

namespace MyCms.Controllers
{
    [MinifyHtml]
    public class SearchController : Controller
    {
        private MyCmsContex db = new MyCmsContex();
        INewsRepository NewsRepo;
        public SearchController()
        {
            NewsRepo = new NewsRepository(db);
        }
        public ActionResult Index(string q)
        {
            ViewBag.search = q;
            return View(NewsRepo.SearchNews(q));
        }
    }
}