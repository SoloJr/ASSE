﻿//---------------------------------------------------------------------
// <copyright file="BookPublisherTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using System;
    using System.Collections.Generic;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// BookPublisher tests for Domain Model
    /// </summary>
    [TestClass]
    public class BookPublisherTest
    {
        /// <summary>
        /// The validator
        /// </summary>
        private BookPublisherValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.validator = new BookPublisherValidator();
        }

        /// <summary>
        /// Tests the book publisher create.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherCreate()
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
                Id = 1,
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
                Pages = 200,
                Type = BookType.Hardback,
                ReleaseDate = new DateTime(2020, 12, 12),
                ForLecture = 10,
                ForRent = 10
            };

            book.Publishers.Add(bookPublisher);

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
        }

        /// <summary>
        /// Tests the book publisher create with object not identifier.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherCreateWithObjectNotId()
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
                Id = 1,
                Language = "Romanian",
                Name = "Amintiri din Copilarie",
                Year = 1885,
                Authors = new List<Author>
                {
                    new Author()
                }
            };

            var bookPublisherId = 1;
            var bookPublisher = new BookPublisher
            {
                Id = bookPublisherId,
                RentCount = 200,
                Pages = 200,
                Type = BookType.Hardback,
                Book = book,
                BookId = book.Id,
                Publisher = publisher,
                PublisherId = publisher.Id,
                ReleaseDate = new DateTime(2020, 12, 31),
                ForLecture = 10,
                ForRent = 10
            };

            book.Publishers.Add(bookPublisher);

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
            Assert.IsNotNull(bookPublisher.Book);
            Assert.IsNotNull(bookPublisher.Publisher);
            Assert.IsTrue(bookPublisherId == bookPublisher.Id);
        }

        /// <summary>
        /// Tests the book publisher fail without book identifier.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutBookId()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                Type = BookType.Hardback,
                ReleaseDate = DateTime.Now,
                RentCount = 10,
                ForRent = 10,
                ForLecture = 10,
                PublisherId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the book publisher fail without publisher identifier.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutPublisherId()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                Type = BookType.Hardback,
                ReleaseDate = DateTime.Now,
                RentCount = 10,
                ForRent = 10,
                ForLecture = 10,
                BookId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the type of the book publisher fail without.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutType()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                ReleaseDate = DateTime.Now,
                RentCount = 10,
                ForRent = 10,
                ForLecture = 10,
                BookId = 1,
                PublisherId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the book publisher fail without rent count.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutRentCount()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                ReleaseDate = DateTime.Now,
                Type = BookType.Hardback,
                ForRent = 10,
                ForLecture = 10,
                BookId = 1,
                PublisherId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the book publisher fail without for rent.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutForRent()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                ReleaseDate = DateTime.Now,
                Type = BookType.Hardback,
                RentCount = 10,
                ForLecture = 10,
                BookId = 1,
                PublisherId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the book publisher fail without for lecture.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutForLecture()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                ReleaseDate = DateTime.Now,
                Type = BookType.Hardback,
                RentCount = 10,
                ForRent = 10,
                BookId = 1,
                PublisherId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the book publisher fail without release date.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutReleaseDate()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                Type = BookType.Hardback,
                RentCount = 10,
                ForRent = 10,
                ForLecture = 10,
                BookId = 1,
                PublisherId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the book publisher fail without pages.
        /// </summary>
        [TestMethod]
        public void TestBookPublisherFailWithoutPages()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 1,
                Pages = 100,
                Type = BookType.Hardback,
                RentCount = 10,
                ForRent = 10,
                ForLecture = 10,
                BookId = 1,
                PublisherId = 1
            };

            var result = this.validator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
