using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class DomainServiceTests
    {
        private Mock<IBookService> _bookServiceMock;

        [TestInitialize]
        public void Init()
        {
            _bookServiceMock = new Mock<IBookService>();
        }

        [TestMethod]
        public void Test()
        {

        }
    }
}
