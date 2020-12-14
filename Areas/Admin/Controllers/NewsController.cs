using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;

namespace MyCms.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private MyCmsContex db = new MyCmsContex();
        INewsRepository NewsRepo;
        ICategoryRepository CategoryRepo;
        public NewsController()
        {
            NewsRepo = new NewsRepository(db);
            CategoryRepo = new CategoryRepository(db);
        }
        // GET: Admin/News
        public ActionResult Index()
        {

            return View(NewsRepo.GetAllNews());
        }


        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(CategoryRepo.GetAllCategories(), "CategoryID", "CatTittle");
            return View();
        }

        // POST: Admin/News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,CategoryID,Tittle,ShortDescription,Text,AuthorName,Visit,Tags,ImageName,ShowInSlider,UploadDate")] News news, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                news.Visit = 0;
                news.UploadDate = DateTime.Now;
                if (imgUp != null)
                {
                    news.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/NewsImages/" + news.ImageName));
                }
                NewsRepo.InsertNews(news);
                NewsRepo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(CategoryRepo.GetAllCategories(), "CategoryID", "CatTittle", news.CategoryID);
            return View(news);
        }

        // GET: Admin/News/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = NewsRepo.GetNewsById(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(CategoryRepo.GetAllCategories(), "CategoryID", "CatTittle", news.CategoryID);
            return View(news);
        }

        // POST: Admin/News/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,CategoryID,Tittle,ShortDescription,Text,AuthorName,Visit,ImageName,ShowInSlider,UploadDate,Tags")] News news, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (news.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/NewsImages/" + news.ImageName));
                    }
                    news.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/NewsImages/" + news.ImageName));
                }

                NewsRepo.UpdateNews(news);
                NewsRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(CategoryRepo.GetAllCategories(), "CategoryID", "CatTittle", news.CategoryID);
            return View(news);
        }

        // GET: Admin/News/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = NewsRepo.GetNewsById(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = NewsRepo.GetNewsById(id);
            if (news.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/NewsImages/" + news.ImageName));
            }
            NewsRepo.DeleteNews(news);
            NewsRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CategoryRepo.Dispose();
                NewsRepo.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
