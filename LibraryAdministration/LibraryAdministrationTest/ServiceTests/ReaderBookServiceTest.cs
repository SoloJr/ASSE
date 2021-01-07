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

            _service = new ReaderBookService(mockContext.Object);
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

            _service = new ReaderBookService(mockContext.Object);
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
        public void TestNumberOfBooksFromSameDomainInGivenSpan()
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

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckPastLoansForDomains(1, 4);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestNumberOfBooksFromSameDomainInGivenSpanSuccess()
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

            _service = new ReaderBookService(mockContext.Object);
            var result = _service.CheckPastLoansForDomains(1, 4);

            Assert.IsTrue(result);
        }
    }
}
