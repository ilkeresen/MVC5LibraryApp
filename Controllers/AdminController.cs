using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        // GET: Admin
        DbLibraryEntities Context = new DbLibraryEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBLAdmin tBLAdmin)
        {
            var Admin = Context.TBLAdmin.FirstOrDefault(x => x.AdminMail == tBLAdmin.AdminMail && x.AdminPassword == tBLAdmin.AdminPassword);
            if (Admin != null)
            {
                FormsAuthentication.SetAuthCookie(Admin.AdminMail, false);
                Session["AdminMail"] = Admin.AdminMail.ToString();
                Session["AdminPhoto"] = Admin.AdminPhoto;
                Session["AdminRol"] = Admin.AdminRol;
                if (Admin.AdminStatus.ToString() == "True")
                {
                    Session["AdminStatus"] = "Aktif";
                }
                else
                {
                    Session["AdminStatus"] = "Pasif";
                }

                return RedirectToAction("Index", "Dashboard");
            }
            TempData["Error"] = "E-Posta Adresi veya Şifre Hatalı.";
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}