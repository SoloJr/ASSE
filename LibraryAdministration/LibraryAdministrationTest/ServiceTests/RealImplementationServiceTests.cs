using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class RealImplementationServiceTests
    {
        private AuthorService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _service = new AuthorService();
        }

        [TestMethod]
        public void TestServiceNotNull()
        {
            Assert.IsNotNull(_service);
        }
    }
}
