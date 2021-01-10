using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class ReaderBookServiceTest
    {
        private ReaderBook _readerBook;

        private IReaderBookService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _readerBook = new ReaderBook
            {
                LoanDate = DateTime.Now.AddDays(-3),
                DueDate = DateTime.Now.AddDays(14),
                Id = 1,
                ReaderId = 1,
                BookPublisherId = 1
            };
        }

        [TestMethod]
        public void TestInsertReaderBook()
        {
            var mockSet = new Mock<DbSet<ReaderBook>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<ReaderBook>()).Returns(mockSet.Object);

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.Insert(_readerBook);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<ReaderBook>())), Times.Once());
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
        public void TestGetAllBooksForReader()
        {
            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.GetAllBooksOnLoan(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestSuccessRentBookForNumberOfBooks()
        {
            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckBeforeLoan(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFailRentBookForNumberOfBooks()
        {
            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckBeforeLoan(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestSuccessRentBookForNumberOfBooksToday()
        {
            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckBooksRentedToday(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFailRentBookForNumberOfBooksToday()
        {
            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckBooksRentedToday(1);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the number of books from same domain in given span.
        /// Nu pot imprumuta mai mult de D carti dintr-un acelasi domeniu – de tip frunza sau de nivel superior - in ultimele L luni
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

            _readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckPastLoansForDomains(1, 4);

            Assert.IsFalse(result);
        }

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

            _readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckPastLoansForDomains(1, 4);

            Assert.IsTrue(result);
        }

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

            _readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckPastLoansForDomains(1, 4);

            Assert.IsFalse(result);
        }

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

            _readerBook.BookPublisher = bookPublishers.ElementAt(1);

            var data = new List<ReaderBook>
            {
                _readerBook,
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

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckPastLoansForDomains(1, 4);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFailRentSameBook()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 2,
                BookId = 1
            };

            var bpData = new List<BookPublisher>
            {
                bookPublisher
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    BookPublisher = bpData.ElementAt(0),
                    BookPublisherId = 2,
                    ReaderId = 2,
                    LoanDate = DateTime.Now,
                    Id = 1
                },
                new ReaderBook
                {
                    BookPublisher = bpData.ElementAt(0),
                    BookPublisherId = 2,
                    ReaderId = 1,
                    LoanDate = DateTime.Now,
                    Id = 2
                },
                new ReaderBook
                {
                    BookPublisher = bpData.ElementAt(0),
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
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bpData.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bpData.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bpData.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bpData.GetEnumerator());


            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckSameBookRented(1, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestSuccessRentSameBookAlreadyRent()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 2,
                BookId = 1
            };

            var bpData = new List<BookPublisher>
            {
                bookPublisher
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    BookPublisher = bpData.ElementAt(0),
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
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bpData.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bpData.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bpData.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bpData.GetEnumerator());


            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckSameBookRented(1, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSuccessRentSameBookNeverRent()
        {
            var bookPublisher = new BookPublisher
            {
                Id = 2,
                BookId = 2
            };

            var bpData = new List<BookPublisher>
            {
                bookPublisher
            }.AsQueryable();

            var data = new List<ReaderBook>
            {
                new ReaderBook
                {
                    BookPublisher = bpData.ElementAt(0),
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
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(bpData.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(bpData.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(bpData.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(bpData.GetEnumerator());


            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckSameBookRented(1, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSameAccount()
        {
            var mockContext = new Mock<LibraryContext>();

            _service = new ReaderBookService(mockContext.Object);

            var per = int.Parse(ConfigurationManager.AppSettings["PER"]);
            var nmc = int.Parse(ConfigurationManager.AppSettings["NMC"]);
            var d = int.Parse(ConfigurationManager.AppSettings["D"]);
            var l = int.Parse(ConfigurationManager.AppSettings["L"]);
            var ncz = int.Parse(ConfigurationManager.AppSettings["NCZ"]);
            var delta = int.Parse(ConfigurationManager.AppSettings["DELTA"]);
            var c = int.Parse(ConfigurationManager.AppSettings["C"]);
            var lim = int.Parse(ConfigurationManager.AppSettings["LIM"]);
            var persimp = int.Parse(ConfigurationManager.AppSettings["PERSIMP"]);

            var details = _service.GetRentDetails();

            Assert.IsTrue(details.C == 2 * c);
            Assert.IsTrue(details.NMC == 2 * nmc);
            Assert.IsTrue(details.D == 2 * d);
            Assert.IsTrue(details.LIM == 2 * lim);
            Assert.IsTrue(details.DELTA == delta / 2);
            Assert.IsTrue(details.PER == per / 2);
        }

        [TestMethod]
        public void TestDifferentAccount()
        {
            var mockContext = new Mock<LibraryContext>();

            _service = new ReaderBookService(mockContext.Object, false);

            var per = int.Parse(ConfigurationManager.AppSettings["PER"]);
            var nmc = int.Parse(ConfigurationManager.AppSettings["NMC"]);
            var d = int.Parse(ConfigurationManager.AppSettings["D"]);
            var l = int.Parse(ConfigurationManager.AppSettings["L"]);
            var ncz = int.Parse(ConfigurationManager.AppSettings["NCZ"]);
            var delta = int.Parse(ConfigurationManager.AppSettings["DELTA"]);
            var c = int.Parse(ConfigurationManager.AppSettings["C"]);
            var lim = int.Parse(ConfigurationManager.AppSettings["LIM"]);
            var persimp = int.Parse(ConfigurationManager.AppSettings["PERSIMP"]);

            var details = _service.GetRentDetails();

            Assert.IsTrue(details.C == c);
            Assert.IsTrue(details.NMC == nmc);
            Assert.IsTrue(details.D == d);
            Assert.IsTrue(details.LIM == lim);
            Assert.IsTrue(details.DELTA == delta);
            Assert.IsTrue(details.PER == per);
        }

        [TestMethod]
        public void TestCheckBeforeLoanWrongParam()
        {
            const int id = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckBeforeLoan(id));
        }

        [TestMethod]
        public void TestCheckBooksRentedTodayWrongParam()
        {
            const int id = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckBooksRentedToday(id));
        }

        [TestMethod]
        public void TestCheckPastLoansForDomainsWrongDomainIdParam()
        {
            const int domainId = -1;

            const int readerId = 1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckPastLoansForDomains(readerId, domainId));
        }

        [TestMethod]
        public void TestCheckPastLoansForDomainsWrongReaderIdParam()
        {
            const int domainId = 1;

            const int readerId = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckPastLoansForDomains(readerId, domainId));
        }

        [TestMethod]
        public void TestCheckSameBookRentedWrongReaderIdParam()
        {
            const int bookId = 1;

            const int readerId = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckSameBookRented(bookId, readerId));
        }

        [TestMethod]
        public void TestCheckSameBookRentedWrongBookIdParam()
        {
            const int bookId = -1;

            const int readerId = 1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckSameBookRented(bookId, readerId));
        }

        [TestMethod]
        public void TestGetAllBooksOnLoanWrongParam()
        {
            const int readerId = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderBookService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.GetAllBooksOnLoan(readerId));
        }

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

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckLoanExtension(1, 7);

            Assert.IsTrue(result);
        }

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

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckLoanExtension(1, 7);

            Assert.IsTrue(result);
        }

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

            _service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LoanExtensionException>(() => _service.CheckLoanExtension(1, 7));
        }

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

            _service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LoanExtensionException>(() => _service.CheckLoanExtension(1, 7));
        }

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

            _service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LibraryArgumentException>(() => _service.CheckLoanExtension(-1, 7));
        }

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

            _service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<LibraryArgumentException>(() => _service.CheckLoanExtension(-1, 7));
        }

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

            _service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<ObjectNotFoundException>(() => _service.CheckLoanExtension(100, 7));
        }

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

            _service = new ReaderBookService(mockContext.Object, false);
            Assert.ThrowsException<ObjectNotFoundException>(() => _service.CheckLoanExtension(100, 7));
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchReaderLowerThanTrashold()
        {
            #region mock data

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

            #endregion

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchEmployeeLowerThanTrashold()
        {
            #region mock data

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

            #endregion

            _service = new ReaderBookService(mockContext.Object, true);
            var result = _service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchReaderFailsConstraintForDomain()
        {
            #region mock data

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

            #endregion

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchEmployeeFailsConstraintForDomain()
        {
            #region mock data

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

            #endregion

            _service = new ReaderBookService(mockContext.Object, true);
            var result = _service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchReaderSuccess()
        {
            #region mock data

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

            #endregion

            _service = new ReaderBookService(mockContext.Object, false);
            var result = _service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchEmployeeSuccess()
        {
            #region mock data

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

            #endregion

            _service = new ReaderBookService(mockContext.Object, true);
            var result = _service.CheckMultipleBooksDomainMatch(bookPublishers.Select(x => x.Id).ToList());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchFailsNullParam()
        {
            var mockSet = new Mock<DbSet<ReaderBook>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            _service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LibraryArgumentException>(() => _service.CheckMultipleBooksDomainMatch(null));
        }

        [TestMethod]
        public void TestCheckMultipleBooksDomainMatchFailsWrongParam()
        {
            var wrongParameter = new List<int>();

            var mockSet = new Mock<DbSet<ReaderBook>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.ReaderBooks).Returns(mockSet.Object);

            _service = new ReaderBookService(mockContext.Object);
            Assert.ThrowsException<LibraryArgumentException>(() => _service.CheckMultipleBooksDomainMatch(wrongParameter));
        }
    }
}
