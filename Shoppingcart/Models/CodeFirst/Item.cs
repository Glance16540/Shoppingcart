using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shoppingcart.Models.CodeFirst
{
    public class Item
    {
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string MediaURL { get; set; }
        public string Description { get; set; }





    }
}