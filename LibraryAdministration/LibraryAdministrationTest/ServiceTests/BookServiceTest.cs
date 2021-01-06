using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class BookServiceTest
    {
        private Book _book;

        private IBookService _service;


        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Authors = new List<Author>
                {
                    new Author
                    {
                        BirthDate = new DateTime(1970, 1, 1),
                        Name = "Mark Manson",
                        Country = "USA",
                        Id = 1
                    }
                },
                Domains = new List<Domain>
                {
                    new Domain
                    {
                        Name = "Beletristica",
                        Id = 1,
                        EntireDomainId = null,
                        ParentId = null
                    }
                },
                Language = "Romana",
                Publishers = new List<BookPublisher>
                {
                    new BookPublisher
                    {
                        Id = 1,
                        BookId = 1,
                        Pages = 300,
                        Count = 10,
                        PublisherId = 1
                    }
                },
                Id = 1,
                Year = 2015
            };
        }

        [TestMethod]
        public void TestInsertBook()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            _service = new BookService(mockContext.Object);
            var result = _service.Insert(_book);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Book>())), Times.Once());
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
        public void TestUpdateBook()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            _book.Name = "Update";

            _service = new BookService(mockContext.Object);
            var result = _service.Update(_book);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<Book>())), Times.Once());
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
        public void TestDeleteBook()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            _service = new BookService(mockContext.Object);
            _service.Delete(_book);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<Book>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetAllBooks()
        {
            var data = new List<Book>
            {
                _book,
                new Book
                {
                    Name = "Arta Subtila a Seductiei",
                    Authors = new List<Author>
                    {
                        new Author
                        {
                            BirthDate = new DateTime(1970, 1, 1),
                            Name = "Mark Manson",
                            Country = "USA",
                            Id = 1
                        }
                    },
                    Language = "Romana",
                    Publishers = new List<BookPublisher>
                    {
                        new BookPublisher
                        {
                            Id = 1,
                            BookId = 1,
                            Pages = 300,
                            Count = 10,
                            PublisherId = 1
                        }
                    },
                    Id = 1,
                    Year = 2015
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            _service = new BookService(mockContext.Object);

            var authors = _service.GetAll();

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 2);
        }
    }
}
