//---------------------------------------------------------------------
// <copyright file="BookServiceTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.ServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using LibraryAdministration.BusinessLayer;
    using LibraryAdministration.DataMapper;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Helper;
    using LibraryAdministration.Interfaces.Business;
    using LibraryAdministration.Startup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;

    /// <summary>
    /// BookServiceTest class
    /// </summary>
    [TestClass]
    public class BookServiceTest
    {
        /// <summary>
        /// The book
        /// </summary>
        private Book book;

        /// <summary>
        /// The service
        /// </summary>
        private IBookService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.book = new Book
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

        /// <summary>
        /// Tests the insert book.
        /// </summary>
        [TestMethod]
        public void TestInsertBook()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            this.service = new BookService(mockContext.Object);
            var result = this.service.Insert(this.book);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
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

        /// <summary>
        /// Tests the update book.
        /// </summary>
        [TestMethod]
        public void TestUpdateBook()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            this.book.Name = "Update";

            this.service = new BookService(mockContext.Object);
            var result = this.service.Update(this.book);
            try
            {
                mockSet.Verify(m => m.Attach(It.IsAny<Book>()), Times.Once());
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

        /// <summary>
        /// Tests the delete book.
        /// </summary>
        [TestMethod]
        public void TestDeleteBook()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            this.service = new BookService(mockContext.Object);
            this.service.Delete(this.book);
            try
            {
                mockSet.Verify(m => m.Remove(It.IsAny<Book>()), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Tests the get all books.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooks()
        {
            var data = new List<Book>
            {
                this.book,
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

            this.service = new BookService(mockContext.Object);

            var authors = this.service.GetAll();

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 2);
        }

        /// <summary>
        /// Tests the insert book no authors null should fail.
        /// </summary>
        [TestMethod]
        public void TestInsertBookNoAuthorsNullShouldFail()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            var testBook = this.book;
            testBook.Authors = null;

            this.service = new BookService(mockContext.Object);
            var result = this.service.Insert(testBook);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Never);
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

        /// <summary>
        /// Tests the insert book no authors empty should fail.
        /// </summary>
        [TestMethod]
        public void TestInsertBookNoAuthorsEmptyShouldFail()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Book>()).Returns(mockSet.Object);

            var testBook = this.book;
            testBook.Authors = new List<Author>();

            this.service = new BookService(mockContext.Object);
            var result = this.service.Insert(testBook);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Never);
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

        /// <summary>
        /// Tests the get all domains of book.
        /// </summary>
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
                this.book,
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

            this.service = new BookService(mockContext.Object);

            var result = this.service.GetAllDomainsOfBook(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        /// <summary>
        /// Tests the get all domains of book wrong parameter.
        /// </summary>
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
