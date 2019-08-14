using Store.DAL.Entites;
using Store.DAL.Interfaces;
using Store.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Context
{
    public class UnitOfWork:IUnitOfWork
    {
        private EFRepository<Product> productRepository;
        private EFRepository<ProductCategories> categoriesRepository;
        private EFRepository<User> usersRepository;
        private EFRepository<UserRoles> rolesRepository;
        private EFRepository<Order> ordersRepository;
        private StoreContext db;
        
        public UnitOfWork()
        {
            db = new StoreContext();
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository==null)
                {
                    productRepository = new EFRepository<Product>(db);
                }
                return productRepository;
            }
        }

        public IRepository<ProductCategories> Categories
        {
            get
            {
                if (categoriesRepository == null)
                {
                    categoriesRepository = new EFRepository<ProductCategories>(db);
                }
                return categoriesRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new EFRepository<User>(db);
                }
                return usersRepository;
            }
        }

        public IRepository<UserRoles> Roles
        {
            get
            {
                if (rolesRepository == null)
                {
                    rolesRepository = new EFRepository<UserRoles>(db);
                }
                return rolesRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (ordersRepository == null)
                {
                    ordersRepository = new EFRepository<Order>(db);
                }
                return ordersRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

