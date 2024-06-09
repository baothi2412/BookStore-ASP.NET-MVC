
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using Work.App_Start;
using Work.Models;

namespace Work.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        [HttpGet]
        public ActionResult Index()
        {
            user u = Session["userinfo"] as user;
            CartShop gh = Session["cartshop"] as CartShop;
            ViewData["cartlist"] = gh;
            if (u == null)
            {
                return RedirectToAction("Dangnhap", "User");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SaveToDataBase(order o)
        {
            using (var context = new BookStore1Entities2())
            {
                using (DbContextTransaction trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        //order o = new order();
                        o.orderID = Common.GetOrderCount() + 1 + 24000000;
                        o.orderDate = DateTime.Now;
                        o.deliveryDate = DateTime.Now.AddDays(2);
                        user u = SessionConfig.GetUser();
                        o.userID = u.userID;
                        o.paymentID = 2;
                        o.deliveryID = 4;
                        context.Set<order>().Add(o);
                        context.SaveChanges();

                        CartShop gh = Session["cartshop"] as CartShop;
                        foreach (bill b in gh.orderedProduct.Values)
                        {
                            b.orderID = o.orderID;
                            context.bills.Add(b);
                        }
                        context.SaveChanges();

                        trans.Commit();

                        SessionConfig.SetOrder(o);
                        return RedirectToAction("success", "checkout");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }

            return RedirectToAction("Index", "Checkout");
        }

        public ActionResult Success()
        {
            CartShop gh = Session["cartshop"] as CartShop;
            ViewData["cartlist"] = gh;
            Session["cartshop"] = new CartShop();
            return View();
        }
    }
}