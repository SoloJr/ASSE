using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class ReaderServiceTest
    {
        private Reader _reader;

        private ReaderService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com",
                    Id = 1
                },
                Address = "Str. Drumul cu Plopi Nr. 112",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1,
                ReaderPersonalInfoId = 1
            };
        }


        [TestMethod]
        public void TestInsertReader()
        {
            var mockSet = new Mock<DbSet<Reader>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Reader>()).Returns(mockSet.Object);

            _service = new ReaderService(mockContext.Object);
            var result = _service.Insert(_reader);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Reader>())), Times.Once());
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
        public void TestUpdateReader()
        {
            var mockSet = new Mock<DbSet<Reader>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Reader>()).Returns(mockSet.Object);

            _reader.FirstName = "Update";

            _service = new ReaderService(mockContext.Object);
            var result = _service.Update(_reader);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<Reader>())), Times.Once());
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
        public void TestDeleteReader()
        {
            var mockSet = new Mock<DbSet<Reader>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Reader>()).Returns(mockSet.Object);

            _service = new ReaderService(mockContext.Object);
            _service.Delete(_reader);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<Reader>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetAllReaders()
        {
            var data = new List<Reader>
            {
                _reader,
                new Reader
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    ReaderPersonalInfoId = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reader>>();
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Reader>()).Returns(mockSet.Object);

            _service = new ReaderService(mockContext.Object);

            var pubs = _service.GetAll();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }

        [TestMethod]
        public void TestReaderIsEmployeeSuccess()
        {
            var data = new List<Reader>
            {
                _reader
            }.AsQueryable();

            var employee = new Employee
            {
                Address = _reader.Address,
                EmployeePersonalInfoId = _reader.ReaderPersonalInfoId,
                FirstName = _reader.FirstName,
                LastName = _reader.LastName,
                Id = 1
            };

            var emplData = new List<Employee>
            {
                employee
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reader>>();
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetEmployee = new Mock<DbSet<Employee>>();
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(emplData.Provider);
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(emplData.Expression);
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(emplData.ElementType);
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(emplData.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Readers).Returns(mockSet.Object);
            mockContext.Setup(x => x.Employees).Returns(mockSetEmployee.Object);

            _service = new ReaderService(mockContext.Object);

            var pubs = _service.CheckEmployeeStatus(_reader.Id, employee.Id);

            Assert.IsTrue(pubs);
        }

        [TestMethod]
        public void TestReaderIsEmployeeFail()
        {
            var data = new List<Reader>
            {
                _reader
            }.AsQueryable();

            var employee = new Employee
            {
                Address = _reader.Address,
                EmployeePersonalInfoId = 3,
                FirstName = _reader.FirstName,
                LastName = _reader.LastName,
                Id = 1
            };

            var emplData = new List<Employee>
            {
                employee
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reader>>();
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockSetEmployee = new Mock<DbSet<Employee>>();
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(emplData.Provider);
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(emplData.Expression);
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(emplData.ElementType);
            mockSetEmployee.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(emplData.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Readers).Returns(mockSet.Object);
            mockContext.Setup(x => x.Employees).Returns(mockSetEmployee.Object);

            _service = new ReaderService(mockContext.Object);

            var pubs = _service.CheckEmployeeStatus(_reader.Id, employee.Id);

            Assert.IsFalse(pubs);
        }

        [TestMethod]
        public void TestCheckEmployeeStatusWrongParamEmployeeId()
        {
            const int employeeId = -1;

            const int readerId = 1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckEmployeeStatus(readerId, employeeId));
        }

        [TestMethod]
        public void TestCheckEmployeeStatusWrongParamReaderId()
        {
            const int employeeId = 1;

            const int readerId = -1;

            var context = new Mock<LibraryContext>();

            var service = new ReaderService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckEmployeeStatus(readerId, employeeId));
        }
    }
}
