using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    //[HandleError]
    public class CategoriesController : Controller
    {
        ICategoryRepository CategoryRepo;
        MyCmsContex db = new MyCmsContex();
        public CategoriesController()
        {
            CategoryRepo = new CategoryRepository(db);
        }

        // GET: Admin/Categories
        public ActionResult Index()
        {

            return View(CategoryRepo.GetAllCategories());
        }

        // GET: Admin/Categories/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cat = CategoryRepo.GetCategoryById(id.Value);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        // GET: Admin/Categories/Create
        //[Route("Admin/Categories/Create")]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CatTittle")] Category category)
        {
            if (ModelState.IsValid)
            {

                CategoryRepo.InsertCategory(category);
                CategoryRepo.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cat = CategoryRepo.GetCategoryById(id.Value);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return PartialView(cat);
        }

        // POST: Admin/Categories/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CatTittle")] Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepo.UpdateCategory(category);
                CategoryRepo.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cat = CategoryRepo.GetCategoryById(id.Value);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return PartialView(cat);
        }

        // POST: Admin/Categories/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cat = CategoryRepo.GetCategoryById(id);
            CategoryRepo.DeleteCategory(cat);
            CategoryRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CategoryRepo.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
