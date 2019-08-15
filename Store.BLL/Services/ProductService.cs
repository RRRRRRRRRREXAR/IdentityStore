using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.DAL.Entites;
using Store.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork uow;
        public ProductService(IUnitOfWork wrk)
        {
            uow = wrk;
            Logger.InitLogger();
            Logger.Log.Info("shiet!");
        }
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            var pr = uow.Products.GetAll();
            foreach (var s in pr)
            {
                products.Add(new ProductDTO { Id = s.Id, Category =  new ProductCategoryDTO {Id = uow.Categories.Get(s.CategoryId).Id, Name= uow.Categories.Get(s.CategoryId).Name }, Description = s.Description, Name = s.Name, Price = s.Price });
            }
            
            return products;
        }

        public ProductDTO FindProduct(ProductDTO product)
        {
            Func<Product, bool> FindProduct = d => d.Name == product.Name ||d.Description==product.Description || d.Price==product.Price;
            var founded= uow.Products.Find(FindProduct).First();
            var tempCategory = new ProductCategoryDTO();
            if (founded ==null)
            {
                throw new ValidationException("Не найден","");
            }
            tempCategory.Id = uow.Categories.Get(founded.CategoryId).Id;
            tempCategory.Name = uow.Categories.Get(founded.CategoryId).Name;
            return new ProductDTO {Id= founded.Id, Category=tempCategory, Description=founded.Description, Name=founded.Name, Price=founded.Price };
        }

        public ProductDTO GetProduct(int id)
        {
            var product = uow.Products.Get(id);
            var tempCategory = new ProductCategoryDTO();
            if (product==null)
            {
                throw new ValidationException("Товар несуществует","");
            }
            tempCategory.Id = uow.Categories.Get(product.CategoryId).Id;
            tempCategory.Name = uow.Categories.Get(product.CategoryId).Name;
            return new ProductDTO { Id= product.Id, Category=tempCategory, Description=product.Description, Name=product.Name, Price=product.Price};
        }

        public IEnumerable GetCategories()
        {
            var Categories = uow.Categories.GetAll();
            List<ProductCategoryDTO> productCategories = new List<ProductCategoryDTO>();
            foreach (var el in  Categories)
            {
                productCategories.Add(new ProductCategoryDTO {Id=el.Id, Name=el.Name });                
            }
            return productCategories;
        }

        public void MakeOrder(UserDTO user,OrderDTO order)
        {
            List<Product> prd = new List<Product>();
            foreach (var el in order.Products)
            {
                prd.Add(new Product { Id=el.Id, CategoryId=  el.Category.Id, Description=el.Description, Name=el.Name, Price=el.Price });
            }
            Order ord = new Order { Products = new List<Product>(), ShippingAdress = order.ShippingAdress, UserId = order.UserId };
            ord.Products = prd;
            uow.Orders.Create(ord);
            Emailer em = new Emailer();
            em.SentOrder(user,order);

        }
        public ProductCategoryDTO GetCategory(int id)
        {
            var cat = uow.Categories.Get(id);
            return new ProductCategoryDTO { Id = cat.Id, Name = cat.Name };
        }
        

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
