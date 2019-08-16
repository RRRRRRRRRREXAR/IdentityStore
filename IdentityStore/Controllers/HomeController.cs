using IdentityStore.Models;
using Store.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Store.BLL.DTO;

namespace IdentityStore.Controllers
{
    public class HomeController : Controller
    {
        IProductService service;
        public HomeController(IProductService service)
        {

            this.service = service;
        }
        // GET: Product
        public ActionResult Index(int? page)
        {
            var pr = service.GetAllProducts();
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            foreach (var e in pr)
            {
                viewModels.Add(new ProductViewModel { Id = e.Id, Category = new CategoryViewModel { Id = e.Category.Id, Name = e.Category.Name }, Description = e.Description, Name = e.Name, Price = e.Price });
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }
        
        [HttpGet]
        public ActionResult AddToCart(int? id)
        {
            var product = service.GetProduct(id.Value);
            var addedProduct = new ProductViewModel { Id = product.Id, Category = new CategoryViewModel { Id = product.Category.Id, Name = product.Category.Name }, Description = product.Description, Name = product.Name, Price = product.Price };
            var cart = Session["cart"] as List<ProductViewModel>;
            cart.Add(addedProduct);
            Session["cart"] = cart;
            return PartialView(addedProduct);
        }
        [HttpGet]
        public ActionResult GetCart()
        {
            return PartialView(Session["cart"]);
        }
        [HttpGet]
        
        public ActionResult Checkout()
        {
          var cart = Session["cart"] as List<ProductViewModel>;
            OrderViewModel order = new OrderViewModel();
            order.Products = new List<ProductViewModel>();
            order.Products = cart;
            return View(order);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(OrderViewModel order)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var products = Session["Cart"] as List<ProductViewModel>;
            List<ProductDTO> dtoPro = new List<ProductDTO>();
            foreach (var e in products)
            {
                dtoPro.Add(new ProductDTO { Id=e.Id, Category= new ProductCategoryDTO {Id=e.Id, Name=e.Name }, Description=e.Description, Name=e.Name, Price=e.Price });
            }
            service.MakeOrder(new Store.BLL.DTO.UserDTO {  FirstName = user.UserName, Email= user.Email },new Store.BLL.DTO.OrderDTO {ShippingAdress=order.ShippingAdress, UserId= user.Id, Products=dtoPro });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeleteFromCart(int? id)
        {
            var cart = Session["cart"] as List<ProductViewModel>;
            try
            { 
                cart.RemoveAt(id.Value);
            }
            catch
            {

            }
            return PartialView();
        }
        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}