using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    [Authorize(Roles = "A")]
    public class CommentController : Controller
    {
        // GET: Comment
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var CommentList = Context.TBLComment.Where(x => x.CommentStatus == true).OrderByDescending(y=>y.CommentDate).ToList();
            return View(CommentList);
        }

        public ActionResult CommentDelete(int id)
        {
            var Comment = Context.TBLComment.Find(id);
            Comment.CommentStatus = false;
            Context.SaveChanges();
            TempData["item"] = Comment.CommentMail;
            TempData["icon"] = "fa-trash-alt";
            TempData["message"] = "YORUM SİLİNDİ.";
            TempData["alert"] = "danger";
            return RedirectToAction("Index");
        }

        public ActionResult CommentApprovalAdd(int id)
        {
            var Comment = Context.TBLComment.Find(id);
            Comment.CommentApproval = true;
            Context.SaveChanges();
            TempData["item"] = Comment.CommentMail;
            TempData["icon"] = "fa-check";
            TempData["message"] = "YORUM VİTRİNE EKLENDİ.";
            TempData["alert"] = "dark";
            return RedirectToAction("Index");
        }

        public ActionResult CommentApprovalDelete(int id)
        {
            var Comment = Context.TBLComment.Find(id);
            Comment.CommentApproval = false;
            Context.SaveChanges();
            TempData["item"] = Comment.CommentMail;
            TempData["icon"] = "fa-trash-alt";
            TempData["message"] = "YORUM VİTRİNDEN ÇIKARTILDI.";
            TempData["alert"] = "danger";
            return RedirectToAction("Index");
        }
    }
}