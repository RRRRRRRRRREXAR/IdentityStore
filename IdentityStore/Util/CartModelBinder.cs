using IdentityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityStore.Util
{
    public class CartModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            // получаем данные по одному полю
            ValueProviderResult vprId = valueProvider.GetValue("Id");

            // получаем данные по остальным полям
            int price = (int)valueProvider.GetValue("Price").ConvertTo(typeof(int));
            string name = (string)valueProvider.GetValue("Name").ConvertTo(typeof(string));
            string description = (string)valueProvider.GetValue("Description").ConvertTo(typeof(string));
            CategoryViewModel category = (CategoryViewModel)valueProvider.GetValue("Category").ConvertTo(typeof(CategoryViewModel));

            ProductViewModel product = new ProductViewModel {Category=category, Description=description, Name=name, Price=price};

            
            return product;
        }
    }
}