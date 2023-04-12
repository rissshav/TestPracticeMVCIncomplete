using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestPracticeMVC.Models;

namespace TestPracticeMVC.Controllers
{
    public class AccountController : Controller
    {
        InventoryContext _credentials = new InventoryContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users model)
        {
            Users users = _credentials.Users.FirstOrDefault(x => x.Name == model.Name);

            if (users?.Password == model.Password)
            {
                if (users.Role == "admin")
                {
                    FormsAuthentication.SetAuthCookie(model.Name, false);
                    return RedirectToAction("Index", "Admin");
                }
                else if (users.Role == "salesman")
                {
                    FormsAuthentication.SetAuthCookie(model.Name, false);
                    return RedirectToAction("Index", "Salesman");
                }

            }

            ModelState.AddModelError("", "Invalid Username or Password");

            return View();
        }
        /*        public ActionResult Signup()
                {
                    return View();
                }
                [HttpPost]
                public ActionResult Signup(LoginCredentials model)
                {
                    if (ModelState.IsValid)
                    {
                        _credentials.Credentials.Add(model);
                        _credentials.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    return View(model);
                }*/
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}