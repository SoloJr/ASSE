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
    public class BookPublisherServiceTest
    {
        private BookPublisher _bookPublisher;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _bookPublisher = new BookPublisher
            {
                BookId = 1,
                Count = 10,
                Pages = 200,
                PublisherId = 1,
                ReleaseDate = DateTime.MaxValue,
                Type = BookType.Ebook
            };
        }

        [TestMethod]
        public void TestInsertBookPublisher()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookPublisherService>();

            var result = service.Insert(_bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateBookPublisher()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookPublisherService>();

            var result = service.Update(_bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteBookPublisher()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookPublisherService>();

            Assert.ThrowsException<DeleteItemException>(() => service.Delete(_bookPublisher));
        }
    }
}
