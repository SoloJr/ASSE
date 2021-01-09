﻿using System;
using System.Collections.Generic;
using System.Linq;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class BookTest
    {
        private BookValidator _bookValidator;

        [TestInitialize]
        public void Init()
        {
            _bookValidator = new BookValidator();
        }

        [TestMethod]
        public void TestCreateBookWithoutAuthorsPublishersDomains()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie",
                Year = 1885
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestCreateBookWithoutName()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Year = 1885
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        [TestMethod]
        public void TestCreateBookWithoutYear()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie"
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        [TestMethod]
        public void TestCreateBookWithoutLanguage()
        {
            var book = new Book()
            {
                Year = 1885,
                Name = "Amintiri din Copilarie"
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

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

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("The book cannot be in more")));
        }

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

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("The book cannot be in more")));
        }

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

            var result = _bookValidator.Validate(book);

            Assert.AreEqual(bookId, book.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
        }

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

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
        }

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

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
        }
    }
}
