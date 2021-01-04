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
    public class AuthorServiceTest
    {
        private Author _author;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _author = new Author
            {
                Name = "Mark Manson",
                BirthDate = new DateTime(1970, 1, 1),
                Country = "USA",
                Id = 1
            };
        }

        [TestMethod]
        public void TestInsertAuthor()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IAuthorService>();

            var result = service.Insert(_author);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateAuthor()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IAuthorService>();

            var result = service.Update(_author);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteAuthor()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IAuthorService>();

            Assert.ThrowsException<DeleteItemException>(() => service.Delete(_author));
        }
    }
}
