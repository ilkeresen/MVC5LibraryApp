using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using MVC5Library.Models;
using MVC5Library.Models.ClassIEnumerable;

namespace MVC5Library.Controllers
{
    [AllowAnonymous]
    public class ShowcaseController : Controller
    {
        // GET: Showcase
        DbLibraryEntities Context = new DbLibraryEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var UsersCount = Context.TBLUser.Where(x => x.UserStatus == true).Count();
            var AuthorCount = Context.TBLAuthor.Where(x => x.AuthorStatus == true).Count();
            var BookCount = Context.TBLBook.Where(x => x.BookStatus == true).Count();
            var CategoryCount = Context.TBLCategory.Where(x => x.CategoryStatus == true).Count();
            ViewBag.UsersCount = UsersCount;
            ViewBag.AuthorCount = AuthorCount;
            ViewBag.BookCount = BookCount;
            ViewBag.CategoryCount = CategoryCount;
            Showcase showcase = new Showcase();
            showcase.ShowCaseBook = Context.TBLBook.Where(x => x.BookStatus == true && x.BookPhoto != null).ToList();
            showcase.ShowCaseAbout = Context.TBLAbout.ToList();
            showcase.ShowCaseComment = Context.TBLComment.Where(x => x.CommentApproval == true).ToList();
            return View(showcase);
        }

        [HttpPost]
        public ActionResult Index(TBLContact tBLContact)
        {
            tBLContact.ContactStatus = true;
            tBLContact.ContactDate = DateTime.Today;
            Context.TBLContact.Add(tBLContact);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBLUser tBLUser)
        {
            var Users = Context.TBLUser.FirstOrDefault(x => x.UserMail == tBLUser.UserMail && x.UserPassword == tBLUser.UserPassword);
            if (Users != null)
            {
                FormsAuthentication.SetAuthCookie(Users.UserMail, false);
                Session["UserMail"] = Users.UserMail.ToString();
                return RedirectToAction("Index", "Studentpanel");
            }
            TempData["Error"] = "E-Posta Adresi veya Şifre Hatalı.";
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TBLUser tBLUser)
        {
            tBLUser.UserStatus = true;
            tBLUser.UserPhoto = "/AdminTema/dist/img/AdminLTELogo.png";
            Context.TBLUser.Add(tBLUser);
            Context.SaveChanges();
            if (tBLUser != null)
            {
                FormsAuthentication.SetAuthCookie(tBLUser.UserMail, false);
                Session["UserMail"] = tBLUser.UserMail.ToString();
                return RedirectToAction("Index", "Studentpanel");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(TBLUser tBLUser)
        {
            var UserMail = Context.TBLUser.Where(x => x.UserMail == tBLUser.UserMail).SingleOrDefault();

            if (UserMail != null)
            {
                Random random = new Random();
                int NewPassword = random.Next(1,9999);
                UserMail.UserPassword = NewPassword.ToString();
                Context.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "mailadresiniz@gmail.com";
                WebMail.Password = "mailsifreniz";
                WebMail.SmtpPort = 587;
                WebMail.Send(tBLUser.UserMail, "Şifremi Unuttum!",
                    " <!doctype html><html lang='en-US'><head><meta content='text/html; charset=utf-8' http-equiv='Content-Type' />" +
                    "<title>Şifremi Unuttum!</title><meta name='description' content='Reset Password Email Template.'><style type='text/css'>" +
                    " a:hover {text-decoration: underline !important;}</style></head><body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>" +
                    "<table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8' style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;'>" +
                    "<tr><td><table style='background-color: #f2f3f8; max-width:670px;  margin:0 auto;' width='100%' border='0'align='center' cellpadding='0' cellspacing='0'>" +
                    "<tr><td style='height:80px;'>&nbsp;</td> </tr><tr><td style='text-align:center;'><a href='' title='logo' target='_blank'><img width='60' src='https://i.hizliresim.com/atk77f2.png' title='logo' alt='logo'>" +
                    "</a></td></tr><tr><td style='height:20px;'>&nbsp;</td></tr><tr><td><table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'>" +
                    "<tr><td style='height:40px;'>&nbsp;</td></tr><tr><td style='padding:0 35px;'><h1 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>Şifrenizi Sıfırlamak İstediniz.</h1>" +
                    "<spanstyle='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></span><p style='color:#455056; font-size:15px;line-height:24px; margin:0;'>" +
                    "Sayın "+ UserMail.UserName + " " + UserMail.UserSurname +" şifreniz sıfırlandı. Yeni şifreniz aşağıda verilmiştir. Lütfen kimse ile paylaşmayınız. İstediğiniz zaman yeni şifreniz ile giriş yapıp şifrenizi güncelleyebilirsiniz." +
                    "</p><a href=''style='background:#20e277;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;'>" + NewPassword + "</a>" +
                    "</td></tr><tr><td style='height:40px;'>&nbsp;</td></tr></table></td><tr><td style='height:20px;'>&nbsp;</td></tr><tr><td style='text-align:center;'>" +
                    "<p style='font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;'>&copy; <strong>www.mvc5library.com</strong></p>" +
                    "</td></tr><tr><td style='height:80px;'>&nbsp;</td></tr></table></td></tr></table></body></html>"
                    );
                TempData["Success"] = "Yeni Şifreniz E-Posta Adresinize Gönderildi.";
                return View();
            }

            TempData["Error"] = "E-Posta Adresi Sistemde Kayıtlı Değil. Lütfen Tekrar Deneyiniz.";
            return View();
        }
    }
}
