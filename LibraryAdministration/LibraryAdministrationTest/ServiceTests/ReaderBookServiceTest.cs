//---------------------------------------------------------------------
// <copyright file="ReaderBookServiceTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.ServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Core;
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
    /// ReaderBookServiceTest class
    /// </summary>
    [TestClass]
    public class ReaderBookServiceTest
    {
        /// <summary>
        /// The reader book
        /// </summary>
        private ReaderBook readerBook;

        /// <summary>
        /// The service
        /// </summary>
        private IReaderBookService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.readerBook = new ReaderBook
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Id = 1,
                ReaderId = 1,
                BookPublisherId = 1,
                ExtensionDays = 0
            };
        }

        /// <summary>
        /// Tests the insert reader book.
        /// </summary>
        [TestMethod]
        public void TestInsertReaderBook()
        {
            var mockSet = new Mock<DbSet<ReaderBook>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<ReaderBook>()).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.Insert(this.readerBook);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<ReaderBook>()), Times.Once());
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
        /// Tests the get all books for reader.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksForReader()
        {
            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 3
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now.AddYears(-1),
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.GetAllBooksOnLoan(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        /// <summary>
        /// Tests the success rent book for number of books.
        /// </summary>
        [TestMethod]
        public void TestSuccessRentBookForNumberOfBooks()
        {
            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 3
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.CheckBeforeLoan(1);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the fail rent book for number of books.
        /// </summary>
        [TestMethod]
        public void TestFailRentBookForNumberOfBooks()
        {
            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 3
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckBeforeLoan(1);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the success rent book for number of books today.
        /// </summary>
        [TestMethod]
        public void TestSuccessRentBookForNumberOfBooksToday()
        {
            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, true);
            var result = this.service.CheckBooksRentedToday(1);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the fail rent book for number of books today.
        /// </summary>
        [TestMethod]
        public void TestFailRentBookForNumberOfBooksToday()
        {
            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.CheckBooksRentedToday(1);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the number of books from same domain in given span reader fail.
        /// </summary>
        [TestMethod]
        public void TestNumberOfBooksFromSameDomainInGivenSpanReaderFail()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 3,
                    Name = "Stiinta",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 4,
                    Name = "Informatica",
                    ParentId = 3,
                    EntireDomainId = 3
                },
                new Domain
                {
                    Id = 5,
                    Name = "ASSE",
                    ParentId = 4,
                    EntireDomainId = 3
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(2)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                },
                new Book
                {
                    Id = 2,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = 1,
                    PublisherId = 1,
                    Id = 1,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now,
                },
                new BookPublisher
                {
                    Book = books.ElementAt(1),
                    BookId = 2,
                    PublisherId = 1,
                    Id = 2,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now
                }
            }.AsQueryable();

            this.readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 1,
                    BookPublisher = bookPublishers.ElementAt(0),
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 1
                },
                new ReaderBook
                {
                    BookPublisherId = 1,
                    BookPublisher = bookPublishers.ElementAt(0),
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 2
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    BookPublisher = bookPublishers.ElementAt(1),
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetDomain = new Mock<DbSet<Domain>>();
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

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
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Domains).Returns(mockSetDomain.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBook.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckPastLoansForDomains(1, 4);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the number of books from same domain in given span reader success.
        /// </summary>
        [TestMethod]
        public void TestNumberOfBooksFromSameDomainInGivenSpanReaderSuccess()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 3,
                    Name = "Stiinta",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 4,
                    Name = "Informatica",
                    ParentId = 3,
                    EntireDomainId = 3
                },
                new Domain
                {
                    Id = 5,
                    Name = "ASSE",
                    ParentId = 4,
                    EntireDomainId = 3
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(2)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                },
                new Book
                {
                    Id = 2,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = 1,
                    PublisherId = 1,
                    Id = 1,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now,
                },
                new BookPublisher
                {
                    Book = books.ElementAt(1),
                    BookId = 2,
                    PublisherId = 1,
                    Id = 2,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now
                }
            }.AsQueryable();

            this.readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 1,
                    BookPublisher = bookPublishers.ElementAt(0),
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetDomain = new Mock<DbSet<Domain>>();
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

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
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Domains).Returns(mockSetDomain.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBook.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckPastLoansForDomains(1, 4);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the number of books from same domain in given span employee fail.
        /// </summary>
        [TestMethod]
        public void TestNumberOfBooksFromSameDomainInGivenSpanEmployeeFail()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 3,
                    Name = "Stiinta",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 4,
                    Name = "Informatica",
                    ParentId = 3,
                    EntireDomainId = 3
                },
                new Domain
                {
                    Id = 5,
                    Name = "ASSE",
                    ParentId = 4,
                    EntireDomainId = 3
                },
                new Domain
                {
                    Id = 6,
                    Name = "Altceva",
                    ParentId = 4,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 7,
                    Name = "Altceva2",
                    ParentId = 3,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 8,
                    Name = "Altceva 3",
                    ParentId = null,
                    EntireDomainId = null
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(2)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                },
                new Book
                {
                    Id = 2,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0),
                        domains.ElementAt(4),
                        domains.ElementAt(5),
                        domains.ElementAt(3)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = 1,
                    PublisherId = 1,
                    Id = 1,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now,
                },
                new BookPublisher
                {
                    Book = books.ElementAt(1),
                    BookId = 2,
                    PublisherId = 1,
                    Id = 2,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now
                }
            }.AsQueryable();

            this.readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 1,
                    BookPublisher = bookPublishers.ElementAt(0),
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 1
                },
                new ReaderBook
                {
                    BookPublisherId = 1,
                    BookPublisher = bookPublishers.ElementAt(0),
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 2
                },
                new ReaderBook
                {
                    BookPublisherId = 2,
                    BookPublisher = bookPublishers.ElementAt(1),
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetDomain = new Mock<DbSet<Domain>>();
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

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
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Domains).Returns(mockSetDomain.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBook.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.CheckPastLoansForDomains(1, 4);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the number of books from same domain in given span employee success.
        /// </summary>
        [TestMethod]
        public void TestNumberOfBooksFromSameDomainInGivenSpanEmployeeSuccess()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 3,
                    Name = "Stiinta",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 4,
                    Name = "Informatica",
                    ParentId = 3,
                    EntireDomainId = 3
                },
                new Domain
                {
                    Id = 5,
                    Name = "ASSE",
                    ParentId = 4,
                    EntireDomainId = 3
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(2)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                },
                new Book
                {
                    Id = 2,
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Name = "test",
                    Language = "test",
                    Year = 2020
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Book = books.ElementAt(0),
                    BookId = 1,
                    PublisherId = 1,
                    Id = 1,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now,
                },
                new BookPublisher
                {
                    Book = books.ElementAt(1),
                    BookId = 2,
                    PublisherId = 1,
                    Id = 2,
                    ForLecture = 10,
                    ForRent = 10,
                    Pages = 100,
                    ReleaseDate = DateTime.Now
                }
            }.AsQueryable();

            this.readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                this.readerBook,
                new ReaderBook
                {
                    BookPublisherId = 1,
                    BookPublisher = bookPublishers.ElementAt(0),
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetDomain = new Mock<DbSet<Domain>>();
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomain.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

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
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Domains).Returns(mockSetDomain.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBook.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckPastLoansForDomains(1, 4);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the fail rent same book.
        /// </summary>
        [TestMethod]
        public void TestFailRentSameBook()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 2,
                BookId = 1
            };

            var bookPublishers = new List<BookPublisher>
            {
                bookPublisher
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    BookPublisher = bookPublishers.ElementAt(0),
                    BookPublisherId = 2,
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 1
                },
                new ReaderBook
                {
                    BookPublisher = bookPublishers.ElementAt(0),
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 2
                },
                new ReaderBook
                {
                    BookPublisher = bookPublishers.ElementAt(0),
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.CheckSameBookRented(1, 1);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the success rent same book already rent.
        /// </summary>
        [TestMethod]
        public void TestSuccessRentSameBookAlreadyRent()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 2,
                BookId = 1
            };

            var bookPublishers = new List<BookPublisher>
            {
                bookPublisher
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    BookPublisher = bookPublishers.ElementAt(0),
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now.AddDays(-14),
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.CheckSameBookRented(1, 1);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the success rent same book never rent.
        /// </summary>
        [TestMethod]
        public void TestSuccessRentSameBookNeverRent()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 2,
                BookId = 2
            };

            var bookPublishers = new List<BookPublisher>
            {
                bookPublisher
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    BookPublisher = bookPublishers.ElementAt(0),
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now.AddDays(-14),
                    Id = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.CheckSameBookRented(1, 1);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the same account.
        /// </summary>
        [TestMethod]
        public void TestSameAccount()
        {
            var mockContext = new Mock<LibraryContext>();

            this.service = new ReaderBookService(mockContext.Object);

            var per = int.Parse(ConfigurationManager.AppSettings["PER"]);
            var nmc = int.Parse(ConfigurationManager.AppSettings["NMC"]);
            var d = int.Parse(ConfigurationManager.AppSettings["D"]);
            var l = int.Parse(ConfigurationManager.AppSettings["L"]);
            var ncz = int.Parse(ConfigurationManager.AppSettings["NCZ"]);
            var delta = int.Parse(ConfigurationManager.AppSettings["DELTA"]);
            var c = int.Parse(ConfigurationManager.AppSettings["C"]);
            var lim = int.Parse(ConfigurationManager.AppSettings["LIM"]);
            var persimp = int.Parse(ConfigurationManager.AppSettings["PERSIMP"]);

            var details = this.service.GetRentDetails();

            Assert.IsTrue(details.C == 2 * c);
            Assert.IsTrue(details.NMC == 2 * nmc);
            Assert.IsTrue(details.D == 2 * d);
            Assert.IsTrue(details.LIM == 2 * lim);
            Assert.IsTrue(details.DELTA == delta / 2);
            Assert.IsTrue(details.PER == per / 2);
        }

        /// <summary>
        /// Tests the different account.
        /// </summary>
        [TestMethod]
        public void TestDifferentAccount()
        {
            var mockContext = new Mock<LibraryContext>();

            this.service = new ReaderBookService(mockContext.Object, false);

            var per = int.Parse(ConfigurationManager.AppSettings["PER"]);
            var nmc = int.Parse(ConfigurationManager.AppSettings["NMC"]);
            var d = int.Parse(ConfigurationManager.AppSettings["D"]);
            var l = int.Parse(ConfigurationManager.AppSettings["L"]);
            var ncz = int.Parse(ConfigurationManager.AppSettings["NCZ"]);
            var delta = int.Parse(ConfigurationManager.AppSettings["DELTA"]);
            var c = int.Parse(ConfigurationManager.AppSettings["C"]);
            var lim = int.Parse(ConfigurationManager.AppSettings["LIM"]);
            var persimp = int.Parse(ConfigurationManager.AppSettings["PERSIMP"]);

            var details = this.service.GetRentDetails();

            Assert.IsTrue(details.C == c);
            Assert.IsTrue(details.NMC == nmc);
            Assert.IsTrue(details.D == d);
            Assert.IsTrue(details.LIM == lim);
            Assert.IsTrue(details.DELTA == delta);
            Assert.IsTrue(details.PER == per);
        }

        /// <summary>
        /// Tests the check before loan wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckBeforeLoanWrongParam()
        {
            const int Id = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckBeforeLoan(Id));
        }

        /// <summary>
        /// Tests the check books rented today wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckBooksRentedTodayWrongParam()
        {
            const int Id = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckBooksRentedToday(Id));
        }

        /// <summary>
        /// Tests the check past loans for domains wrong domain identifier parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckPastLoansForDomainsWrongDomainIdParam()
        {
            const int DomainId = -1;

            const int ReaderId = 1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckPastLoansForDomains(ReaderId, DomainId));
        }

        /// <summary>
        /// Tests the check past loans for domains wrong reader identifier parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckPastLoansForDomainsWrongReaderIdParam()
        {
            const int DomainId = 1;

            const int ReaderId = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckPastLoansForDomains(ReaderId, DomainId));
        }

        /// <summary>
        /// Tests the check same book rented wrong reader identifier parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckSameBookRentedWrongReaderIdParam()
        {
            const int BookId = 1;

            const int ReaderId = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckSameBookRented(BookId, ReaderId));
        }

        /// <summary>
        /// Tests the check same book rented wrong book identifier parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckSameBookRentedWrongBookIdParam()
        {
            const int BookId = -1;

            const int ReaderId = 1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckSameBookRented(BookId, ReaderId));
        }

        /// <summary>
        /// Tests the get all books on loan wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksOnLoanWrongParam()
        {
            const int ReaderId = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.GetAllBooksOnLoan(ReaderId));
        }

        /// <summary>
        /// Tests the check loan extension success employee.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionSuccessEmployee()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.CheckLoanExtension(1, 7);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check loan extension success reader.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionSuccessReader()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckLoanExtension(1, 7);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check loan extension fail employee overextension.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionFailEmployeeOverextension()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 25,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LoanExtensionException>(() => this.service.CheckLoanExtension(1, 7));
        }

        /// <summary>
        /// Tests the check loan extension fail reader overextension.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionFailReaderOverextension()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 10,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LoanExtensionException>(() => this.service.CheckLoanExtension(1, 7));
        }

        /// <summary>
        /// Tests the check loan extension fail employee wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionFailEmployeeWrongParam()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.CheckLoanExtension(-1, 7));
        }

        /// <summary>
        /// Tests the check loan extension fail reader wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionFailReaderWrongParam()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.CheckLoanExtension(-1, 7));
        }

        /// <summary>
        /// Tests the extend loan fail reader wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanFailReaderWrongParam()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.ExtendLoan(-1, 7));
        }

        /// <summary>
        /// Tests the check loan extension fail employee not found.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionFailEmployeeNotFound()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<ObjectNotFoundException>(() => this.service.CheckLoanExtension(100, 7));
        }

        /// <summary>
        /// Tests the extend loan fail employee not found.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanFailEmployeeNotFound()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<ObjectNotFoundException>(() => this.service.ExtendLoan(100, 7));
        }

        /// <summary>
        /// Tests the check loan extension fail reader not found.
        /// </summary>
        [TestMethod]
        public void TestCheckLoanExtensionFailReaderNotFound()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<ObjectNotFoundException>(() => this.service.CheckLoanExtension(100, 7));
        }

        /// <summary>
        /// Tests the extend loan fail reader not found.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanFailReaderNotFound()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<ObjectNotFoundException>(() => this.service.ExtendLoan(100, 7));
        }

        /// <summary>
        /// Tests the check multiple books domain match reader lower than Threshold.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchReaderLowerThanThreshold()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 1,
                    Name = "One",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 2,
                    Name = "Two",
                    ParentId = 1,
                    EntireDomainId = null
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 1
                },
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 2
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    Book = books.ElementAt(0),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 2,
                    Book = books.ElementAt(1),
                    BookId = 2
                }
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30),
                }
            }.AsQueryable();

            var mockSetReaderBookMock = new Mock<DbSet<ReaderBook>>();
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetBookMock = new Mock<DbSet<Book>>();
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisherMock = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockSetDomainMock = new Mock<DbSet<Domain>>();
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSetDomainMock.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBookMock.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisherMock.Object);
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSetReaderBookMock.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check multiple books domain match employee lower than Threshold.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchEmployeeLowerThanThreshold()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 1,
                    Name = "One",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 2,
                    Name = "Two",
                    ParentId = 1,
                    EntireDomainId = null
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 1
                },
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 2
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    Book = books.ElementAt(0),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 2,
                    Book = books.ElementAt(1),
                    BookId = 2
                }
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30),
                }
            }.AsQueryable();

            var mockSetReaderBookMock = new Mock<DbSet<ReaderBook>>();
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSetReaderBookMock.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetBookMock = new Mock<DbSet<Book>>();
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisherMock = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockSetDomainMock = new Mock<DbSet<Domain>>();
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSetDomainMock.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBookMock.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisherMock.Object);
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSetReaderBookMock.Object);

            this.service = new ReaderBookService(mockContext.Object, true);
            var result = this.service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check multiple books domain match reader fails constraint for domain.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchReaderFailsConstraintForDomain()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 1,
                    Name = "One",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 2,
                    Name = "Two",
                    ParentId = 1,
                    EntireDomainId = null
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 1
                },
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 2
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    Book = books.ElementAt(0),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 2,
                    Book = books.ElementAt(1),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 3,
                    Book = books.ElementAt(1),
                    BookId = 2
                }
            }.AsQueryable();

            var mockSetBookMock = new Mock<DbSet<Book>>();
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisherMock = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockSetDomainMock = new Mock<DbSet<Domain>>();
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSetDomainMock.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBookMock.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisherMock.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the check multiple books domain match employee fails constraint for domain.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchEmployeeFailsConstraintForDomain()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 1,
                    Name = "One",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 2,
                    Name = "Two",
                    ParentId = 1,
                    EntireDomainId = null
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 1
                },
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(1)
                    },
                    Id = 2
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    Book = books.ElementAt(0),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 2,
                    Book = books.ElementAt(1),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 3,
                    Book = books.ElementAt(1),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 4,
                    Book = books.ElementAt(1),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 5,
                    Book = books.ElementAt(1),
                    BookId = 2
                }
            }.AsQueryable();

            var mockSetBookMock = new Mock<DbSet<Book>>();
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisherMock = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockSetDomainMock = new Mock<DbSet<Domain>>();
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSetDomainMock.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBookMock.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisherMock.Object);

            this.service = new ReaderBookService(mockContext.Object, true);
            var result = this.service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the check multiple books domain match reader success.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchReaderSuccess()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 1,
                    Name = "One",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 2,
                    Name = "Two",
                    ParentId = 1,
                    EntireDomainId = null
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 1
                },
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(1)
                    },
                    Id = 2
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    Book = books.ElementAt(0),
                    BookId = 1
                },
                new BookPublisher()
                {
                    Id = 2,
                    Book = books.ElementAt(1),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 3,
                    Book = books.ElementAt(1),
                    BookId = 2
                }
            }.AsQueryable();

            var mockSetBookMock = new Mock<DbSet<Book>>();
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisherMock = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockSetDomainMock = new Mock<DbSet<Domain>>();
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSetDomainMock.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBookMock.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisherMock.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check multiple books domain match employee success.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchEmployeeSuccess()
        {
            var domains = new List<Domain>
            {
                new Domain
                {
                    Id = 1,
                    Name = "One",
                    ParentId = null,
                    EntireDomainId = null
                },
                new Domain
                {
                    Id = 2,
                    Name = "Two",
                    ParentId = 1,
                    EntireDomainId = null
                }
            }.AsQueryable();

            var books = new List<Book>
            {
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(0)
                    },
                    Id = 1
                },
                new Book
                {
                    Domains = new List<Domain>
                    {
                        domains.ElementAt(1)
                    },
                    Id = 2
                }
            }.AsQueryable();

            var bookPublishers = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    Book = books.ElementAt(0),
                    BookId = 1
                },
                new BookPublisher()
                {
                    Id = 2,
                    Book = books.ElementAt(1),
                    BookId = 2
                },
                new BookPublisher()
                {
                    Id = 3,
                    Book = books.ElementAt(1),
                    BookId = 2
                }
            }.AsQueryable();

            var mockSetBookMock = new Mock<DbSet<Book>>();
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockSetBookMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            var mockSetBookPublisherMock = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublishers.Provider);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublishers.Expression);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublishers.ElementType);
            mockSetBookPublisherMock.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublishers.GetEnumerator());

            var mockSetDomainMock = new Mock<DbSet<Domain>>();
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(domains.Provider);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(domains.Expression);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(domains.ElementType);
            mockSetDomainMock.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(domains.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSetDomainMock.Object);
            mockContext.Setup(x => x.Books).Returns(mockSetBookMock.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisherMock.Object);

            this.service = new ReaderBookService(mockContext.Object, true);
            var result = this.service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check multiple books domain match fails null parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchFailsNullParam()
        {
            var mockSet = new Mock<DbSet<ReaderBook>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.CheckMultipleBooksDomainMatch(null));
        }

        /// <summary>
        /// Tests the check multiple books domain match fails wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchFailsWrongParam()
        {
            var wrongParameter = new List<int>();

            var mockSet = new Mock<DbSet<ReaderBook>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.CheckMultipleBooksDomainMatch(wrongParameter));
        }

        /// <summary>
        /// Tests the extend loan success employee.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanSuccessEmployee()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<ReaderBook>()).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            var result = this.service.ExtendLoan(1, 7);

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests the extend loan success reader.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanSuccessReader()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<ReaderBook>()).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.ExtendLoan(1, 7);

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests the extend loan fail employee over extension.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanFailEmployeeOverExtension()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 25,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LoanExtensionException>(() => this.service.ExtendLoan(1, 7));
        }

        /// <summary>
        /// Tests the extend loan reader over extension.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanReaderOverExtension()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 10,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LoanExtensionException>(() => this.service.ExtendLoan(1, 7));
        }

        /// <summary>
        /// Tests the check loan extension fail employee wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestExtendLoanEmployeeWrongParam()
        {
            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.ExtendLoan(-1, 7));
        }

        /// <summary>
        /// Tests the get all books rented in between dates success.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksRentedInBetweenDatesSuccess()
        {
            var start = new DateTime(2021, 1, 1);
            var end = new DateTime(2021, 2, 1);
            var bookPublisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1
                },
                new BookPublisher
                {
                    Id = 2,
                    BookId = 1,
                    PublisherId = 2
                },
                new BookPublisher
                {
                    Id = 3,
                    BookId = 2,
                    PublisherId = 2
                }
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    BookPublisher = bookPublisherData.ElementAt(0),
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2021, 1, 5)
                },
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    BookPublisher = bookPublisherData.ElementAt(1),
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 1, 5)
                },
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    BookPublisher = bookPublisherData.ElementAt(1),
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2021, 1, 9)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetBookPublisher = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublisherData.Provider);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublisherData.Expression);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublisherData.ElementType);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublisherData.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<ReaderBook>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSetBookPublisher.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.GetAllBooksRentedInBetweenDates(1, start, end);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
        }

        /// <summary>
        /// Tests the get all books rented in between dates success no data.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksRentedInBetweenDatesSuccessNoData()
        {
            var start = new DateTime(2021, 1, 25);
            var end = new DateTime(2021, 2, 1);
            var bookPublisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1
                },
                new BookPublisher
                {
                    Id = 2,
                    BookId = 1,
                    PublisherId = 2
                },
                new BookPublisher
                {
                    Id = 3,
                    BookId = 2,
                    PublisherId = 2
                }
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    BookPublisher = bookPublisherData.ElementAt(0),
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2021, 1, 5)
                },
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    BookPublisher = bookPublisherData.ElementAt(1),
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 1, 5)
                },
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    BookPublisher = bookPublisherData.ElementAt(1),
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2021, 1, 9)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetBookPublisher = new Mock<DbSet<BookPublisher>>();
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bookPublisherData.Provider);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bookPublisherData.Expression);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bookPublisherData.ElementType);
            mockSetBookPublisher.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bookPublisherData.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<ReaderBook>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBookPublisher.Object);
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSetBookPublisher.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            var result = this.service.GetAllBooksRentedInBetweenDates(1, start, end);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// Tests the get all books rented in between dates fails wrong parameter date start.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksRentedInBetweenDatesFailsWrongParamDateStart()
        {
            var start = DateTime.MinValue.AddDays(1);
            var end = new DateTime(2021, 2, 1);

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.GetAllBooksRentedInBetweenDates(data.ElementAt(0).ReaderId, start, end));
        }

        /// <summary>
        /// Tests the get all books rented in between dates fails wrong parameter date end.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksRentedInBetweenDatesFailsWrongParamDateEnd()
        {
            var start = new DateTime(2020, 2, 1);
            var end = DateTime.MinValue.AddDays(1);

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.GetAllBooksRentedInBetweenDates(data.ElementAt(0).ReaderId, start, end));
        }

        /// <summary>
        /// Tests the get all books rented in between dates fails wrong parameter date start is greater.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksRentedInBetweenDatesFailsWrongParamDateStartIsGreater()
        {
            var start = new DateTime(2020, 2, 1);
            var end = new DateTime(2010, 2, 1);

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<ArgumentException>(() => this.service.GetAllBooksRentedInBetweenDates(data.ElementAt(0).ReaderId, start, end));
        }

        /// <summary>
        /// Tests the get all books rented in between dates fails wrong parameter reader identifier.
        /// </summary>
        [TestMethod]
        public void TestGetAllBooksRentedInBetweenDatesFailsWrongParamReaderId()
        {
            var start = new DateTime(2020, 2, 1);
            var end = new DateTime(2021, 2, 1);

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    Id = 1,
                    ReaderId = 1,
                    BookPublisherId = 1,
                    DueDate = new DateTime(2021, 1, 11),
                    ExtensionDays = 0,
                    LoanDate = new DateTime(2020, 12, 30)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ReaderBook>>();
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ReaderBook>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            this.service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LibraryArgumentException>(() => this.service.GetAllBooksRentedInBetweenDates(-1, start, end));
        }
    }
}
