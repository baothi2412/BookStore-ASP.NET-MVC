using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Work.App_Start;
using Work.Models;

namespace Work.Controllers
{
    public class UserController : Controller
    {
        private BookStore1Entities2 db = new BookStore1Entities2();

        // GET: login
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(user nguoidung)
        {
            try
            {
                user curentUser = new user();
                curentUser = db.users.Where(u => u.userName == nguoidung.userName).FirstOrDefault();
                if (curentUser == null)
                {
                    nguoidung.createAt = DateTime.Now;
                    nguoidung.updateAt = DateTime.Now;
                    nguoidung.role = "user";
                    nguoidung.status = true;
                    db.users.Add(nguoidung);
                    db.SaveChanges();
                    TempData["successMessage"] = "Đăng Ký Thành Công";
                    return RedirectToAction("Dangnhap");
                }
                return View("Dangky");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Dangnhap()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection userForm)
        {


            return View();

        }




        [HttpPost]
        public ActionResult Dangnhap(string email, string password)
        {
            using (var dbContext = new BookStore1Entities2())
            {
                // Use case-insensitive comparison for email
                user user = dbContext.users.FirstOrDefault(x => x.email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase) && x.password.Equals(password));

                if (user != null)
                {
                    Session["IdUsSS"] = user.userID.ToString();
                    Session["emailSS"] = user.email.ToString();

                    bool isAuthentic = user.email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase) && user.password.Equals(password);
                    
                    if (isAuthentic)
                    {
                        if (email.Equals("123@gmail.com", StringComparison.OrdinalIgnoreCase) && password.Equals("123"))
                        {
                            // Set user in session for authentication
                            SessionConfig.SetUser(user);

                            // Redirect to Admin area if the user is the default admin
                            return RedirectToAction("Index", "products", new { Area = "Admin" });
                        }
                        else
                        {
                            // Set user in session for authentication
                            SessionConfig.SetUser(user);

                            // Redirect to Home if the user is not the default admin
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                // Authentication failed
                ViewBag.Notification = "Sai tên đăng nhập hoặc mật khẩu";
                return View("Dangnhap");
            }
        }



        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
    }
}
