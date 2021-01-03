using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class DomainTest
    {
        private Mock<IDomainService> _domainServiceMock;



        [TestInitialize]
        public void Init()
        {
            _domainServiceMock = new Mock<IDomainService>();
        }

        [TestMethod]
        public void TestCreateDomain()
        {
            var domain = new Domain
            {
                Id = 1,
                Name = "Test Domain",
                ParentId = null
            };

            _domainServiceMock.Setup(m => m.Insert(domain)).Returns(new ValidationResult(null));
        }
    }
}
