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
using System.Data.Entity.Core;
using System.Linq;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class BookPublisherServiceTest
    {
        private BookPublisherService _service;

        private BookPublisher _bookPublisher;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _bookPublisher = new BookPublisher
            {
                BookId = 1,
                RentCount = 10,
                Pages = 200,
                PublisherId = 1,
                ReleaseDate = DateTime.MaxValue,
                Type = BookType.Ebook,
                ForLecture = 10,
                ForRent = 4
            };
        }

        [TestMethod]
        public void TestInsertBookPublisher()
        {
            var mockSet = new Mock<DbSet<BookPublisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            _service = new BookPublisherService(mockContext.Object);
            var result = _service.Insert(_bookPublisher);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<BookPublisher>())), Times.Once());
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
        public void TestUpdateBookPublisher()
        {
            var mockSet = new Mock<DbSet<BookPublisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            _bookPublisher.ReleaseDate = DateTime.Now;

            _service = new BookPublisherService(mockContext.Object);
            var result = _service.Update(_bookPublisher);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<BookPublisher>())), Times.Once());
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
        public void TestDeleteBookPublisher()
        {
            var mockSet = new Mock<DbSet<BookPublisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            _service = new BookPublisherService(mockContext.Object);
            _service.Delete(_bookPublisher);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<BookPublisher>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetAllBookPublishers()
        {
            var data = new List<BookPublisher>
            {
                _bookPublisher,
                new BookPublisher
                {
                    BookId = 1,
                    RentCount = 10,
                    Pages = 200,
                    PublisherId = 2,
                    ReleaseDate = DateTime.Now,
                    Type = BookType.Ebook
                },
                new BookPublisher
                {
                    BookId = 1,
                    RentCount = 10,
                    Pages = 200,
                    PublisherId = 3,
                    ReleaseDate = DateTime.Now,
                    Type = BookType.Ebook
                },
                new BookPublisher
                {
                    BookId = 2,
                    RentCount = 10,
                    Pages = 200,
                    PublisherId = 1,
                    ReleaseDate = DateTime.Now,
                    Type = BookType.Ebook
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<BookPublisher>>();
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            _service = new BookPublisherService(mockContext.Object);

            var bps = _service.GetAll();

            Assert.IsNotNull(bps);
            Assert.AreEqual(bps.Count(), 4);
        }

        [TestMethod]
        public void TestGetAllVersionsOfBookSuccess()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 1
                },
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 2,
                },
                new BookPublisher
                {
                    BookId = 2,
                    PublisherId = 1
                }
            };

            var data = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Name = "Test Book",
                    Publishers = publisherData
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Books).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new BookPublisherService(mockContext.Object);

            var result = _service.GetAllEditionsOfBook(data.ElementAt(0).Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        [TestMethod]
        public void TestGetAllVersionsOfBookFailsWrongBookId()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 1
                },
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 2,
                },
                new BookPublisher
                {
                    BookId = 2,
                    PublisherId = 1
                }
            };

            var data = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Name = "Test Book",
                    Publishers = publisherData
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Books).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new BookPublisherService(mockContext.Object);

            Assert.ThrowsException<ObjectNotFoundException>(() => _service.GetAllEditionsOfBook(123));
        }

        [TestMethod]
        public void TestGetAllVersionsOfBookFailsWrongParam()
        {
            var wrongParam = -1;

            var context = new Mock<LibraryContext>();

            var service = new BookPublisherService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.GetAllEditionsOfBook(wrongParam));
        }

        [TestMethod]
        public void TestCheckBookDetailsForAvailabilitySuccess()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 20,
                    RentCount = 1
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new BookPublisherService(mockContext.Object);

            var result = _service.CheckBookDetailsForAvailability(publisherData[0].Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityWrongBookId()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 20,
                    RentCount = 1
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new BookPublisherService(mockContext.Object);

            Assert.ThrowsException<ObjectNotFoundException>(() => _service.CheckBookDetailsForAvailability(123));
        }

        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityFailsWrongParam()
        {
            var wrongParam = -1;

            var context = new Mock<LibraryContext>();

            var service = new BookPublisherService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckBookDetailsForAvailability(wrongParam));
        }

        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityFailNoRent()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 0,
                    RentCount = 0
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new BookPublisherService(mockContext.Object);

            var result = _service.CheckBookDetailsForAvailability(publisherData[0].Id);

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityFailAlreadyTooManyRented()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 20,
                    RentCount = 18
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            _service = new BookPublisherService(mockContext.Object);

            var result = _service.CheckBookDetailsForAvailability(publisherData[0].Id);

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
    }
}
