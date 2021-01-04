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
    class PersonalInfoServiceTest
    {
        private PersonalInfo _personalInfo;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _personalInfo = new PersonalInfo
            {
                PhoneNumber = "0731233233",
                Email = "mircea.solo1995@gmail.com"
            };
        }

        [TestMethod]
        public void TestInsertPersonalInfo()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IPersonalInfoService>();

            var result = service.Insert(_personalInfo);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdatePersonalInfo()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IPersonalInfoService>();

            var result = service.Update(_personalInfo);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeletePersonalInfo()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IPersonalInfoService>();

            Assert.ThrowsException<DeleteItemException>(() => service.Delete(_personalInfo));
        }
    }
}
