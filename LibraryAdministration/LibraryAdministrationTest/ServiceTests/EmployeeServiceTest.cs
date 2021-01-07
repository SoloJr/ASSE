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
    public class EmployeeServiceTest
    {
        private Employee _employee;

        private EmployeeService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112",
                FirstName = "Mircea",
                LastName = "Solovastru"
            };
        }

        [TestMethod]
        public void TestInsertEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Employee>()).Returns(mockSet.Object);

            _service = new EmployeeService(mockContext.Object);
            var result = _service.Insert(_employee);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Employee>())), Times.Once());
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
        public void TestUpdateEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Employee>()).Returns(mockSet.Object);

            _employee.FirstName = "Update";

            _service = new EmployeeService(mockContext.Object);
            var result = _service.Update(_employee);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<Employee>())), Times.Once());
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
        public void TestDeleteEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Employee>()).Returns(mockSet.Object);

            _service = new EmployeeService(mockContext.Object);
            _service.Delete(_employee);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<Employee>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetAllEmployees()
        {
            var data = new List<Employee>
            {
                _employee,
                new Employee
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    EmployeePersonalInfoId = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Employee>()).Returns(mockSet.Object);

            _service = new EmployeeService(mockContext.Object);

            var pubs = _service.GetAll();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }
    }
}
