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
    [HandleError]
    [MinifyHtml]
    public class NewsController : Controller
    {
        private MyCmsContex db = new MyCmsContex();
        private ICategoryRepository CategoryRepo;
        private INewsRepository NewsRepo;
        private ICommentRepository CommentRepo;
        public NewsController()
        {
            CategoryRepo = new CategoryRepository(db);
            NewsRepo = new NewsRepository(db);
            CommentRepo = new CommentRepository(db);
        }

        // GET: News
        public ActionResult Index()
        {
            return PartialView();
        }

        [Route("News/ShowCategory")]
        public ActionResult ShowCategory()
        {
            return PartialView(CategoryRepo.GetCategoryForSidebar());
        }

        [Route("News/ShowCategoryInDropMenu")]
        public ActionResult ShowCategoryInDropMenu()
        {
            return PartialView(CategoryRepo.GetAllCategories());
        }
        public ActionResult ShowNewsTab()
        {
            return PartialView();
        }
        [Route("News/ShowMostViewedNews")]
        public ActionResult ShowMostViewedNews()
        {
            return PartialView(NewsRepo.GetMostVisitedNews(5));
        }
        public ActionResult ShowMostCommented()
        {
            return PartialView(NewsRepo.GetMostCommented(5));
        }
        //[Route("News/ShowLatestNews/")]
        //public ActionResult ShowLatestNews()
        //{
        //    return PartialView(NewsRepo.GetLatestNews());
        //}
        [Route("Archive")]
        public ActionResult ArchiveNews()
        {
            return View(NewsRepo.GetAllNews());
        }
        [Route("Category/{Id}/{Tittle}")]
        public ActionResult ShowNewsByCategoryId(int Id, string Tittle)
        {
            ViewBag.tittle = Tittle;
            return View(NewsRepo.GetNewsByCategoryId(Id));
        }
        [Route("News/{id}")]
        public ActionResult ShowNewsPage(int id)
        {
            var news = NewsRepo.GetNewsById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            news.Visit += 1;
            NewsRepo.UpdateNews(news);
            NewsRepo.Save();
            return View(news);
        }
        //    [Route("News/AddComment/{NewsId}")]
        public ActionResult AddComment(int id, string name, string email, string comment)
        {
            NewsComment CommentObj = new NewsComment()
            {
                NewsID = id,
                Name = name,
                Email = email,
                Comment = comment,
                SubmitDate = DateTime.Now
            };
            CommentRepo.AddComment(CommentObj);
            return PartialView("ShowComments", CommentRepo.GetCommentByNewsId(id));
        }
        //    [Route("ShowComments/{id}")]
        public ActionResult ShowComments(int id)
        {
            return PartialView(CommentRepo.GetCommentByNewsId(id));
        }

    }
}