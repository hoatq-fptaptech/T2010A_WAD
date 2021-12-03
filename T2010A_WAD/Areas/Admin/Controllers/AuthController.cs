using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2010A_WAD.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
namespace T2010A_WAD.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Admin/Auth
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var check = db.Admins.FirstOrDefault(s => s.Username.Equals(admin.Username));
                if(check == null)
                {
                    // convert pass thanh md5
                    admin.Password = GetMD5(admin.Password);
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(admin.Username, true);// xac nhận đã đăng nhập
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ViewBag.Error = "Email đã tồn tại!";
                }
            }
            return View();
        }

        private string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] frData = Encoding.UTF8.GetBytes(str);
            byte[] toData = md5.ComputeHash(frData);
            string hashString = "";
            foreach(var i in toData)
            {
                hashString += i.ToString("x2");
            }
            return hashString;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Password = GetMD5(admin.Password);
                var data = db.Admins.Where(s => s.Username.Equals(admin.Username) && s.Password.Equals(admin.Password)).FirstOrDefault();
                if(data != null)
                {
                    FormsAuthentication.SetAuthCookie(data.Username, true);// xac nhận đã đăng nhập
                    return RedirectToAction("Index", "Category");
                }
            }
            return View();
        }
        
    }
}