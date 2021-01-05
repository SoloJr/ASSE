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
using Moq;
using Ninject;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class DomainServiceTests
    {
        private Domain _domain;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _domain = new Domain
            {
                Name = "Beletristica",
                Id = 1,
                EntireDomainId = null,
                ParentId = null
            };
        }

        [TestMethod]
        public void TestInsertDomain()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IDomainService>();

            var result = service.Insert(_domain);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateDomain()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IDomainService>();

            var result = service.Update(_domain);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteDomain()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IDomainService>();

            //Assert.ThrowsException<DeleteItemException>(() => service.Delete(_domain));
        }
    }
}
