using Store.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Context
{
    public class StoreContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategories> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> Roles { get; set; }
        
        public StoreContext()
        {
            Database.SetInitializer<StoreContext>(new StoreDbInitializer());
        } 
        public StoreContext(string ConnectionString):base(ConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
        }
    }

    public class StoreDbInitializer:DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {


            context.Categories.Add(new ProductCategories {Name="test" });
            context.Products.Add(new Product { Description="test", CategoryId=1, Name="Test", Price=200});
            context.Products.Add(new Product { Description = "test2", CategoryId = 1, Name = "Test", Price = 200 });
            context.Products.Add(new Product { Description = "test2", CategoryId = 1, Name = "Test", Price = 200 });
            context.Products.Add(new Product { Description = "test3", CategoryId = 1, Name = "Test", Price = 200 });
            context.Products.Add(new Product { Description = "test4", CategoryId = 1, Name = "Test", Price = 200 });

        }
    }
}
