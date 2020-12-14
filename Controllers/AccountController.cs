using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMarkupMin.AspNet4.Mvc;
namespace MyCms.Controllers
{
    [MinifyHtml]
    public class AccountController : Controller
    {
        private MyCmsContex db = new MyCmsContex();
        private IAdminLoginRepository LoginRepo;
        public AccountController()
        {
            LoginRepo = new LoginRepository(db);
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                if (LoginRepo.isExist(login.UserName, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("Username", "کاربری یافت نشد");
                }
            }

            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}