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
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        protected LibraryContext _context;

        protected BaseRepository(LibraryContext context)
        {
            _context = context;
        }

        public virtual void Insert(T entity)
        {
            using (_context)
            {
                var dbSet = _context.Set<T>();
                dbSet.Add(entity);

                _context.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (_context)
            {
                var dbSet = _context.Set<T>();
                dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;

                _context.SaveChanges();
            }
        }

        public virtual void Delete(int id)
        {
            Delete(GetById(id));
        }

        public virtual void Delete(T entityToDelete)
        {
            using (_context)
            {
                var dbSet = _context.Set<T>();

                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                dbSet.Remove(entityToDelete);

                _context.SaveChanges();
            }
        }

        public virtual T GetById(int id)
        {
            using (_context)
            {
                return _context.Set<T>().Find(id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (_context)
            {
                var dbSet = _context.Set<T>();

                return dbSet.ToList();
            }
        }
    }
}
