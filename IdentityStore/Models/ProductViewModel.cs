using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityStore.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}