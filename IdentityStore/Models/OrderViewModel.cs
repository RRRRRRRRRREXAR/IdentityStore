using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityStore.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
        public string ShippingAdress { get; set; }
    }
}