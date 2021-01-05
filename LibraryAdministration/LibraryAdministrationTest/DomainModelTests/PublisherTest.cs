using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class PublisherTest
    {
        private PublisherValidator _validator;
        private Publisher _stub;

        [TestInitialize]
        public void Init()
        {
            _validator = new PublisherValidator();
            _stub = new Publisher
            {
                FoundingDate = new DateTime(2010, 10, 10),
                Headquarter = "Bucharest",
                Name = "Bucurestimea Mare",
                Id = 1
            };
        }

        [TestMethod]
        public void TestCreatePublisher()
        {
            var publisher = _stub;

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreatePublisherWithBook()
        {
            var publisher = _stub;
            publisher.Books.Add(new BookPublisher());

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsTrue(publisher.Books.Count > 0);
        }
    }
}
