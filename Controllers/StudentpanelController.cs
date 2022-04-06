using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    [Authorize]
    public class StudentpanelController : Controller
    {
        // GET: Studentpanel
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var UserMail = (string)Session["UserMail"];
            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();

            var UserID = Context.TBLUser.Where(x => x.UserMail == UserMail).Select(y => y.UserID).FirstOrDefault();
            var UserHowManyBook = Context.TBLMovement.Where(x => x.MovementUser == UserID).Count();

            var UserHowManyMessage = Context.TBLMessage.Where(x => x.MessageBuyer == UserMail && x.MessageStatus == true).Count();

            var AnnouncementCount = Context.TBLAnnouncement.Where(x => x.AnnouncementStatus == true).Count();

            var CommentStatus = Context.TBLComment.Where(x => x.CommentMail == UserMail).ToList().LastOrDefault();

            if (CommentStatus == null)
            {
                ViewBag.CommentStatus = false;
            }
            else
            {
                if (CommentStatus.CommentStatus == true)
                {
                    ViewBag.CommentStatus = true;
                }
                else if (CommentStatus.CommentStatus == false)
                {
                    ViewBag.CommentStatus = false;
                }
                else
                {
                    ViewBag.CommentStatus = false;
                }
            }

            TempData["UserHowManyBook"] = UserHowManyBook;
            TempData["UserHowManyMessage"] = UserHowManyMessage;
            TempData["AnnouncementCount"] = AnnouncementCount;
            TempData["UserNameSurname"] = Users.UserName + " " + Users.UserSurname;
            TempData["UserPhoto"] = Users.UserPhoto;
            TempData["UserMail"] = Users.UserMail;
            TempData["UserSchool"] = Users.UserSchool;
            TempData["UserPhone"] = Users.UserPhone;
            TempData["UserNickName"] = Users.UserNickName;
            return View();
        }

        public PartialViewResult Settings()
        {
            var UserMail = (string)Session["UserMail"];
            var UserID = Context.TBLUser.Where(x => x.UserMail == UserMail).Select(y => y.UserID).FirstOrDefault();
            var User = Context.TBLUser.Find(UserID);
            return PartialView("Settings", User);
        }

        [HttpPost]
        public ActionResult SettingsUpdate(TBLUser tBLUser)
        {
            var User = Context.TBLUser.Find(tBLUser.UserID);
            User.UserName = tBLUser.UserName;
            User.UserSurname = tBLUser.UserSurname;
            User.UserMail = tBLUser.UserMail;
            User.UserNickName = tBLUser.UserNickName;
            User.UserPassword = tBLUser.UserPassword;
            User.UserPhone = tBLUser.UserPhone;
            User.UserPhoto = tBLUser.UserPhoto;
            User.UserSchool = tBLUser.UserSchool;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Showcase");
        }

        public ActionResult MyBooks()
        {
            var UserMail = (string)Session["UserMail"];
            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();

            TempData["UserNameSurname"] = Users.UserName + " " + Users.UserSurname;
            TempData["UserPhoto"] = Users.UserPhoto;

            var UserID = Context.TBLUser.Where(x => x.UserMail == UserMail).Select(z => z.UserID).FirstOrDefault();
            var Books = Context.TBLMovement.Where(x => x.MovementUser == UserID).ToList();
            return View(Books);
        }

        public PartialViewResult AnnouncementList()
        {
            var AnnouncementList = Context.TBLAnnouncement.Where(y => y.AnnouncementStatus == true).OrderByDescending(x => x.AnnouncementDate).ToList();
            return PartialView("AnnouncementList", AnnouncementList);
        }

        public PartialViewResult MessageList()
        {
            var UserMail = (string)Session["UserMail"];
            var MessageList = Context.TBLMessage.Where(x => x.MessageBuyer == UserMail && x.MessageStatus == true).OrderByDescending(y => y.MessageDate).ToList();
            return PartialView("MessageList", MessageList);
        }

        public PartialViewResult Comment()
        {
            return PartialView("Comment");
        }

        [HttpPost]
        public ActionResult CommentAdd(TBLComment tBLComment)
        {
            var UserMail = (string)Session["UserMail"];
            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();

            tBLComment.CommentUserPhoto = Users.UserPhoto;
            tBLComment.CommentUserName = Users.UserName;
            tBLComment.CommentUserSurname = Users.UserSurname;
            tBLComment.CommentUserSchool = Users.UserSchool;
            tBLComment.CommentDate = DateTime.Today;
            tBLComment.CommentMail = UserMail;
            tBLComment.CommentApproval = false;
            tBLComment.CommentStatus = true;
            Context.TBLComment.Add(tBLComment);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}