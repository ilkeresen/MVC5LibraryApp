using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Library.Models;

namespace MVC5Library.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        DbLibraryEntities Context = new DbLibraryEntities();
        public ActionResult Index()
        {
            var UserMail = (string)Session["UserMail"];

            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();
            TempData["UserNameSurname"] = Users.UserName + " " + Users.UserSurname;
            TempData["UserPhoto"] = Users.UserPhoto;

            var Message = Context.TBLMessage.Where(x => x.MessageBuyer == UserMail && x.MessageStatus == true).OrderByDescending(x => x.MessageID).ToList();

            return View(Message);
        }

        public PartialViewResult Folders()
        {
            var UserMail = (string)Session["UserMail"];

            var BuyerMessage = Context.TBLMessage.Count(x=>x.MessageBuyer == UserMail && x.MessageStatus == true).ToString();
            ViewBag.BuyerMessage = BuyerMessage;
            var SenderMessage = Context.TBLMessage.Count(x=>x.MessageSender == UserMail && x.MessageStatus == true).ToString();
            ViewBag.SenderMessage = SenderMessage;
            var TrashMessage = Context.TBLMessage.Count(x=>x.MessageSender == UserMail && x.MessageStatus == false || x.MessageBuyer == UserMail && x.MessageStatus == false).ToString();
            ViewBag.TrashMessage = TrashMessage;

            return PartialView();
        }

        public PartialViewResult Labels()
        {
            var UserMail = (string)Session["UserMail"];

            var ImportantMessage = Context.TBLMessage.Count(x=>x.MessageBuyer == UserMail && x.MessageLabel == "Important" && x.MessageStatus == true || x.MessageSender == UserMail && x.MessageLabel == "Important" && x.MessageStatus == true).ToString();
            ViewBag.ImportantMessage = ImportantMessage;
            var PromotionsMessage = Context.TBLMessage.Count(x=>x.MessageBuyer == UserMail && x.MessageLabel == "Promotions" && x.MessageStatus == true || x.MessageSender == UserMail && x.MessageLabel == "Promotions" && x.MessageStatus == true).ToString();
            ViewBag.PromotionsMessage = PromotionsMessage;
            var SocialMessage = Context.TBLMessage.Count(x=>x.MessageBuyer == UserMail && x.MessageLabel == "Social" && x.MessageStatus == true || x.MessageSender == UserMail && x.MessageLabel == "Social" && x.MessageStatus == true).ToString();
            ViewBag.SocialMessage = SocialMessage;
            var NoMessage = Context.TBLMessage.Count(x => x.MessageBuyer == UserMail && x.MessageLabel == null && x.MessageStatus == true || x.MessageSender == UserMail && x.MessageLabel == null && x.MessageStatus == true).ToString();
            ViewBag.NeMessage = NoMessage;

            return PartialView();
        }

        public ActionResult MessageSender()
        {
            var UserMail = (string)Session["UserMail"];

            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();
            TempData["UserNameSurname"] = Users.UserName + " " + Users.UserSurname;
            TempData["UserPhoto"] = Users.UserPhoto;

            var Message = Context.TBLMessage.Where(x => x.MessageSender == UserMail && x.MessageStatus == true).OrderByDescending(x => x.MessageID).ToList();
            return View(Message);
        }

        public ActionResult MessageDetail(int id)
        {
            var UserMail = (string)Session["UserMail"];

            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();
            TempData["UserNameSurname"] = Users.UserName + " " + Users.UserSurname;
            TempData["UserPhoto"] = Users.UserPhoto;

            var MessageDetails = Context.TBLMessage.Where(x=>x.MessageID == id).ToList();
            return View(MessageDetails);
        }

        public ActionResult MessageTrashList()
        {
            var UserMail = (string)Session["UserMail"];

            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();
            TempData["UserNameSurname"] = Users.UserName + " " + Users.UserSurname;
            TempData["UserPhoto"] = Users.UserPhoto;

            var Message = Context.TBLMessage.Where(x => x.MessageSender == UserMail && x.MessageStatus == false || x.MessageBuyer == UserMail && x.MessageStatus == false).OrderByDescending(x => x.MessageID).ToList();
            return View(Message);
        }

        public ActionResult MessageTrash(int id)
        {
            var MessageTrash = Context.TBLMessage.Find(id);
            MessageTrash.MessageStatus = false;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MessageRemove(int id)
        {
            var MessageTrash = Context.TBLMessage.Find(id);
            Context.TBLMessage.Remove(MessageTrash);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MessageNew()
        {
            var UserMail = (string)Session["UserMail"];

            var Users = Context.TBLUser.Where(x => x.UserMail == UserMail).FirstOrDefault();
            TempData["UserNameSurname"] = Users.UserName + " " + Users.UserSurname;
            TempData["UserPhoto"] = Users.UserPhoto;
            return View();
        }

        [HttpPost]
        public ActionResult MessageNew(TBLMessage tBLMessage)
        {
            var UserMail = (string)Session["UserMail"];

            tBLMessage.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            tBLMessage.MessageSender = UserMail;
            tBLMessage.MessageStatus = true;
            Context.TBLMessage.Add(tBLMessage);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        //Etiketleri Yap
    }
}