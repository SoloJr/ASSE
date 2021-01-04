using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;
using Ninject.Modules;

namespace LibraryAdministrationTest.Mocks
{
    class MockBindings : NinjectModule
    {
        public override void Load()
        {
            LoadRepositoryLayer();
            LoadServiceLayer();
        }

        private void LoadRepositoryLayer()
        {
            Bind<IBookService>().To<BookServiceMock>();
        }

        private void LoadServiceLayer()
        {
            Bind<IBookRepository>().To<BookRepositoryMock>();
        }
    }

    internal class BookServiceMock : BaseService<Book, IBookRepository>, IBookService
    {
        public BookServiceMock()
            : base(Injector.Get<IBookRepository>(), new BookValidator())
        {

        }

        public IEnumerable<Book> GetBooksWithAuthors()
        {
            var book = new Book
            {
                Name = "name",
                Id = 1,
                Language = "ceva"
            };
            return new List<Book>{book};
        }
    }

    abstract class BaseRepositoryMock<T> : IRepository<T>
        where T : class
    {
        public void Insert(T entity)
        {
            // we can consider that it was inserted
        }

        public void Update(T item)
        {
            // we can consider that it was updated
        }

        public void Delete(T entity)
        {
            throw new DeleteItemException();
            // we can consider that it was deleted
        }

        public T GetById(object id)
        {
            return null;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }
    }

    internal class BookRepositoryMock : BaseRepositoryMock<Book>, IBookRepository
    {

    }

    public class DeleteItemException : Exception
    {
        public DeleteItemException()
        {
            
        }

        public DeleteItemException(string name)
            : base("Just a test mock for this")
        {

        }
    }
}
