using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        public void TestCreatePublisherFailsNoName()
        {
            var publisher = _stub;
            publisher.Name = null;

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestCreatePublisherFailsNameTooShort()
        {
            var publisher = _stub;
            publisher.Name = "b";

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestCreatePublisherFailsNameTooLong()
        {
            var publisher = _stub;
            publisher.Name = "Raaandooooooooooooooooooooooooooooooooooooooooooooooooooooooooooom";

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestCreatePublisherFailsNoFoundingDate()
        {
            var publisher = _stub;
            publisher.FoundingDate = DateTime.MinValue;

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestCreatePublisherFailsNoHeadquarter()
        {
            var publisher = _stub;
            publisher.Headquarter = null;

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestCreatePublisherFailsHeadquarterTooShort()
        {
            var publisher = _stub;
            publisher.Headquarter = "b";

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestCreatePublisherFailsHeadquarterTooLong()
        {
            var publisher = _stub;
            publisher.Headquarter = "Raaandooooooooooooooooooooooooooooooooooooooooooooooooooooooooooom";

            var result = _validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
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

        [TestMethod]
        public void TestCreatePublisherDetails()
        {
            var publisher = _stub;
            publisher.Books.Add(new BookPublisher
            {
                Id = 1,
                BookId = 1,
                PublisherId = 1,
                ForRent = 11,
                RentCount = 10,
                Pages = 100
            });

            var result = _validator.Validate(publisher);

            foreach (var book in publisher.Books)
            {
                Assert.IsNotNull(book.RentCount);
                Assert.IsNotNull(book.Pages);
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsTrue(publisher.Books.Count > 0);
        }
    }
}
