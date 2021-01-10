using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
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
                        RentCount = 10,
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
                            RentCount = 10,
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

        [TestMethod]
        public void TestInsertBookNoAuthorsNullShouldFail()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            var testBook = _book;
            testBook.Authors = null;

            _service = new BookService(mockContext.Object);
            var result = _service.Insert(testBook);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Book>())), Times.Never);
                mockContext.Verify(m => m.SaveChanges(), Times.Never);
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestInsertBookNoAuthorsEmptyShouldFail()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            var testBook = _book;
            testBook.Authors = new List<Author>();

            _service = new BookService(mockContext.Object);
            var result = _service.Insert(testBook);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Book>())), Times.Never);
                mockContext.Verify(m => m.SaveChanges(), Times.Never);
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void TestGetAllDomainsOfBook()
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
                ParentId = 1,
                Id = 2,
                EntireDomainId = null
            };

            var domains = new List<Domain>
            {
                domainOne,
                domainTwo
            }.AsQueryable();

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
                            RentCount = 10,
                            PublisherId = 1
                        }
                    },
                    Id = 2,
                    Year = 2015,
                    Domains = new List<Domain>
                    {
                        domainTwo
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetDomain = new Mock<DbSet<Domain>>();
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Books).Returns(mockSet.Object);
            mockContext.Setup(x => x.Domains).Returns(mockSetDomain.Object);

            _service = new BookService(mockContext.Object);

            var result = _service.GetAllDomainsOfBook(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        [TestMethod]
        public void TestGetAllDomainsOfBookWrongParam()
        {
            var wrongParam = -1;

            var context = new Mock<LibraryContext>();

            var service = new BookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.GetAllDomainsOfBook(wrongParam));
        }
    }
}
