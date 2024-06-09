using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Work.Models;

namespace Work.Areas.Admin.Controllers
{
    public class categoriesController : Controller
    {
        private BookStore1Entities2 db = new BookStore1Entities2();

        // GET: categories
     
        public ActionResult Index(int? page)
        {
            int pageSize = 10; // Set your desired page size here
            int pageNumber = (page ?? 1);

            var categories = db.categories.ToList(); // Fetch all categories
            return View(categories.ToPagedList(pageNumber, pageSize));
        }


        // GET: categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: categories/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoryID,categoryName,description,status,createAt,updateAt")] category category)
        {
           
                if (ModelState.IsValid)
                {
                    // Generate categoryID
                    category.categoryID = GenerateCategoryID(category.categoryName);

                    // Set createAt to current date and time
                    category.createAt = DateTime.Now;

                    // Add category to the database and save changes
                    db.categories.Add(category);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            
        

            // If ModelState is not valid or there's an exception, return to the Create view with errors
            return View(category);
        }

 



        private string GenerateCategoryID(string categoryName)
        {
            // Extract the first character from supplierName
            string firstLetter = categoryName.Length >= 2 ? categoryName.Substring(0, 1).ToUpper() : "XX";

            // Convert the first letter to uppercase
           

            // Generate a unique supplierID with a numeric suffix
            int suffix = 1;
            string generatedID = $"{firstLetter}{suffix}";

            // Check if the generatedID already exists, increment the suffix until a unique ID is found
            while (db.categories.Any(s => s.categoryID == generatedID))
            {
                // Increment the suffix by 1
                suffix += 1;

                // If the suffix exceeds 9, reset it to 1
                if (suffix > 9)
                {
                    suffix = 1;
                }

                // Generate a new supplierID with the updated suffix
                generatedID = $"{firstLetter}{suffix}";
            }

            return generatedID;
        }




        // GET: categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categoryID,categoryName,description,status,createAt,updateAt")] category category)
        {
            if (ModelState.IsValid)
            {
                category.updateAt = DateTime.Now;
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            category category = db.categories.Find(id);

            // Kiểm tra xem category có tồn tại không
            if (category == null)
            {
                // Nếu không tồn tại, hiển thị thông báo và quay trở lại trang Index
                TempData["ErrorMessage"] = "Cannot find the category to delete.";
                return RedirectToAction("Index");
            }

            // Kiểm tra xem category có đang được sử dụng không
            if (IsCategoryInUse(category))
            {
                // Nếu category đang được sử dụng, hiển thị thông báo và quay trở lại trang Index
                TempData["ErrorMessage"] = "Cannot delete the category as it is currently in use.";
                return RedirectToAction("Index");
            }

            try
            {
                // Xóa category và lưu thay đổi
                db.categories.Remove(category);
                db.SaveChanges();

                // Chuyển hướng về trang Index sau khi xóa thành công
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi khi xóa
                TempData["ErrorMessage"] = "An error occurred while deleting the category: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
        private bool IsCategoryInUse(category category)
        {
            // Thực hiện kiểm tra ở đây, ví dụ: kiểm tra trong các bảng khác trong cơ sở dữ liệu
            // Nếu category đang được sử dụng, trả về true; ngược lại, trả về false
            // Ví dụ: 
            // return db.products.Any(p => p.CategoryId == category.CategoryId);
            // (Giả sử có một bảng products có khóa ngoại trỏ đến category)
            return false; // Cần thay đổi logic kiểm tra dựa trên cấu trúc cơ sở dữ liệu thực tế của bạn
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
