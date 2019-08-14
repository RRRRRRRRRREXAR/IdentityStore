using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.DAL.Entites;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class AdminService:IAdminService
    {
        private IUnitOfWork uow;
        public AdminService(IUnitOfWork wrk)
        {
            uow = wrk;
        }

        public void CreateProduct(ProductDTO product)
        {
            uow.Products.Create(new Product { CategoryId=product.Category.Id, Description=product.Description, Name=product.Name, Price=product.Price });
        }

        public void UpdateProduct(ProductDTO product)
        {
            uow.Products.Update(new Product {Id=product.Id, CategoryId = product.Category.Id, Description=product.Description, Name=product.Name, Price=product.Price});
        }

        public void DeleteProduct(int id)
        {
            uow.Products.Delete(id);
        }

        public void CreateCategory(ProductCategoryDTO category)
        {
            uow.Categories.Create(new ProductCategories { Name=category.Name });
        }

        public void UpdateCategory(ProductCategoryDTO category)
        {
            uow.Categories.Update(new ProductCategories { Name=category.Name,Id=category.Id});
        }

        public void DeleteCategory(ProductCategoryDTO category)
        {
            uow.Categories.Delete(category.Id);
        }
        public List<ProductCategoryDTO> GetCategories()
        {
            var Categories = uow.Categories.GetAll();
            List<ProductCategoryDTO> productCategories = new List<ProductCategoryDTO>();
            foreach (var el in Categories)
            {
                productCategories.Add(new ProductCategoryDTO { Id = el.Id, Name = el.Name });
            }
            return productCategories;
        }

        public List<ProductDTO> GetProducts()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            var pr = uow.Products.GetAll();
            foreach (var s in pr)
            {
                products.Add(new ProductDTO { Id = s.Id, Category = new ProductCategoryDTO { Id = uow.Categories.Get(s.CategoryId).Id, Name = uow.Categories.Get(s.CategoryId).Name }, Description = s.Description, Name = s.Name, Price = s.Price });
            }

            return products;
        }

        public ProductDTO GetProduct(int id)
        {
            var product = uow.Products.Get(id);
            var tempCategory = new ProductCategoryDTO();
            if (product == null)
            {
                throw new ValidationException("Товар несуществует", "");
            }
            tempCategory.Id = uow.Categories.Get(product.CategoryId).Id;
            tempCategory.Name = uow.Categories.Get(product.CategoryId).Name;
            return new ProductDTO { Id = product.Id, Category = tempCategory, Description = product.Description, Name = product.Name, Price = product.Price };
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
