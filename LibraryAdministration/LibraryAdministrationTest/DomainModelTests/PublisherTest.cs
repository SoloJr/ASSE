//---------------------------------------------------------------------
// <copyright file="PublisherTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using System;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// PublisherTest class
    /// </summary>
    [TestClass]
    public class PublisherTest
    {
        /// <summary>
        /// The validator
        /// </summary>
        private PublisherValidator validator;

        /// <summary>
        /// The this.stub
        /// </summary>
        private Publisher stub;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.validator = new PublisherValidator();
            this.stub = new Publisher
            {
                FoundingDate = new DateTime(2010, 10, 10),
                Headquarter = "Bucharest",
                Name = "Bucurestimea Mare",
                Id = 1
            };
        }

        /// <summary>
        /// Tests the name of the create publisher fails no.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherFailsNoName()
        {
            var publisher = this.stub;
            publisher.Name = null;

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher fails name too short.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherFailsNameTooShort()
        {
            var publisher = this.stub;
            publisher.Name = "b";

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher fails name too long.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherFailsNameTooLong()
        {
            var publisher = this.stub;
            publisher.Name = "Raaandooooooooooooooooooooooooooooooooooooooooooooooooooooooooooom";

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher fails no founding date.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherFailsNoFoundingDate()
        {
            var publisher = this.stub;
            publisher.FoundingDate = DateTime.MinValue;

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher fails no headquarter.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherFailsNoHeadquarter()
        {
            var publisher = this.stub;
            publisher.Headquarter = null;

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher fails headquarter too short.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherFailsHeadquarterTooShort()
        {
            var publisher = this.stub;
            publisher.Headquarter = "b";

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher fails headquarter too long.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherFailsHeadquarterTooLong()
        {
            var publisher = this.stub;
            publisher.Headquarter = "Raaandooooooooooooooooooooooooooooooooooooooooooooooooooooooooooom";

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisher()
        {
            var publisher = this.stub;

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create publisher with book.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherWithBook()
        {
            var publisher = this.stub;
            publisher.Books.Add(new BookPublisher());

            var result = this.validator.Validate(publisher);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsTrue(publisher.Books.Count > 0);
        }

        /// <summary>
        /// Tests the create publisher details.
        /// </summary>
        [TestMethod]
        public void TestCreatePublisherDetails()
        {
            var publisher = this.stub;
            publisher.Books.Add(new BookPublisher
            {
                Id = 1,
                BookId = 1,
                PublisherId = 1,
                ForRent = 11,
                RentCount = 10,
                Pages = 100
            });

            var result = this.validator.Validate(publisher);

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
