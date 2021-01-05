using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataMapper;
using LibraryAdministration.Interfaces.DataAccess;

namespace LibraryAdministration.DataAccessLayer
{
    abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        public virtual void Insert(T entity)
        {
            using (var ctx = new LibraryContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Add(entity);

                ctx.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (var ctx = new LibraryContext())
            {
                var dbSet = ctx.Set<T>();
                dbSet.Attach(item);
                ctx.Entry(item).State = EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        public virtual void Delete(int id)
        {
            Delete(GetById(id));
        }

        public virtual void Delete(T entityToDelete)
        {
            using (var ctx = new LibraryContext())
            {
                var dbSet = ctx.Set<T>();

                if (ctx.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                dbSet.Remove(entityToDelete);

                ctx.SaveChanges();
            }
        }

        public virtual T GetById(int id)
        {
            using (var ctx = new LibraryContext())
            {
                return ctx.Set<T>().Find(id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var ctx = new LibraryContext())
            {
                var dbSet = ctx.Set<T>();

                return dbSet.ToList();
            }
        }
    }
}
