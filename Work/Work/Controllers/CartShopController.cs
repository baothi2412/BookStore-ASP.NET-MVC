using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work.Models;

namespace Work.Controllers
{
    public class CartShopController : Controller
    {
        // GET: CartShop
        public ActionResult Index()
        {
            CartShop gh = Session["cartshop"] as CartShop;
            ViewData["cartlist"] = gh;
            return View();
        }

        public ActionResult Increase(string productID)
        {
            CartShop gh = Session["cartshop"] as CartShop;
            gh.addItem(productID);
            Session["cartshop"] = gh;
            return RedirectToAction("Index");
        }
        public ActionResult Decrease(string productID)
        {
            CartShop gh = Session["cartshop"] as CartShop;
            gh.decrease(productID);
            Session["cartshop"] = gh;
            return RedirectToAction("Index");
        }

        public ActionResult RemoveItem(string productID)
        {
            CartShop gh = Session["cartshop"] as CartShop;
            gh.deleteItem(productID);
            Session["cartshop"] = gh;
            return RedirectToAction("Index");
        }

    }
}
