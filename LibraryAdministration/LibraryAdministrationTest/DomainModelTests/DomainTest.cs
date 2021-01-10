using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

        [TestMethod]
        public void TestCreateWithBooks()
        {
            var domain = new Domain
            {
                Id = 1,
                Name = "Test Domain",
                ParentId = null,
                EntireDomainId = null
            };

            var book = new Book()
            {
                Id = 1,
                Language = "Romana",
                Year = 1885,
                Name = "Amintiri din Copilarie"
            };

            book.Domains.Add(domain);
            domain.Books.Add(book);

            var result = _validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsTrue(domain.Books.ElementAt(0).Id == book.Id);
            Assert.IsTrue(book.Domains.ElementAt(0).Id == domain.Id);
        }
    }
}
