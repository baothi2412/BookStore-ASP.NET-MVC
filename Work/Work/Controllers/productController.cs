using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Work.Models;


namespace Work.Controllers
{
    public class ProductController : Controller
    {
        private BookStore1Entities2 db = new BookStore1Entities2();

        // GET: Product

        [HttpGet]
        public ActionResult Shop(int? page)
        {
            int pageSize = 6; // You can adjust this based on the number of items per page
            int pageNumber = (page ?? 1);

            // Use LINQ to filter products with status = true
            IEnumerable<product> productList = db.products.Where(p => p.status == true).ToList();

            var products = productList.ToPagedList(pageNumber, pageSize);


            return View(products);
        }
        public ActionResult Shopdetail(string id)
        {
            var product = db.products.SingleOrDefault(p => p.productID == id);

            if (product == null)
            {
                // Handle the case where the product with the given ID is not found
                return HttpNotFound();
            }

            // Return the view with a collection containing a single product
            return View(new List<product> { product });
        }



        public JsonResult ListName(string q)
        {
            var data = new HomeController().ListName(q);
            return Json(new
            {
                data = data,

            }, JsonRequestBehavior.AllowGet);
        }
       



       




    }
}
