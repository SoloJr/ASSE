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
    public class BookRentalServiceTest
    {
        private BookRental _bookRental;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _bookRental = new BookRental
            {
                RentBookId = 1,
                ForRent = 100,
                Id = 1
            };
        }

        [TestMethod]
        public void TestInsertBookRental()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookRentalService>();

            var result = service.Insert(_bookRental);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateBookRental()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookRentalService>();

            var result = service.Update(_bookRental);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteBookRental()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IBookRentalService>();

            //Assert.ThrowsException<DeleteItemException>(() => service.Delete(_bookRental));
        }
    }
}
