using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkSheet.Models.Entity;
using WorkSheet.Models.Context;
using WorkSheet.Services.Interface;
using WorkSheet.Services;

namespace WorkSheet.Controllers
{
    public class LoginController : Controller
    {
        private WorkContext db = new WorkContext();
        ILoginService loginService;
       
        // GET: /Login/Create
        public LoginController()
        {
            loginService = new LoginService();
        }

        public ActionResult Login()
        {
            Session["User"] = null;
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                User myUser = loginService.LoginUser(loginmodel);
                
                if (myUser != null)
                {
                    Session["User"] = myUser;
                    //Session["UserId"] = myUser.UserId;
                    return RedirectToAction("Index", "Task");
                    //return RedirectToAction("IndexHistory2", "Task");
                }
                else
                {
                    ViewBag.Message = "Wrong Email or Password";
                    //return View();
                }
            }

            return View(new LoginModel());
        }


        [ChildActionOnly] //Layout tan çlıştırınca hata veriyordu
        public ActionResult PartialMenu()
        {
            return PartialView(loginService.GetMenu());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
