using Customizing_Authorization_Filter.Models;
using Customizing_Authorization_Filter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Customizing_Authorization_Filter.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Logindetails(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            LoginDetails loginDetails = new LoginDetails();
            UserModel user =loginDetails.GetUserDetails(model.UserName,model.Password);
            if (user.Roles != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                FormsAuthentication.SetAuthCookie(Convert.ToString(user.UserID), true);
                var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                //Based on the Role we can transfer the user to different page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
           
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginUser", "Login");
        }
    }
}