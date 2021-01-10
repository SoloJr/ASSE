//---------------------------------------------------------------------
// <copyright file="DomainTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using System.Linq;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Domain Tests for Domain Model
    /// </summary>
    [TestClass]
    public class DomainTest
    {
        /// <summary>
        /// The validator
        /// </summary>
        private DomainValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.validator = new DomainValidator();
        }

        /// <summary>
        /// Tests the create domain success.
        /// </summary>
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

            var result = this.validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create domain fail entire domain.
        /// </summary>
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

            var result = this.validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create domain fail parent.
        /// </summary>
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

            var result = this.validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create with books.
        /// </summary>
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

            var result = this.validator.Validate(domain);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsTrue(domain.Books.ElementAt(0).Id == book.Id);
            Assert.IsTrue(book.Domains.ElementAt(0).Id == domain.Id);
        }
    }
}
