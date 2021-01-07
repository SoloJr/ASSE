using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class BookTest
    {
        private Mock<IBookService> _bookServiceMock;

        private BookValidator _bookValidator;

        private BookPublisherValidator _bookPublisherValidator;

        private BookRentalValidator _bookRentalValidator;

        [TestInitialize]
        public void Init()
        {
            _bookServiceMock = new Mock<IBookService>();
            _bookValidator = new BookValidator();
            _bookPublisherValidator = new BookPublisherValidator();
            _bookRentalValidator = new BookRentalValidator();
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
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
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
                }
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("Too many domains")));
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
                }
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("Too many domains")));
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
                Id = bookId
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
                Year = 1885
            };

            var bookPublisher = new BookPublisher
            {
                BookId = book.Id,
                PublisherId = publisher.Id,
                Count = 200,
                Pages = 200
            };

            book.Publishers.Add(bookPublisher);

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
        }

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
                Year = 1885
            };

            var bookPublisher = new BookPublisher
            {
                BookId = book.Id,
                PublisherId = publisher.Id,
                Count = 200,
                Pages = 200,
                Type = BookType.Hardback,
                ReleaseDate = new DateTime(2020, 12, 12)
            };

            book.Publishers.Add(bookPublisher);

            var result = _bookPublisherValidator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
        }

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
                Year = 1885
            };

            var bookPublisherId = 1;
            var bookPublisher = new BookPublisher
            {
                Id = bookPublisherId,
                Count = 200,
                Pages = 200,
                Type = BookType.Hardback,
                Book = book,
                BookId = book.Id,
                Publisher = publisher,
                PublisherId = publisher.Id,
                ReleaseDate = new DateTime(2020, 12, 31)
            };

            book.Publishers.Add(bookPublisher);

            var result = _bookPublisherValidator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
            Assert.IsNotNull(bookPublisher.Book);
            Assert.IsNotNull(bookPublisher.Publisher);
            Assert.IsTrue(bookPublisherId == bookPublisher.Id);
        }

        [TestMethod]
        public void TestBookRental()
        {
            var bookRental = new BookRental
            {
                RentBookPublisherId = 1,
                ForRent = 100,
                Id = 1
            };

            var book = new BookPublisher()
            {
                Id = 1,
                BookId = 1,
                PublisherId = 1,
                Count = 200,
                Pages = 100,
                Type = BookType.Hardback
            };

            bookRental.BookPublisher = book;
            bookRental.RentBookPublisherId = book.Id;



            Assert.IsNotNull(bookRental.BookPublisher);
            var result = _bookRentalValidator.Validate(bookRental);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
        }
    }
}
