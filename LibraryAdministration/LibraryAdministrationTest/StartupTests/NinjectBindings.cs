using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;

namespace LibraryAdministrationTest.StartupTests
{
    [TestClass]
    public class NinjectBindings
    {
        [TestMethod]
        public void TestInjector()
        {
            Injector.Inject(new MockBindings());
            var kernel = Injector.Kernel;

            Assert.IsNotNull(kernel);
        }

        [TestMethod]
        public void TestInjectorNoArgs()
        {
            Assert.ThrowsException<NullReferenceException>(() => Injector.Inject(null));
        }

        [TestMethod]
        public void TestInjectorGet()
        {
            Injector.Inject(new MockBindings());
            var kernel = Injector.Kernel;

            var service = kernel.Get<IDomainService>();

            Assert.IsNotNull(service);
            Assert.IsNotNull(kernel);
        }

        [TestMethod]
        public void TestTypeInjectionService()
        {
            Injector.Inject(new MockBindings());
            var kernel = Injector.Kernel;

            var service = kernel.Get<IDomainService>();

            Assert.IsTrue(service is DomainService);

            Assert.IsNotNull(service);
            Assert.IsNotNull(kernel);
        }

        [TestMethod]
        public void TestTypeInjectionRepository()
        {
            Injector.Inject(new MockBindings());
            var kernel = Injector.Kernel;

            var repo = kernel.Get<IDomainRepository>();

            Assert.IsTrue(repo is DomainRepository);

            Assert.IsNotNull(repo);
            Assert.IsNotNull(kernel);
        }
    }
}
