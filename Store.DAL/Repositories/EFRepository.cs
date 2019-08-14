using Store.DAL.Context;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
        public class EFRepository<T> : IRepository<T> where T : class
        {
            private StoreContext db;
            DbSet<T> _dbSet;

            public EFRepository(StoreContext context)
            {
                this.db = context;
                _dbSet = context.Set<T>();
            }

            public IEnumerable<T> GetAll()
            {
                return _dbSet.AsNoTracking().ToList();
            }

            public T Get(int id)
            {
                return _dbSet.Find(id);
            }

            public void Create(T item)
            {
                _dbSet.Add(item);
                db.SaveChanges();
            }

            public void Update(T item)
            {
                db.Set<T>().AddOrUpdate(item);
                db.SaveChanges();
            }

            public IEnumerable<T> Find(Func<T, Boolean> predicate)
            {
                return _dbSet.Where(predicate).ToList();
            }

            public void Delete(int id)
            {
                T item = _dbSet.Find(id);
                if (item != null)
                {
                    _dbSet.Remove(item);
                    db.SaveChanges();
                }

            }

            public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
            {
                return Include(includeProperties).ToList();
            }

            public IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
                params Expression<Func<T, object>>[] includeProperties)
            {
                var query = Include(includeProperties);
                return query.Where(predicate).ToList();
            }

            private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
            {
                IQueryable<T> query = _dbSet.AsNoTracking();
                return includeProperties
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
        }
    
}
