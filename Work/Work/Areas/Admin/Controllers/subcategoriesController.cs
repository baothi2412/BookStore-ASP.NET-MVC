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
    public class subcategoriesController : Controller
    {
        private BookStore1Entities2 db = new BookStore1Entities2();

        // GET: subcategories
        public ActionResult Index(int? page)
        {
            int pageSize = 10; // Set your desired page size here
            int pageNumber = (page ?? 1);

            var subcategory = db.subcategories.ToList(); // Fetch all categories
            return View(subcategory.ToPagedList(pageNumber, pageSize));
        }

        // GET: subcategories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategory subcategory = db.subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: subcategories/Create
        public ActionResult Create()
        {
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName");
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subcategoryID,subcategoryName,status,categoryID,supplierID,createAt,updateAt")] subcategory subcategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Generate subcategoryID based on the supplierID
                    subcategory.subcategoryID = GenerateSubcategoryID(subcategory.supplierID);

                    // Set createAt to the current date and time
                    subcategory.createAt = DateTime.Now;

                    // Add subcategory to the database and save changes
                    db.subcategories.Add(subcategory);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Log validation errors
                LogValidationErrors(ex);
                ModelState.AddModelError(string.Empty, "Validation failed at the database level. Please check the error messages.");
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                LogException(ex);
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
            }

            // If ModelState is not valid or there's an exception, return to the Create view with errors
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", subcategory.categoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", subcategory.supplierID);
            return View(subcategory);
        }

        private void LogValidationErrors(DbEntityValidationException ex)
        {
            // Log validation errors
            foreach (var entityValidationError in ex.EntityValidationErrors)
            {
                foreach (var validationError in entityValidationError.ValidationErrors)
                {
                    Console.WriteLine($"Entity: {entityValidationError.Entry.Entity.GetType().Name}, Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                }
            }
        }

        private void LogException(Exception ex)
        {
            // Log or handle other exceptions
            Console.WriteLine($"Exception: {ex.Message}");
        }

        private string GenerateSubcategoryID(string supplierID)
        {
            // Ensure that supplierID is not null or empty
            if (string.IsNullOrEmpty(supplierID))
            {
                // Handle the case where supplierID is null or empty
                return "DefaultSubcategoryID"; // or throw an exception or handle as appropriate
            }

            // Extract the first three characters from supplierID
            string prefix = supplierID.Length >= 3 ? supplierID.Substring(0, 3).ToUpper() : "XXX";

            // You can add a counter if needed
            int counter = 1;

            // Generate a unique subcategoryID
            string generatedID = $"{prefix}{counter:D2}"; // Ensures counter is always two digits

            // Check if the generatedID already exists, increment the counter until a unique ID is found
            while (db.subcategories.Any(s => s.subcategoryID == generatedID))
            {
                counter += 1; // Increment counter by 1

                // Generate a new subcategoryID with the updated counter
                generatedID = $"{prefix}{counter:D2}";
            }

            return generatedID;
        }



        // GET: subcategories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            subcategory subcategory = db.subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }

            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", subcategory.categoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", subcategory.supplierID);
            return View(subcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subcategoryID,subcategoryName,status,categoryID,supplierID,createAt,updateAt")] subcategory subcategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Generate a new unique subcategoryID based on supplierID (Modify this logic based on your requirements)
                   
                    subcategory.updateAt = DateTime.Now;
                    // Update the state of the subcategory entity
                    db.Entry(subcategory).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Log validation errors
                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        Console.WriteLine($"Entity: {entityValidationError.Entry.Entity.GetType().Name}, Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }

                ModelState.AddModelError(string.Empty, "Validation failed at the database level. Please check the error messages.");
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
            }

            // If ModelState is not valid or there's an exception, return to the Edit view with errors
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", subcategory.categoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", subcategory.supplierID);
            return View(subcategory);
        }

     


        // GET: subcategories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategory subcategory = db.subcategories.SingleOrDefault(c => c.subcategoryID == id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }


        // POST: subcategories/Delete/5
        // POST: subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            subcategory subcategoryToDelete = db.subcategories.Find(id);

            if (subcategoryToDelete == null)
            {
                return HttpNotFound();
            }

            // Check if the subcategory is associated with any products
            bool isSubcategoryUsedInProducts = db.products.Any(p => p.subcategoryID == id);

            if (isSubcategoryUsedInProducts)
            {
                // Subcategory is used in products, cannot be deleted
                ViewBag.ErrorMessage = "Cannot delete this subcategory as it is currently used in products.";
                return View("Error");
            }

            // Check if categoryID is not already null
            if (subcategoryToDelete.categoryID != null)
            {
                subcategoryToDelete.categoryID = null;
            }

            // Remove the subcategory from the DbSet
            db.subcategories.Remove(subcategoryToDelete);

            // Save changes to the database
            db.SaveChanges();

            return RedirectToAction("Index");
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
