using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class ContactController : Controller
    {
        DbLibraryEntities Context = new DbLibraryEntities();
        // GET: Contact
        public ActionResult Index()
        {
            var Contact = Context.TBLContact.Where(x => x.ContactStatus == true).OrderByDescending(y => y.ContactDate).ToList();
            return View(Contact);
        }

        public PartialViewResult Folders()
        {
            var ContactCountMessage = Context.TBLContact.Where(x => x.ContactStatus == true).Count();
            ViewBag.ContactCountMessage = ContactCountMessage;
            var TrashCountMessage = Context.TBLContact.Where(x=>x.ContactStatus == false).Count();
            ViewBag.TrashCountMessage = TrashCountMessage;
            return PartialView();
        }

        public ActionResult ContactTrash(int id)
        {
            var Contact = Context.TBLContact.Find(id);
            Contact.ContactStatus = false;
            Context.SaveChanges();
            TempData["item"] = Contact.ContactMail;
            TempData["icon"] = "fa-trash-alt";
            TempData["message"] = "MESAJ ÇÖP KUTUSUNA TAŞINDI.";
            TempData["alert"] = "danger";
            return RedirectToAction("Index","Contact");
        }
        
        public ActionResult ContactDelete(int id)
        {
            var Contact = Context.TBLContact.Find(id);
            Context.TBLContact.Remove(Contact);
            Context.SaveChanges();
            TempData["item"] = Contact.ContactMail;
            TempData["icon"] = "fa-trash-alt";
            TempData["message"] = "MESAJ SİLİNDİ.";
            TempData["alert"] = "danger";
            return RedirectToAction("Index","Contact");
        }

        public ActionResult ContactDetail(int id)
        {
            var Contact = Context.TBLContact.Where(x=>x.ContactID == id).ToList();
            return View(Contact);
        }

        public ActionResult ContactTrashList()
        {
            var Contact = Context.TBLContact.Where(x => x.ContactStatus == false).OrderByDescending(y => y.ContactDate).ToList();
            return View(Contact);
        }
    }
}