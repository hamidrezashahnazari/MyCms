using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Web.Mvc;
using DataLayer.Repositories;

namespace MyCms.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private MyCmsContex db = new MyCmsContex();
        private IAdminRepository AdminRepo;
        public HomeController()
        {
            AdminRepo = new AdminRepository(db);
        }

        // GET: Admin/Home

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "AdminID,FullName,BirthDate,Email,Password,ConfirmPassword,Bio")] DataLayer.Admin Admin)
        {
            if (ModelState.IsValid)
            {
                DataLayer.Admin admin = new DataLayer.Admin()
                {
                    FullName = Admin.FullName,
                    BirthDate = Admin.BirthDate,
                    Email = Admin.Email,
                    Password = Admin.Password,
                    ConfirmPassword = Admin.ConfirmPassword,
                    Bio = Admin.Bio
                };
                AdminRepo.InsertAdmin(admin);
                AdminRepo.Save();
            }
            return View(Admin);
        }
    }
}
