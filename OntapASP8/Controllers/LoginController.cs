using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OntapASP8.Models;

namespace OntapASP8.Controllers
{
    public class LoginController : Controller
    {
        private QLNV db = new QLNV();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.tblUsers.Where(u => u.username == username && u.password == password).FirstOrDefault();
            if(user == null) {
                ViewBag.errLogin = "Sai ten dang nhap hoac mat khau";
                return View("Login");
            }
            else
            {
                Session["username"] = username;
                Session["IsLogin"] = true;
                return RedirectToAction("Index", "NhanViens");
            }
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["IsLogin"] = null;
            return RedirectToAction("Index", "NhanViens");
        }
    }
}