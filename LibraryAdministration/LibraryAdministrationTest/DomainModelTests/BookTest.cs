//---------------------------------------------------------------------
// <copyright file="BookTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Book Test for Domain Model
    /// </summary>
    [TestClass]
    public class BookTest
    {
        /// <summary>
        /// The validator
        /// </summary>
        private BookValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.validator = new BookValidator();
        }

        /// <summary>
        /// Tests the create book without authors publishers domains.
        /// </summary>
        [TestMethod]
        public void TestCreateBookWithoutAuthorsPublishersDomains()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie",
                Year = 1885
            };

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        /// <summary>
        /// Tests the name of the create book without.
        /// </summary>
        [TestMethod]
        public void TestCreateBookWithoutName()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Year = 1885
            };

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        /// <summary>
        /// Tests the create book without year.
        /// </summary>
        [TestMethod]
        public void TestCreateBookWithoutYear()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie"
            };

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        /// <summary>
        /// Tests the create book without language.
        /// </summary>
        [TestMethod]
        public void TestCreateBookWithoutLanguage()
        {
            var book = new Book()
            {
                Year = 1885,
                Name = "Amintiri din Copilarie"
            };

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        /// <summary>
        /// Tests the create book with too many domains.
        /// </summary>
        [TestMethod]
        public void TestCreateBookWithTooManyDomains()
        {
            var domainOne = new Domain
            {
                Name = "Beletristica",
                ParentId = null,
                Id = 1,
                EntireDomainId = null
            };

            var domainTwo = new Domain
            {
                Name = "Stiinta",
                ParentId = null,
                Id = 2,
                EntireDomainId = null
            };

            var book = new Book()
            {
                Language = "Romana",
                Year = 1885,
                Name = "Amintiri din Copilarie",
                Domains = new List<Domain>
                {
                    domainOne,
                    domainTwo
                },
                Authors = new List<Author>
                {
                    new Author()
                }
            };

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("The book cannot be in more")));
        }

        /// <summary>
        /// Tests the kind of the create book with many domains of same.
        /// </summary>
        [TestMethod]
        public void TestCreateBookWithManyDomainsOfSameKind()
        {
            var domainOne = new Domain
            {
                Name = "Beletristica",
                ParentId = null,
                Id = 1
            };

            var domainTwo = new Domain
            {
                Name = "Povesti pentru Copii",
                ParentId = 1,
                Id = 2
            };

            var book = new Book()
            {
                Language = "Romana",
                Year = 1885,
                Name = "Amintiri din Copilarie",
                Domains = new List<Domain>
                {
                    domainOne,
                    domainTwo
                },
                Authors = new List<Author>
                {
                    new Author()
                }
            };

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("The book cannot be in more")));
        }

        /// <summary>
        /// Tests the book identifier.
        /// </summary>
        [TestMethod]
        public void TestBookId()
        {
            var bookId = 1;
            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie",
                Year = 1885,
                Id = bookId,
                Authors = new List<Author>
                {
                    new Author
                    {
                    }
                }
            };

            var result = this.validator.Validate(book);

            Assert.AreEqual(bookId, book.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        /// <summary>
        /// Tests the book with authors.
        /// </summary>
        [TestMethod]
        public void TestBookWithAuthors()
        {
            var author = new Author
            {
                Name = "Ion Creanga",
                Country = "Romania",
                BirthDate = new DateTime(1850, 1, 1)
            };

            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie",
                Year = 1885
            };

            book.Authors.Add(author);

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        /// <summary>
        /// Tests the book with publishers.
        /// </summary>
        [TestMethod]
        public void TestBookWithPublishers()
        {
            var publisher = new Publisher
            {
                Name = "Editura Pentru Copii",
                FoundingDate = new DateTime(2000, 1, 1),
                Headquarter = "Romania",
                Id = 1
            };

            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie",
                Year = 1885,
                Authors = new List<Author>
                {
                    new Author()
                }
            };

            var bookPublisher = new BookPublisher
            {
                BookId = book.Id,
                PublisherId = publisher.Id,
                RentCount = 200,
                Pages = 200
            };

            book.Publishers.Add(bookPublisher);

            var result = this.validator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
        }
    }
}
