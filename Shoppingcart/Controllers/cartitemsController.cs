using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shoppingcart.Models;
using Shoppingcart.Models.CodeFirst;

namespace Shoppingcart.Controllers
{
    public class cartitemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: cartitems
        public ActionResult Index()
        {
            return View(db.Cartitem.ToList());
        }

        // GET: cartitems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cartitem cartitem = db.Cartitem.Find(id);
            if (cartitem == null)
            {
                return HttpNotFound();
            }
            return View(cartitem);
        }

        // GET: cartitems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cartitems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Itemid,Count,Creationdate,CustomerID")] cartitem cartitem)
        {
            if (ModelState.IsValid)
            {
                db.Cartitem.Add(cartitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartitem);
        }

        // GET: cartitems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cartitem cartitem = db.Cartitem.Find(id);
            if (cartitem == null)
            {
                return HttpNotFound();
            }
            return View(cartitem);
        }

        // POST: cartitems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Itemid,Count,Creationdate,CustomerID")] cartitem cartitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartitem);
        }

        // GET: cartitems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cartitem cartitem = db.Cartitem.Find(id);
            if (cartitem == null)
            {
                return HttpNotFound();
            }
            return View(cartitem);
        }

        // POST: cartitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cartitem cartitem = db.Cartitem.Find(id);
            db.Cartitem.Remove(cartitem);
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
