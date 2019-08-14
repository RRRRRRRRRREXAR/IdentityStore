using Store.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<ProductCategories> Categories { get; }
        IRepository<User> Users { get; }
        IRepository<UserRoles> Roles { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
