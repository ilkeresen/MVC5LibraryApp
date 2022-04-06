using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    [Authorize(Roles = "A")]
    public class LoanController : Controller
    {
        // GET: Loan
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var movement = Context.TBLMovement.Where(x => x.MovementStatus == false).ToList();
            return View(movement);
        }

        [HttpGet]
        public ActionResult LoanToGive()
        {
            List<SelectListItem> User = (from i in Context.TBLUser.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.UserName + " " + i.UserSurname,
                                             Value = i.UserID.ToString()
                                         }).ToList();
            List<SelectListItem> Book = (from i in Context.TBLBook.Where(x=>x.BookStatus == true).ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.BookName,
                                             Value = i.BookID.ToString()
                                         }).ToList();
            List<SelectListItem> Employee = (from i in Context.TBLEmployee.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.EmployeeName + " " + i.EmployeeSurname,
                                                 Value = i.EmployeeID.ToString()
                                             }).ToList();

            ViewBag.User = User;
            ViewBag.Book = Book;
            ViewBag.Employee = Employee;

            return View();
        }

        [HttpPost]
        public ActionResult LoanToGive(TBLMovement tBLMovement)
        {
            //tBLMovement.MovementStatus = true;
            Context.TBLMovement.Add(tBLMovement);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult LoanReturnIt(int id)
        {
            var Loan = Context.TBLMovement.Find(id);
            DateTime ReturnDate = DateTime.Parse(Loan.MovementReturnDate.ToString());
            DateTime NowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan Difference = NowDate - ReturnDate;
            ViewBag.ReturnDate = Difference.TotalDays;
            return View("LoanReturnIt", Loan);
        }

        [HttpPost]
        public ActionResult LoanReturnIt(TBLMovement tBLMovement)
        {
            var Movement = Context.TBLMovement.Find(tBLMovement.MovementID);
            Movement.MovementUserReturnDate = tBLMovement.MovementUserReturnDate;
            Movement.MovementStatus = true;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}