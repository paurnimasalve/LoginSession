using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginSession.Models;

namespace LoginSession.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(User user)
        {
            using (LoginDetailEntities db = new LoginDetailEntities())
            {
                var userDetails = db.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    user.LoginErrorMessage = "Invalid values added.";
                    return View("Index", user);
                }
                else
                {
                    Session["UserID"] = user.UserID;
                    Session["UserName"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Logout(User user)
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}