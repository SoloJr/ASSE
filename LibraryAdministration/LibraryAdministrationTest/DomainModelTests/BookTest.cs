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

        [TestInitialize]
        public void Init()
        {
            _bookServiceMock = new Mock<IBookService>();
            _bookValidator = new BookValidator();
            _bookPublisherValidator = new BookPublisherValidator();
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

            var result = _bookPublisherValidator.Validate(bookPublisher);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
            Assert.IsTrue(book.Publishers.Count > 0);
            Assert.IsNotNull(bookPublisher.Book);
            Assert.IsNotNull(bookPublisher.Publisher);
            Assert.IsTrue(bookPublisherId == bookPublisher.Id);
        }
    }
}
