using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var movement = Context.TBLMovement.Where(x => x.MovementStatus == true).ToList();
            return View(movement);
        }
    }
}