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
    }
}
