using Store.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityStore.Models;
using Store.BLL.DTO;
using PagedList;

namespace IdentityStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController:Controller
    {
        IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;

        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            CreateProductModel model = new CreateProductModel();
            model.Product = new ProductViewModel();
            model.Categories = new List<CategoryViewModel>();
            var cat = service.GetCategories();
            foreach (var e in cat)
            {
                model.Categories.Add(new CategoryViewModel { Id=e.Id, Name=e.Name});
            }
             
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateProduct(CreateProductModel model)
        {
            var cat = service.GetCategories().Single(d=>d.Id==model.Product.Category.Id);
            service.CreateProduct(new Store.BLL.DTO.ProductDTO { Price=model.Product.Price,Id = model.Product.Id, Category = cat, Name = model.Product.Name, Description = model.Product.Description });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChangeProduct(int? id)
        {
            CreateProductModel model = new CreateProductModel();
            var pr = service.GetProduct(id.Value);
            model.Product = new ProductViewModel { Id=pr.Id,Description=pr.Description, Name=pr.Name, Price=pr.Price};
            model.Categories = new List<CategoryViewModel>();
            var cat = service.GetCategories();
            foreach (var e in cat)
            {
                model.Categories.Add(new CategoryViewModel { Id = e.Id, Name = e.Name });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeProduct(CreateProductModel model)
        {
            service.UpdateProduct(new Store.BLL.DTO.ProductDTO { Price = model.Product.Price, Id = model.Product.Id, Category = service.GetCategory(model.Product.Category.Id), Name = model.Product.Name, Description = model.Product.Description });
            return RedirectToAction("Index");
        } 
        
        [HttpGet]
        public ActionResult DeleteProduct(int? id)
        {
            ProductViewModel model = new ProductViewModel();
            var pr = service.GetProduct(id.Value);
            model = new ProductViewModel { Id = pr.Id, Description = pr.Description, Name = pr.Name, Price = pr.Price, Category= new CategoryViewModel {Name=pr.Category.Name } };

            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteProduct(ProductViewModel product)
        {
            service.DeleteProduct(product.Id);
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel category)
        {
            service.CreateCategory(new ProductCategoryDTO { Name=category.Name});
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public ActionResult ChangeCategory(int? id)
        {
            CategoryViewModel category = new CategoryViewModel();
            var cat = service.GetCategory(id.Value);
            category.Id = cat.Id;
            category.Name = cat.Name;
            return View(category);
           
        }
        [HttpPost]
        public ActionResult ChangeCategory(CategoryViewModel category)
        {
            service.UpdateCategory(new ProductCategoryDTO {Id=category.Id, Name=category.Name});
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            CategoryViewModel category = new CategoryViewModel();
            var cat = service.GetCategory(id.Value);
            category.Id = cat.Id;
            category.Name = cat.Name;
            return View(category);
        }
        [HttpPost]
        public ActionResult DeleteCategory(CategoryViewModel category)
        {
            service.DeleteCategory(new ProductCategoryDTO {Id=category.Id, Name=category.Name});
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var pr = service.GetProducts();
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            foreach (var e in pr)
            {
                viewModels.Add(new ProductViewModel { Id = e.Id, Category = new CategoryViewModel { Id = e.Category.Id, Name = e.Category.Name }, Description = e.Description, Name = e.Name, Price = e.Price });
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Categories(int? page)
        {
            var pr = service.GetCategories();
            List<CategoryViewModel> viewModels = new List<CategoryViewModel>();
            foreach (var e in pr)
            {
                viewModels.Add( new CategoryViewModel { Id = e.Id, Name = e.Name });
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }
    }
}