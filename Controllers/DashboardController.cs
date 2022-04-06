using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var ContactCountMessage = Context.TBLContact.Where(x => x.ContactStatus == true).Count();
            Session["ContactCount"] = ContactCountMessage;
            var movement = Context.TBLMovement.Where(x => x.MovementStatus == false).Count();
            Session["MovementLoanCount"] = movement;
            var CommentCount = Context.TBLComment.Where(x => x.CommentStatus == true).Count();
            Session["CommentCount"] = CommentCount;
            ///////////////////////////////////LİNQ CARDS/////////////////////////////////////////////
            //Users
            var Users = Context.TBLUser.Count();
            ViewBag.Users = Users;
            //OrderByDescending Take 8 UsersList
            var UsersList = Context.TBLUser.OrderByDescending(x => x.UserID).Take(8).ToList();
            ViewBag.UsersList = UsersList;
            //Books
            var Books = Context.TBLBook.Count();
            ViewBag.Books = Books;
            //Loans
            var Loans = Context.TBLBook.Where(x => x.BookStatus == false).Count();
            ViewBag.Loans = Loans;
            //Money
            var Money = Context.TBLCriminal.Sum(x => x.CriminalMoney);
            ViewBag.Money = Money;
            //Categorys
            var Categorys = Context.TBLCategory.Where(x => x.CategoryStatus == true).Count();
            ViewBag.Categorys = Categorys;
            //Messages
            var Messages = Context.TBLContact.Count();
            ViewBag.Messages = Messages;
            //TopCountBookAuthor
            var TopCountBookAuthor = Context.TopCountBookAuthor().FirstOrDefault();
            ViewBag.TopCountBookAuthor = TopCountBookAuthor;
            //BookPublisher
            var BookPublisher = Context.TBLBook.GroupBy(x => x.BookPublisher)
                .OrderByDescending(y => y.Count())
                .Select(z => z.Key)
                .FirstOrDefault();
            ViewBag.BookPublisher = BookPublisher;
            //OnlineUser
            var OnlineUser = Context.TopOnlineUser().FirstOrDefault();
            ViewBag.OnlineUser = OnlineUser;
            //BestPerson
            var BestPerson = Context.TopBestPerson().FirstOrDefault();
            ViewBag.BestPerson = BestPerson;
            //ReadBook
            var ReadBook = Context.TopReadBook().FirstOrDefault();
            ViewBag.ReadBook = ReadBook;
            //ThisDayLoan
            var ThisDayLoan = Context.TBLMovement
                .Where(x => x.MovementPurchaseDate == DateTime.Today && x.MovementStatus == false)
                .Count();
            ViewBag.ThisDayLoan = ThisDayLoan;
            //AllDayLoan
            var AllLoan = Context.TBLMovement
                .Where(x => x.MovementStatus == false)
                .Count();
            ViewBag.AllLoan = AllLoan;
            ///////////////////////////////////LİNQ CARDS/////////////////////////////////////////////

            //Employee -> booksales
            var Employee = (from x in Context.TBLMovement
                            group x by x.TBLEmployee.EmployeeName + " " + x.TBLEmployee.EmployeeSurname into g
                            select new EmployeeSales
                            {
                                Name = g.Key,
                                Count = g.Count()
                            }).OrderByDescending(y => y.Count).ToList();

            ViewBag.EmployeeBooks = Employee;

            List<string> EmployeeName = new List<string>();
            List<string> EmployeeSalesCount = new List<string>();

            foreach (var item in Employee)
            {
                EmployeeName.Add(item.Name);
                EmployeeSalesCount.Add(item.Count.ToString());
            }

            ViewBag.EmployeeName = EmployeeName;
            ViewBag.EmployeeNamee = EmployeeName[0];
            ViewBag.EmployeeSalesCount = EmployeeSalesCount;
            ViewBag.EmployeeSalesCountt = EmployeeSalesCount[0];

            //Users -> booksales
            var UsersBook = (from x in Context.TBLMovement
                             group x by x.TBLUser.UserName + " " + x.TBLUser.UserSurname into g
                             select new EmployeeSales
                             {
                                 Name = g.Key,
                                 Count = g.Count()
                             }).OrderByDescending(y => y.Count).ToList();

            ViewBag.UsersBooks = UsersBook;

            List<string> UsersBookName = new List<string>();
            List<string> UsersBookSalesCount = new List<string>();

            foreach (var item in UsersBook)
            {
                UsersBookName.Add(item.Name);
                UsersBookSalesCount.Add(item.Count.ToString());
            }

            ViewBag.UsersBookName = UsersBookName;
            ViewBag.UsersBookNamee = UsersBookName[0];
            ViewBag.UsersBookSalesCount = UsersBookSalesCount;
            ViewBag.UsersBookSalesCountt = UsersBookSalesCount[0];

            return View();
        }

        public PartialViewResult TodoList()
        {
            var AdminMail = (string)Session["AdminMail"];
            List<TBLTodo> tBLTodo = Context.TBLTodo.Where(x => x.TodoStatus == true && x.TodoMail == AdminMail).OrderByDescending(y=>y.TodoDate).ToList();
            return PartialView(tBLTodo);
        }

        [HttpPost]
        public JsonResult TodoDelete(int id)
        {
            TBLTodo tBLTodo = Context.TBLTodo.Where(x => x.TodoID == id).SingleOrDefault();

            if (tBLTodo != null)
            {
                tBLTodo.TodoStatus = false;
                Context.SaveChanges();
            }

            return Json(tBLTodo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TodoGetItem(int id)
        {
            TBLTodo tBLTodo = Context.TBLTodo.Where(x => x.TodoID == id).FirstOrDefault();
            TBLTodo NewtBLTodo = new TBLTodo();

            if (tBLTodo != null)
            {
                NewtBLTodo.TodoID = tBLTodo.TodoID;
                NewtBLTodo.TodoMail = tBLTodo.TodoMail;
                NewtBLTodo.TodoContent = tBLTodo.TodoContent;
                NewtBLTodo.TodoDate = tBLTodo.TodoDate;
            }

            return Json(NewtBLTodo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TodoUpdate(TBLTodo tBLTodo)
        {
            TBLTodo GettBLTodo = Context.TBLTodo.Find(tBLTodo.TodoID);

            if (GettBLTodo != null)
            {
                GettBLTodo.TodoID = tBLTodo.TodoID;
                GettBLTodo.TodoMail = tBLTodo.TodoMail;
                GettBLTodo.TodoContent = tBLTodo.TodoContent;
                GettBLTodo.TodoDate = tBLTodo.TodoDate;
                Context.SaveChanges();
            }

            return Json(GettBLTodo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TodoAdd(TBLTodo tBLTodo)
        {
            if (tBLTodo != null)
            {
                var AdminMail = (string)Session["AdminMail"];
                tBLTodo.TodoMail = AdminMail;
                tBLTodo.TodoStatus = true;
                tBLTodo.TodoApproval = true;
                Context.TBLTodo.Add(tBLTodo);
                Context.SaveChanges();
            }

            return Json(tBLTodo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TodoUpdateT(int id)
        {
            TBLTodo GettBLTodo = Context.TBLTodo.Find(id);
            if (GettBLTodo != null)
            {
                GettBLTodo.TodoApproval = true;
                Context.SaveChanges();
            }
            return Json(GettBLTodo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TodoUpdateF(int id)
        {
            TBLTodo GettBLTodo = Context.TBLTodo.Find(id);
            if (GettBLTodo != null)
            {
                GettBLTodo.TodoApproval = false;
                Context.SaveChanges();
            }
            return Json(GettBLTodo, JsonRequestBehavior.AllowGet);
        }
    }
}