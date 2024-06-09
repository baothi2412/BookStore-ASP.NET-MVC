using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work.App_Start;
using Work.Models;

namespace Work.Controllers
{
    public class HomeController : Controller
    {
        BookStore1Entities2 db = new BookStore1Entities2();
        public ActionResult Index()
        {

            return View();


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Shop()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult MainMenu()
        {
            try
            {
                // Include subcategories when fetching categories
                IEnumerable<category> categoryList = db.categories.Include("subcategories").ToList();
                return PartialView("MainMenu", categoryList);
            }
            catch (Exception ex)
            {
                // Handle the exception (log, show user-friendly message, etc.)
                ViewBag.ErrorMessage = "An error occurred while fetching category data.";
                return PartialView("Error");
            }
        }
        public ActionResult Search(string search)
        {
            var searchResults = db.products
                .Join(db.categories,
                      product => product.categoryID,
                      category => category.categoryID,
                      (product, category) => new { Product = product, Category = category })
                .Where(joinResult => joinResult.Product.productName.Contains(search) || joinResult.Category.categoryName.Contains(search))
                .OrderBy(joinResult => joinResult.Product.productName)
                .Select(joinResult => joinResult.Product)
                .ToList();

            ViewBag.Search = search;
            ViewBag.AutocompleteResults = db.products
                .Where(x => x.productName.Contains(search))
                .Select(x => x.productName)
                .ToList();

            return View(searchResults);
        }

        public List<string> ListName(string search)
        {
            return db.products
                .Where(x => x.productName.Contains(search))
                .Select(x => x.productName)
                .ToList();
        }
        public ActionResult AddToCart(string productID)
        {
            CartShop gh = Session["cartshop"] as CartShop;

            // If the cart is null, create a new instance
            if (gh == null)
            {
                gh = new CartShop();
                Session["cartshop"] = gh;
            }

            gh.addItem(productID);

            ViewData["cartlist"] = gh;

            int totalItem = gh.cartItem();
            ViewBag.totalCart = totalItem;

            return View("Index");
        }


        public ActionResult OrderTracking()
        {
            user u = SessionConfig.GetUser();
            if (u == null)
            {
                return RedirectToAction("Dangnhap", "User");
            }
            Session["listorder"] = Common.GetOrders(u.userID);
            ViewData["listorder"] = Common.GetOrders(u.userID);
            return View();
        }


    }
}