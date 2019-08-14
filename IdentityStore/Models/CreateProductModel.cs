using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityStore.Models
{
    public class CreateProductModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public ProductViewModel Product { get; set; }
    }
}