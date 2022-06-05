using PatientAppointmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientAppointmentSystem.Controllers
{
    public class AccountsController : Controller
    {
        DataModelEntities db = new DataModelEntities();
            public ActionResult Login()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult Login(User u)
        {
           bool IsValid = db.Users.Any(x=>x.UserName==u.UserName && x.Password==u.Password);
            if (IsValid)
            {
                Session["UserName"] = u.UserName.ToString();
                FormsAuthentication.SetAuthCookie(u.UserName,false);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalide UserName Or Password");
                return View();
            }
           
        } 
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
           
        }
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signup(User u)
        {
            var x = db.Users.Add(u);
            db.SaveChanges();

            return RedirectToAction("Login");
        } 
        
       
    }
}