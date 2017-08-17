using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shoppingcart.Models.CodeFirst
{
    public class Order
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public bool Completed { get; set; }

        //public virtual ApplicationUser Customer { get; set; }
        //public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}