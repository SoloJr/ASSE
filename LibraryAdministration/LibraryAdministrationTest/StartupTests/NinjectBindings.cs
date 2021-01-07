using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

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

            var repo = kernel.Get<IDomainService>();

            Assert.IsNotNull(repo);
            Assert.IsNotNull(kernel);
        }
    }
}
