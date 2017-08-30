using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shoppingcart.Models
{
    public class Universal: Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        { base.OnActionExecuting(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.FullName = user.FullName;


                ViewBag.CartItems = db.Cartitem.AsNoTracking().Where(c =>c.CustomerID == user.Id).ToList();
                ViewBag.carttotal = user.Cartitem.Sum(a => a.UnitTotal);
            }
        }
    }
}