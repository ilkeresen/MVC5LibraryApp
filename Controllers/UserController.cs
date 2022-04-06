using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;
using PagedList;
using PagedList.Mvc;

namespace MVC5Library.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var Users = Context.TBLUser.Where(x => x.UserStatus == true).ToList();
            return View(Users);
        }

        [HttpGet]
        public ActionResult UserAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserAdd(TBLUser tBLUser)
        {
            tBLUser.UserStatus = true;
            Context.TBLUser.Add(tBLUser);
            Context.SaveChanges();
            TempData["item"] = tBLUser.UserName + " " + tBLUser.UserSurname;
            TempData["icon"] = "fa-check";
            TempData["message"] = "ÜYE EKLENDİ.";
            TempData["alert"] = "dark";
            return RedirectToAction("Index");
        }

        public ActionResult UserDelete(int id)
        {
            var User = Context.TBLUser.Find(id);
            User.UserStatus = false;
            Context.SaveChanges();
            TempData["item"] = User.UserName + " " + User.UserSurname;
            TempData["icon"] = "fa-trash-alt";
            TempData["message"] = "ÜYE SİLİNDİ.";
            TempData["alert"] = "danger";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UserUpdate(int id)
        {
            var User = Context.TBLUser.Find(id);
            return View("UserUpdate", User);
        }

        [HttpPost]
        public ActionResult UserUpdate(TBLUser tBLUser)
        {
            var User = Context.TBLUser.Find(tBLUser.UserID);

            User.UserName = tBLUser.UserName;
            User.UserSurname = tBLUser.UserSurname;
            User.UserMail = tBLUser.UserMail;
            User.UserNickName = tBLUser.UserNickName;
            User.UserPassword = tBLUser.UserPassword;
            User.UserPhoto = tBLUser.UserPhoto;
            User.UserPhone = tBLUser.UserPhone;
            User.UserSchool = tBLUser.UserSchool;
            Context.SaveChanges();
            TempData["item"] = User.UserName + " " + User.UserSurname;
            TempData["icon"] = "fa-edit";
            TempData["message"] = "ÜYE GÜNCELLENDİ.";
            TempData["alert"] = "info";
            return RedirectToAction("Index");
        }

        public ActionResult UserDetail(int id)
        {
            var User = Context.TBLUser.Find(id);
            TempData["UserImage"] = User.UserPhoto;
            TempData["UserNameSurname"] = User.UserName + " " + User.UserSurname;
            TempData["UserNickName"] = User.UserNickName;
            TempData["UserSchool"] = User.UserSchool;
            TempData["UserPhone"] = User.UserPhone;
            TempData["UserMail"] = User.UserMail;
            return View();
        }

        public ActionResult UserList(int page = 1)
        {
            var Users = Context.TBLUser.ToList().ToPagedList(page, 6);
            return View(Users);
        }

        public ActionResult UserBookPast(int id)
        {
            var User = Context.TBLUser.Find(id);
            TempData["UserNameSurname"] = User.UserName + " " + User.UserSurname;
            var BookPast = Context.TBLMovement.Where(x => x.MovementUser == id).ToList();
            return View(BookPast);
        }
    }
}