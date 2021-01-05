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
    public class ReaderServiceTest
    {
        private Reader _reader;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112",
                FirstName = "Mircea",
                LastName = "Solovastru"
            };
        }

        [TestMethod]
        public void TestInsertReader()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IReaderService>();

            var result = service.Insert(_reader);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateReader()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IReaderService>();

            var result = service.Update(_reader);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteReader()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IReaderService>();

            //Assert.ThrowsException<DeleteItemException>(() => service.Delete(_reader));
        }
    }
}
