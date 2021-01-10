//---------------------------------------------------------------------
// <copyright file="NinjectBindingsTests.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.StartupTests
{
    using System;
    using LibraryAdministration.BusinessLayer;
    using LibraryAdministration.DataAccessLayer;
    using LibraryAdministration.Interfaces.Business;
    using LibraryAdministration.Interfaces.DataAccess;
    using LibraryAdministration.Startup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Ninject;

    /// <summary>
    /// BindingsTests class
    /// </summary>
    [TestClass]
    public class NinjectBindingsTests
    {
        /// <summary>
        /// Tests the injector.
        /// </summary>
        [TestMethod]
        public void TestInjector()
        {
            Injector.Inject(new MockBindings());
            var kernel = Injector.Kernel;

            Assert.IsNotNull(kernel);
        }

        /// <summary>
        /// Tests the injector no arguments.
        /// </summary>
        [TestMethod]
        public void TestInjectorNoArgs()
        {
            Assert.ThrowsException<NullReferenceException>(() => Injector.Inject(null));
        }

        /// <summary>
        /// Tests the injector get.
        /// </summary>
        [TestMethod]
        public void TestInjectorGet()
        {
            Injector.Inject(new MockBindings());
            var kernel = Injector.Kernel;

            var service = kernel.Get<IDomainService>();

            Assert.IsNotNull(service);
            Assert.IsNotNull(kernel);
        }

        /// <summary>
        /// Tests the type injection service.
        /// </summary>
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

        /// <summary>
        /// Tests the type injection repository.
        /// </summary>
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
