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
using Microsoft.AspNet.Identity;

namespace Shoppingcart.Controllers
{
    [Authorize]
    public class cartitemsController : Universal
    {


        // GET: cartitems
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user.Cartitem.ToList());
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
        //public ActionResult Create()
        //{
        //    return View();
        //}



        // POST: cartitems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? itemId)
        {
            if (itemId != null)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null)
                {
                    return RedirectToAction("Login","Account");
                }
                if (db.Cartitem.Where(c => c.CustomerID == user.Id).Any(c => c.Itemid == itemId))
                {
                    var existingItem = db.Cartitem.Where(c => c.CustomerID == user.Id).FirstOrDefault(c => c.Itemid == itemId);
                    existingItem.Count += 1;
                    db.SaveChanges();
                }
                else
                {
                    cartitem cart = new cartitem();
                    cart.Itemid = (int)itemId;
                    cart.CustomerID = user.Id;
                    cart.Count = 1;
                    cart.Creationdate = DateTime.Now;
                    db.Cartitem.Add(cart);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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
