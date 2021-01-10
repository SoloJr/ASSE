using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class AuthorTest
    {
        private AuthorValidator _validator;

        [TestInitialize]
        public void Init()
        {
            _validator = new AuthorValidator(); ;
        }

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

            var result = _validator.Validate(author);

            Assert.IsTrue(authorId == author.Id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateAuthorFailNoName()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateAuthorFailNoCountry()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Name = "Ion Creanga"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateAuthorFailNoBirthDate()
        {
            var author = new Author
            {
                Country = "Romania",
                Name = "Ion Creanga"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateAuthorFailNameTooShort()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania",
                Name = "IC"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateAuthorFailNameTooLong()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania",
                Name = "Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _validator.Validate(author);

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
