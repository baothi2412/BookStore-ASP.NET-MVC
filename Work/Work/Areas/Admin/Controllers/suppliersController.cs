using PagedList;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Work.Models;

namespace Work.Areas.Admin.Controllers
{
    public class SuppliersController : Controller
    {
        private BookStore1Entities2 db = new BookStore1Entities2();

        // GET: suppliers
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var suppliers = db.suppliers.OrderBy(s => s.supplierName).ToList();
            var pagedSuppliers = suppliers.ToPagedList(pageNumber, pageSize);

            return View(pagedSuppliers);
        }

        // GET: suppliers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        // GET: suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "supplierID,supplierName,email,phone,status")] supplier supplier)
        {
            if (ModelState.IsValid)
            {
                // Generate supplierID based on supplierName
                supplier.supplierID = GenerateSupplierID(supplier.supplierName);

                db.suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        private string GenerateSupplierID(string supplierName)
        {
            // Extract the first three characters from supplierName
            string prefix = supplierName.Length >= 3 ? supplierName.Substring(0, 3).ToUpper() : "XXX";

            // Initialize a counter
            int counter = 1;

            // Generate a unique supplierID
            string generatedID = $"{prefix}{counter:D2}"; // Ensures counter is always two digits

            // Check if the generatedID already exists, increment the counter until a unique ID is found
            while (db.suppliers.Any(s => s.supplierID == generatedID))
            {
                counter += 1; // Increment counter by 1

                // Generate a new supplierID with the updated counter
                generatedID = $"{prefix}{counter:D2}";
            }

            return generatedID;
        }




        // ... existing code ...

        // GET: suppliers/Edit/5
        // POST: suppliers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "supplierID,supplierName,email,phone,status")] supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing entity from the database
                    var existingSupplier = db.suppliers.Find(supplier.supplierID);

                    if (existingSupplier == null)
                    {
                        return HttpNotFound();
                    }

                    // Update other properties
                    existingSupplier.supplierName = supplier.supplierName;
                    existingSupplier.email = supplier.email;
                    existingSupplier.phone = supplier.phone;
                    existingSupplier.status = supplier.status;

                    // Generate supplierID only if it is null (indicating a new supplier)
                    if (existingSupplier.supplierID == null)
                    {
                        existingSupplier.supplierID = GenerateSupplierID(existingSupplier.supplierName);
                    }

                    // Mark the entity as modified
                    db.Entry(existingSupplier).State = EntityState.Modified;

                    // Save changes to the database
                    db.SaveChanges();

                    // Clear model state errors on successful update
                    ModelState.Clear();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (supplier)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. The entity was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (supplier)databaseEntry.ToObject();

                        if (databaseValues.supplierName != clientValues.supplierName)
                        {
                            ModelState.AddModelError("supplierName", "Current value: " + databaseValues.supplierName);
                        }

                        // Handle other properties as needed...

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value.");
                    }

                    return View(supplier);
                }
            }

            return View(supplier);
        }



        // GET: suppliers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            supplier supplier = db.suppliers.SingleOrDefault(c => c.supplierID == id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }


        // POST: suppliers/Delete/5
        // POST: suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                supplier supplier = db.suppliers.Find(id);

                if (supplier == null)
                {
                    return HttpNotFound();
                }

                // Update foreign key references in the products table
                foreach (var product in supplier.products.ToList())
                {
                    product.supplierID = null;  // Set the foreign key to null
                }

                // Update foreign key references in the subcategories table
                foreach (var subcategory in supplier.subcategories.ToList())
                {
                    subcategory.supplierID = null;  // Set the foreign key to null
                }

                // Save changes to the database before removing the supplier
                db.SaveChanges();

                // Remove the supplier from the DbSet
                db.suppliers.Remove(supplier);

                // Save changes to the database
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception due to foreign key constraint violation
                ViewBag.ErrorMessage = "This supplier cannot be deleted because it is referenced by other records.";
                return View("Error");
            }
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
