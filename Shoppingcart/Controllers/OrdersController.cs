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
    public class OrdersController : Universal
    {
        [Authorize]
        // GET: Orders
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var orders = db.Order.AsNoTracking().Where(c => c.CustomerId == user.Id).ToList();
            return View(orders);
        }
        [Authorize]
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        [Authorize]
        // GET: Orders/Create

        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include ="Address,City,State,Zipcode,Country,Phone")]Order order)
         
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var CartItems = db.Cartitem.Where(c => c.CustomerID == user.Id).ToList();
            if (ModelState.IsValid)
            {
                order.Completed = true;
                order.CustomerId = user.Id;
                order.OrderDate = DateTime.Now;
                order.Total = ViewBag.carttotal;
                db.Order.Add(order);
                db.SaveChanges();
                
                foreach (var cartItem in user.Cartitem.ToList())
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ItemId = cartItem.Itemid;
                    orderItem.OrderId = order.ID;
                    orderItem.Quantity = cartItem.Count;
                    orderItem.UnitPrice = cartItem.Item.Price;
                    db.OrderItems.Add(orderItem);
                    db.Cartitem.Remove(cartItem);
                    db.SaveChanges();
                }
              
                return RedirectToAction("Details", new { id = order.ID });
            }

            return View(order);
        }

        // GET: Orders/Edit/5
    
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Order.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
      
        //public ActionResult Edit([Bind(Include = "ID,Address,City,State,Zipcode,Country,Phone,Total,OrderDate,CustomerId,Completed")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}

        // GET: Orders/Delete/5
    
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Order.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        // POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
      
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Order.Find(id);
        //    db.Order.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
