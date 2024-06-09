using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Work.Models;
using PagedList;
using System.Data.Entity.Validation;

namespace Work.Areas.Admin.Controllers
{ 
    public class productsController : Controller
    {
        private BookStore1Entities2 db = new BookStore1Entities2();

        // GET: products
        public ActionResult Index(int? page)
        {
            int pageSize = 10; // Set your desired page size here
            int pageNumber = (page ?? 1);
           

            var products = db.products.Include(p => p.author).Include(p => p.subcategory).OrderByDescending(p => p.createAt);
            return View(products.ToPagedList(pageNumber, pageSize));
        }


        // GET: products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            // Populate dropdowns for category, author, subcategory, and supplier
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName");
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName");
            ViewBag.subcategoryID = new SelectList(db.subcategories, "subcategoryID", "subcategoryName");
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName");

            // Return the view for creating a new product
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productName,description,avatar,status,price,sale,categoryID,subcategoryID,authorID,supplierID,createAt,updateAt,totalStart,totalFeedback,totalOrder")] product product, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Generate productID based on categoryID and subcategoryID
                    string generatedProductID = $"{product.categoryID}{product.subcategoryID}";

                    // Check for duplicate productID in the database
                    bool isDuplicateProductID = db.products.Any(p => p.productID == generatedProductID);

                    if (isDuplicateProductID)
                    {
                        // Add model error if a duplicate productID is found
                        ModelState.AddModelError("productID", "This product ID already exists. Please choose a different one.");

                        // Repopulate dropdowns to maintain selected values
                        PopulateDropdowns(product);

                        // Return the view with the model to show the error message
                        return View(product);
                    }

                    // Continue with the rest of the code if no duplicate productID is found

                    if (image != null && image.ContentLength > 0)
                    {
                        // Generate a unique filename based on productID and the original file extension
                        string filename = $"{generatedProductID}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                        // Combine the path with the generated filename
                        string path = Path.Combine(Server.MapPath("~/Content/client/"), filename);

                        // Save the uploaded file to the specified path
                        image.SaveAs(path);

                        // Set the avatar property of the product object to the generated filename
                        product.avatar = filename;
                    }

                    // Set createAt timestamp
                    product.createAt = DateTime.Now;

                    // Set the generated productID
                    product.productID = generatedProductID;

                    // Add the product object to the database and save changes
                    db.products.Add(product);
                    db.SaveChanges();

                    // Redirect to the Index action after successful creation
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log other exceptions
                LogException(ex);

                // Add a general model error for other exceptions
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
            }

            // If ModelState is not valid or there's an exception, return to the Create view with errors
            PopulateDropdowns(product);

            // Return the view with the model to correct input errors or display the error message
            return View(product);
        }

        private void LogValidationError(DbValidationError validationError)
        {
            // Implement your logging logic here
            // For example, you can log to a file or a logging framework
            // This is a placeholder, and you should customize it based on your logging needs
            Console.WriteLine($"Validation Error - Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
        }


        // Helper method to populate dropdowns
        private void PopulateDropdowns(product product)
        {
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", product.categoryID);
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", product.authorID);
            ViewBag.subcategoryID = new SelectList(db.subcategories, "subcategoryID", "subcategoryName", product.subcategoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", product.supplierID);
        }



        // GET: products/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the product in the database based on the provided ID
            product product = db.products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            // Save the original product ID to use it later
            string originalProductID = product.productID;

            // Populate dropdowns for category, author, subcategory, and supplier
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", product.categoryID);
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", product.authorID);
            ViewBag.subcategoryID = new SelectList(db.subcategories, "subcategoryID", "subcategoryName", product.subcategoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", product.supplierID);

            // Store the original product ID in TempData to be used in the [HttpPost] Edit action
            TempData["OriginalProductID"] = originalProductID;

            // Return the view with the product for editing
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productName,description,avatar,status,price,sale,categoryID,subcategoryID,authorID,supplierID,createAt,updateAt,totalStart,totalFeedback,totalOrder")] product editedProduct, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the original product ID from TempData
                    string originalProductID = TempData["OriginalProductID"] as string;

                    // Retrieve the product from the database using the original product ID
                    product existingProduct = db.products.Find(originalProductID);

                    // Update the product properties with the edited values
                    existingProduct.productName = editedProduct.productName;
                    existingProduct.description = editedProduct.description;
                    existingProduct.status = editedProduct.status;
                    existingProduct.price = editedProduct.price;
                    existingProduct.sale = editedProduct.sale;
                    existingProduct.categoryID = editedProduct.categoryID;
                    existingProduct.subcategoryID = editedProduct.subcategoryID;
                    existingProduct.authorID = editedProduct.authorID;
                    existingProduct.supplierID = editedProduct.supplierID;

                    // Handle image upload and update the avatar property
                    HandleImageUpload(existingProduct, image);

                    // Update the state of the product entity
                    existingProduct.updateAt = DateTime.Now;
                    db.Entry(existingProduct).State = EntityState.Modified;
                    db.SaveChanges();

                    // Redirect to the Index action after successful edit
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
                // Log other exceptions
                LogException(ex);
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
            }

            // If ModelState is not valid or there's an exception, return to the Edit view with errors
            PopulateDropdowns(editedProduct);

            // Return the view with the model to correct input errors or display the error message
            return View(editedProduct);
        }

        private void HandleImageUpload(product editedProduct, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                // Generate a unique filename based on productID and the original file extension
                string uniqueFileName = $"{editedProduct.productID}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                string path = Path.Combine(Server.MapPath("~/Content/client/"), uniqueFileName);

                // Save the uploaded file to the specified path
                image.SaveAs(path);

                // Update the avatar property of the product object to the generated filename
                editedProduct.avatar = uniqueFileName;
            }
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
            Console.WriteLine($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
