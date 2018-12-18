using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZophopWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["sessUser"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string txtUsername, string txtPassword)
        {
            if(string.IsNullOrEmpty(txtUsername) || string.IsNullOrEmpty(txtPassword))
            {
                ViewBag.Message = "Please enter Username and Password!";
            }
            else if(!txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["LOGIN_USERNAME"]) &&
                    !txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["ADMIN_USERNAME"]) &&
                    !txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["ZOPHOP_USERNAME"]))
            {
                ViewBag.Message = "Invalid Username or Password!";
            }
            else if (txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["LOGIN_USERNAME"]) &&
                    !txtPassword.Equals(System.Configuration.ConfigurationManager.AppSettings["LOGIN_PASSWORD"]))
            {
                ViewBag.Message = "Invalid Username or Password!";
            }
            else if (txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["ADMIN_USERNAME"]) &&
                    !txtPassword.Equals(System.Configuration.ConfigurationManager.AppSettings["ADMIN_PASSWORD"]))
            {
                ViewBag.Message = "Invalid Username or Password!";
            }
            else if (txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["ZOPHOP_USERNAME"]) &&
                !txtPassword.Equals(System.Configuration.ConfigurationManager.AppSettings["ZOPHOP_PASSWORD"]))
            {
                ViewBag.Message = "Invalid Username or Password!";
            }
            else if (txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["LOGIN_USERNAME"]) &&
                    txtPassword.Equals(System.Configuration.ConfigurationManager.AppSettings["LOGIN_PASSWORD"]))
            {
                Session["sessUser"] = "user";
                //return RedirectToAction("Index", "Devices");
                return RedirectToAction("Index", "Dashboard");
            }
            else if (txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["ADMIN_USERNAME"]) &&
                    txtPassword.Equals(System.Configuration.ConfigurationManager.AppSettings["ADMIN_PASSWORD"]))
            {
                Session["sessUser"] = "admin";
                //return RedirectToAction("Index", "Devices");
                return RedirectToAction("Index", "Dashboard");
            }
            else if (txtUsername.Equals(System.Configuration.ConfigurationManager.AppSettings["ZOPHOP_USERNAME"]) &&
                    txtPassword.Equals(System.Configuration.ConfigurationManager.AppSettings["ZOPHOP_PASSWORD"]))
            {
                Session["sessUser"] = "zophop";
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session["sessUser"] = null;
            return RedirectToAction("Index");
        }
    }
}