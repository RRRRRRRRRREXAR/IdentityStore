using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<Product> Products { get; set; } 

        public string ShippingAdress { get; set; }
    }
}
