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
    }
}
