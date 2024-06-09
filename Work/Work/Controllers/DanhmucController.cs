using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Work.Models;

namespace Work.Controllers
{
    public class DanhmucController : Controller
    {
        BookStore1Entities2 db = new BookStore1Entities2();
        // GET: Danhmuc
        public ActionResult ListCatParView()
        {

            IEnumerable<category> categoryList = db.categories.Where(p => p.status == true).ToList();

            // Truyền danh sách sản phẩm vào view
            return View(categoryList);
        }
        public ActionResult ProductCategory(string id, string search, int? page)
        {
            int pageSize = 6; // Number of products per page
            int pageNumber = page ?? 1;

            IQueryable<product> productList;

            // Try to retrieve a category by ID
            category selectedCategory = db.categories.FirstOrDefault(c => c.categoryID == id);

            if (selectedCategory != null)
            {
                productList = selectedCategory.products.AsQueryable();
            }
            else
            {
                // Try to retrieve a subcategory by ID
                subcategory selectedSubcategory = db.subcategories.FirstOrDefault(s => s.subcategoryID == id);

                if (selectedSubcategory != null)
                {
                    productList = selectedSubcategory.products.AsQueryable();
                }
                else
                {
                    // If neither category nor subcategory is found, create a default query for all products
                    productList = db.products.AsQueryable();
                }
            }

            // If a search term is provided, filter the product list based on the search term
            if (!string.IsNullOrEmpty(search))
            {
                productList = productList.Where(p => p.productName.Contains(search) || p.category.categoryName.Contains(search)|| p.subcategory.subcategoryName.Contains(search));
            }
            productList = productList.OrderBy(p => p.productName);
            // Use PagedList to paginate the product list
            IPagedList<product> pagedProductList = productList.ToPagedList(pageNumber, pageSize);

            // Return the paged product list directly to the view
            return View(pagedProductList);
        }




    }
}
