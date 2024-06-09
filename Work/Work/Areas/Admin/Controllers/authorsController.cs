using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Work.Models;


namespace Work.Areas.Admin.Controllers
{
    public class authorsController : Controller
    {
        private BookStore1Entities2 db = new BookStore1Entities2();

        // GET: authors
        public ActionResult Index(int? page)
        {
            int pageSize = 10; // Set your desired page size here
            int pageNumber = (page ?? 1);

            var authors = db.authors.ToList(); // Fetch all categories
            return View(authors.ToPagedList(pageNumber, pageSize));
        }

        // GET: authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "authorID,authorName,email,phone,description")] author author)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "authorID,authorName,email,phone,description")] author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            author authorToDelete = db.authors.Include(a => a.products).SingleOrDefault(a => a.authorID == id);

            if (authorToDelete == null)
            {
                return HttpNotFound();
            }

            return View(authorToDelete);
        }

        // POST: authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ? id)
        {
            author authorToDelete = db.authors.Include(a => a.products).SingleOrDefault(a => a.authorID == id);

            if (authorToDelete == null)
            {
                return HttpNotFound();
            }

            // Delete associated products
            db.products.RemoveRange(authorToDelete.products);

            // Delete the author
            db.authors.Remove(authorToDelete);

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
