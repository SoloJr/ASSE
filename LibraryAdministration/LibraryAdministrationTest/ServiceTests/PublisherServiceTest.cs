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
    class PublisherServiceTest
    {
        private Publisher _publisher;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _publisher = new Publisher
            {
                Name = "Editura 2000",
                FoundingDate = new DateTime(2000, 1, 1),
                Headquarter = "Bucuresti"
            };
        }

        [TestMethod]
        public void TestInsertPublisher()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IPublisherService>();

            var result = service.Insert(_publisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdatePublisher()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IPublisherService>();

            var result = service.Update(_publisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeletePublisher()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IPublisherService>();

            Assert.ThrowsException<DeleteItemException>(() => service.Delete(_publisher));
        }
    }
}
