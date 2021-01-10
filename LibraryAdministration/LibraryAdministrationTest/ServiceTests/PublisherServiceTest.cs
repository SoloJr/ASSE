using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class PublisherServiceTest
    {
        private Publisher _publisher;

        private PublisherService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _publisher = new Publisher
            {
                Name = "Editura 2000",
                FoundingDate = new DateTime(2000, 1, 1),
                Headquarter = "Bucuresti"
            };
        }


        [TestMethod]
        public void TestInsertPublisher()
        {
            var mockSet = new Mock<DbSet<Publisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Publisher>()).Returns(mockSet.Object);

            _service = new PublisherService(mockContext.Object);
            var result = _service.Insert(_publisher);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Publisher>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdatePublisher()
        {
            var mockSet = new Mock<DbSet<Publisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Publisher>()).Returns(mockSet.Object);

            _publisher.Name = "Update";

            _service = new PublisherService(mockContext.Object);
            var result = _service.Update(_publisher);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<Publisher>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeletePublisher()
        {
            var mockSet = new Mock<DbSet<Publisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Publisher>()).Returns(mockSet.Object);

            _service = new PublisherService(mockContext.Object);
            _service.Delete(_publisher);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<Publisher>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetAllPublishers()
        {
            var data = new List<Publisher>
            {
                _publisher,
                new Publisher
                {
                    Name = "Editura 2001",
                    FoundingDate = new DateTime(2001, 1, 1),
                    Headquarter = "Bucuresti"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Publisher>>();
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Publisher>()).Returns(mockSet.Object);

            _service = new PublisherService(mockContext.Object);

            var pubs = _service.GetAll();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }

        [TestMethod]
        public void TestGetAllPublishersOfABook()
        {
            var data = new List<Publisher>
            {
                _publisher,
                new Publisher
                {
                    Name = "Editura 2001",
                    FoundingDate = new DateTime(2001, 1, 1),
                    Headquarter = "Bucuresti"
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Language = "Romana",
                    Name = "Book 1",
                    Year = 2020
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = books.ElementAt(0).Id,
                    ForRent = 11,
                    RentCount = 10,
                    ReleaseDate = DateTime.Now,
                    Pages = 100,
                    Type = BookType.Hardback,
                    Id = 1,
                    Publisher = data.ElementAt(0),
                    PublisherId = data.ElementAt(0).Id
                },
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = books.ElementAt(0).Id,
                    ForRent = 11,
                    RentCount = 10,
                    ReleaseDate = DateTime.Now,
                    Pages = 100,
                    Type = BookType.Hardback,
                    Id = 1,
                    Publisher = data.ElementAt(1),
                    PublisherId = data.ElementAt(1).Id
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Publisher>>();
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockSetBook = new Mock<DbSet<Book>>();
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisher = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Publisher>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBook.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);

            _service = new PublisherService(mockContext.Object);

            var pubs = _service.GetAllBookPublishersOfABook(books.ElementAt(0).Id);

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }

        [TestMethod]
        public void TestGetAllPublishersOfABookWithDifferentBookIdToTestGet()
        {
            var data = new List<Publisher>
            {
                _publisher,
                new Publisher
                {
                    Name = "Editura 2001",
                    FoundingDate = new DateTime(2001, 1, 1),
                    Headquarter = "Bucuresti"
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Language = "Romana",
                    Name = "Book 1",
                    Year = 2020
                },
                new Book
                {
                    Id = 2,
                    Language = "Ceva",
                    Name = "Book 2",
                    Year = 3030
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = books.ElementAt(0).Id,
                    ForRent = 11,
                    RentCount = 10,
                    ReleaseDate = DateTime.Now,
                    Pages = 100,
                    Type = BookType.Hardback,
                    Id = 1,
                    Publisher = data.ElementAt(0),
                    PublisherId = data.ElementAt(0).Id
                },
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = books.ElementAt(0).Id,
                    ForRent = 11,
                    RentCount = 10,
                    ReleaseDate = DateTime.Now,
                    Pages = 100,
                    Type = BookType.Hardback,
                    Id = 1,
                    Publisher = data.ElementAt(1),
                    PublisherId = data.ElementAt(1).Id
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Publisher>>();
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Publisher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockSetBook = new Mock<DbSet<Book>>();
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBook.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisher = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Publisher>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBook.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);

            _service = new PublisherService(mockContext.Object);

            var pubs = _service.GetAllBookPublishersOfABook(books.ElementAt(0).Id);

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }

        [TestMethod]
        public void TestGetAllBookPublishersOfABookWrongParam()
        {
            const int id = -1;

            var context = new Mock<LibraryContext>();

            var service = new PublisherService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.GetAllBookPublishersOfABook(id));
        }
    }
}
