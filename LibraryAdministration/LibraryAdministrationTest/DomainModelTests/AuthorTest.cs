//-----------------------------------------------------------------------
// <copyright file="AuthorTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using System;
    using System.Linq;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Author Tests for Domain Model
    /// </summary>
    [TestClass]
    public class AuthorTest
    {
        /// <summary>
        /// The validator
        /// </summary>
        private AuthorValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.validator = new AuthorValidator();
        }

        /// <summary>
        /// Tests the create author success.
        /// </summary>
        [TestMethod]
        public void TestCreateAuthorSuccess()
        {
            var authorId = 1;
            var author = new Author
            {
                Name = "Ion Creanga",
                BirthDate = new DateTime(1850, 1, 1),
                Country = "Romania",
                DeathDate = new DateTime(2020, 12, 12),
                Id = authorId
            };

            var result = this.validator.Validate(author);

            Assert.IsTrue(authorId == author.Id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the name of the create author fail no.
        /// </summary>
        [TestMethod]
        public void TestCreateAuthorFailNoName()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania"
            };

            var result = this.validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create author fail no country.
        /// </summary>
        [TestMethod]
        public void TestCreateAuthorFailNoCountry()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Name = "Ion Creanga"
            };

            var result = this.validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create author fail no birth date.
        /// </summary>
        [TestMethod]
        public void TestCreateAuthorFailNoBirthDate()
        {
            var author = new Author
            {
                Country = "Romania",
                Name = "Ion Creanga"
            };

            var result = this.validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create author fail name too short.
        /// </summary>
        [TestMethod]
        public void TestCreateAuthorFailNameTooShort()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania",
                Name = "IC"
            };

            var result = this.validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create author fail name too long.
        /// </summary>
        [TestMethod]
        public void TestCreateAuthorFailNameTooLong()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania",
                Name = "Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania"
            };

            var result = this.validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create author with books success.
        /// </summary>
        [TestMethod]
        public void TestCreateAuthorWithBooksSuccess()
        {
            var authorId = 1;
            var author = new Author
            {
                Name = "Ion Creanga",
                BirthDate = new DateTime(1850, 1, 1),
                Country = "Romania",
                DeathDate = new DateTime(2020, 12, 12),
                Id = authorId
            };

            var book = new Book
            {
                Id = 1,
                Language = "Romana",
                Name = "Test"
            };

            author.Books.Add(book);
            book.Authors.Add(author);

            var result = this.validator.Validate(author);

            Assert.IsTrue(authorId == author.Id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.AreEqual(book, author.Books.ElementAt(0));
            Assert.AreEqual(author, book.Authors.ElementAt(0));
            Assert.IsNotNull(author.DeathDate);
        }
    }
}
