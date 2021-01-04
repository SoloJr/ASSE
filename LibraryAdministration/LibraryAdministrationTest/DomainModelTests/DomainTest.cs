using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class DomainTest
    {
        private DomainValidator _validator;

        [TestInitialize]
        public void Init()
        {
            _validator = new DomainValidator();
        }

        [TestMethod]
        public void TestCreateDomainSuccess()
        {
            var domain = new Domain
            {
                Id = 1,
                Name = "Test Domain",
                ParentId = null,
                EntireDomainId = null
            };

            var result = _validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateDomainFailEntireDomain()
        {
            var domain = new Domain
            {
                Id = 1,
                Name = "Test Domain",
                ParentId = 2,
                EntireDomainId = null
            };

            var result = _validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateDomainFailParent()
        {
            var domain = new Domain
            {
                Id = 1,
                Name = "Test Domain",
                ParentId = null,
                EntireDomainId = 3
            };

            var result = _validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
