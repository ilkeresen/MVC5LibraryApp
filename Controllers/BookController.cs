using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var tBLBooks = Context.TBLBook.ToList();
            return View(tBLBooks);
        }

        [HttpGet]
        public ActionResult BookAdd()
        {
            List<SelectListItem> Category = (from i in Context.TBLCategory.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryID.ToString()
                                             }).ToList();

            List<SelectListItem> Author = (from i in Context.TBLAuthor.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AuthorName + " " + i.AuthorSurname,
                                               Value = i.AuthorID.ToString()
                                           }).ToList();

            ViewBag.Category = Category;
            ViewBag.Author = Author;
            return View();
        }

        [HttpPost]
        public ActionResult BookAdd(TBLBook tBLBook)
        {
            var Category = Context.TBLCategory.Where(x => x.CategoryID == tBLBook.BookCategory).FirstOrDefault();
            var Author = Context.TBLAuthor.Where(y => y.AuthorID == tBLBook.BookAuthor).FirstOrDefault();
            tBLBook.TBLCategory = Category;
            tBLBook.TBLAuthor = Author;
            tBLBook.BookStatus = true;
            Context.TBLBook.Add(tBLBook);
            Context.SaveChanges();
            TempData["item"] = tBLBook.BookName;
            TempData["icon"] = "fa-check";
            TempData["message"] = "KİTAP EKLENDİ.";
            TempData["alert"] = "dark";
            return RedirectToAction("Index");
        }

        public ActionResult BookDelete(int id)
        {
            var Book = Context.TBLBook.Find(id);
            Context.TBLBook.Remove(Book);
            Context.SaveChanges();
            TempData["item"] = Book.BookName;
            TempData["icon"] = "fa-trash-alt";
            TempData["message"] = "KİTAP SİLİNDİ.";
            TempData["alert"] = "danger";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult BookUpdate(int id)
        {
            var Book = Context.TBLBook.Find(id);

            List<SelectListItem> Category = (from i in Context.TBLCategory.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryID.ToString()
                                             }).ToList();

            List<SelectListItem> Author = (from i in Context.TBLAuthor.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AuthorName + " " + i.AuthorSurname,
                                               Value = i.AuthorID.ToString()
                                           }).ToList();

            ViewBag.Category = Category;
            ViewBag.Author = Author;
            return View("BookUpdate", Book);
        }

        [HttpPost]
        public ActionResult BookUpdate(TBLBook tBLBook)
        {
            var Book = Context.TBLBook.Find(tBLBook.BookID);

            var Category = Context.TBLCategory.Where(x => x.CategoryID == tBLBook.BookCategory).FirstOrDefault();
            var Author = Context.TBLAuthor.Where(x => x.AuthorID == tBLBook.BookAuthor).FirstOrDefault();

            Book.BookName = tBLBook.BookName;
            Book.BookCategory = Category.CategoryID;
            Book.BookAuthor = Author.AuthorID;
            Book.BookPublicationYear = tBLBook.BookPublicationYear;
            Book.BookPublisher = tBLBook.BookPublisher;
            Book.BookPageNumber = tBLBook.BookPageNumber;
            Context.SaveChanges();
            TempData["item"] = tBLBook.BookName;
            TempData["icon"] = "fa-edit";
            TempData["message"] = "KİTAP GÜNCELLENDİ.";
            TempData["alert"] = "info";
            return RedirectToAction("Index");
        }
    }
}