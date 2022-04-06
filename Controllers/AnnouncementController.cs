using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var Announcement = Context.TBLAnnouncement.Where(x=>x.AnnouncementStatus == true).ToList();
            return View(Announcement);
        }

        [HttpGet]
        public ActionResult AnnouncementAdd()
        {
            List<SelectListItem> Category = (from i in Context.TBLAnnouncement.Take(4).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AnnouncementCategory,
                                                 Value = i.AnnouncementCategory
                                             }).ToList();
            ViewBag.Category = Category;
            return View();
        }

        [HttpPost]
        public ActionResult AnnouncementAdd(TBLAnnouncement tBLAnnouncement)
        {
            tBLAnnouncement.AnnouncementStatus = true;
            Context.TBLAnnouncement.Add(tBLAnnouncement);
            Context.SaveChanges();
            TempData["item"] = tBLAnnouncement.AnnouncementCategory;
            TempData["icon"] = "fa-check";
            TempData["message"] = "DUYURU EKLENDİ.";
            TempData["alert"] = "dark";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AnnouncementUpdate(int id)
        {
            var Announcement = Context.TBLAnnouncement.Find(id);
            List<SelectListItem> Category = (from i in Context.TBLAnnouncement.Take(4).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AnnouncementCategory,
                                                 Value = i.AnnouncementCategory
                                             }).ToList();
            ViewBag.Category = Category;
            return View(Announcement);
        }

        [HttpPost]
        public ActionResult AnnouncementUpdate(TBLAnnouncement tBLAnnouncement)
        {
            var Announcement = Context.TBLAnnouncement.Find(tBLAnnouncement.AnnouncementID);
            Announcement.AnnouncementCategory = tBLAnnouncement.AnnouncementCategory;
            Announcement.AnnouncementContent = tBLAnnouncement.AnnouncementContent;
            Announcement.AnnouncementDate = tBLAnnouncement.AnnouncementDate;
            Announcement.AnnouncementStatus = true;
            Context.SaveChanges();
            TempData["item"] = tBLAnnouncement.AnnouncementCategory;
            TempData["icon"] = "fa-edit";
            TempData["message"] = "DUYURU GÜNCELLENDİ.";
            TempData["alert"] = "info";
            return RedirectToAction("Index");
        }

        public ActionResult AnnouncementDelete(int id)
        {
            var Announcement = Context.TBLAnnouncement.Find(id);
            Announcement.AnnouncementStatus = false;
            Context.SaveChanges();
            TempData["item"] = Announcement.AnnouncementCategory;
            TempData["icon"] = "fa-trash-alt";
            TempData["message"] = "DUYURU SİLİNDİ.";
            TempData["alert"] = "danger";
            return RedirectToAction("Index");
        }

        public ActionResult AnnouncementDetail(int id)
        {
            List<SelectListItem> Category = (from i in Context.TBLAnnouncement.Take(4).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AnnouncementCategory,
                                                 Value = i.AnnouncementCategory
                                             }).ToList();
            ViewBag.Category = Category;
            //var Announcement = Context.TBLAnnouncement.Where(x => x.AnnouncementID == id).ToList();
            var Announcement = Context.TBLAnnouncement.Find(id);
            return View(Announcement);
        }
    }
}