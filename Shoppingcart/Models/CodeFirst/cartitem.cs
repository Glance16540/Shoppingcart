using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shoppingcart.Models.CodeFirst
{
    public class cartitem
    {
        public int ID { get; set; }
        public string Itemid { get; set; }
        public int Count { get; set; }
        public DateTime Creationdate { get; set; }
        public string CustomerID { get; set; }

        //public virtual Item Item { get; set; }
        //public virtual ApplicationUser Customer { get; set; }

        //public decimal unitTotal
        //{
        //    get
        //    {
        //        return Count * Item.Price;
        //    }

        //}
    }
}