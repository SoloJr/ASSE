using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class BookServiceTest
    {
        private Book _book;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Authors = new List<Author>
                {
                    new Author
                    {
                        BirthDate = new DateTime(1970, 1, 1),
                        Name = "Mark Manson",
                        Country = "USA",
                        Id = 1
                    }
                },
                Domains = new List<Domain>
                {
                    new Domain
                    {
                        Name = "Beletristica",
                        Id = 1,
                        EntireDomainId = null,
                        ParentId = null
                    }
                },
                Language = "Romana",
                Publishers = new List<BookPublisher>
                {
                    new BookPublisher
                    {
                        Id = 1,
                        BookId = 1,
                        Pages = 300,
                        Count = 10,
                        PublisherId = 1
                    }
                },
                Id = 1,
                Year = 2015
            };
        }

        [TestMethod]
        public void TestInsertBook()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookService>();

            var result =  service.Insert(_book);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateBook()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookService>();

            var result = service.Update(_book);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteBook()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookService>();

            Assert.ThrowsException<DeleteItemException>(() => service.Delete(_book));
        }
    }
}
