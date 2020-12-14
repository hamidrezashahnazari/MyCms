using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;

namespace MyCms.Controllers
{
    [HandleError]
    [MinifyHtml]

    public class HomeController : Controller
    {
        private MyCmsContex db = new MyCmsContex();
        private INewsRepository NewsRepo;
        public HomeController()
        {
            NewsRepo = new NewsRepository(db);
        }
        public ActionResult Index(int pageId = 1)
        {
            int skip = (pageId - 1) * 3;
            int count = NewsRepo.GetAllNews().Count();
            ViewBag.NewsId = pageId;
            //if (count % 2 == 0)
            //{
            //    count++;
            //}
            ViewBag.Count = count / 3;
            var NewsList = db.News.OrderBy(n => n.UploadDate).Skip(skip).Take(3).ToList();
            return View(NewsList);
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
        public ActionResult ShowInSlider()
        {
            return PartialView(NewsRepo.NewsInSlider());
        }

    }
}