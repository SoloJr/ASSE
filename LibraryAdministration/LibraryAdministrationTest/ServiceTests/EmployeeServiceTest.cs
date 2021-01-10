//---------------------------------------------------------------------
// <copyright file="EmployeeServiceTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.ServiceTests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using LibraryAdministration.BusinessLayer;
    using LibraryAdministration.DataMapper;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Startup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;

    /// <summary>
    /// EmployeeServiceTest class
    /// </summary>
    [TestClass]
    public class EmployeeServiceTest
    {
        /// <summary>
        /// The employee
        /// </summary>
        private Employee employee;

        /// <summary>
        /// The service
        /// </summary>
        private EmployeeService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.employee = new Employee
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

        /// <summary>
        /// Tests the insert employee.
        /// </summary>
        [TestMethod]
        public void TestInsertEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Employee>()).Returns(mockSet.Object);

            this.service = new EmployeeService(mockContext.Object);
            var result = this.service.Insert(this.employee);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
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
        /// Tests the update employee.
        /// </summary>
        [TestMethod]
        public void TestUpdateEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Employee>()).Returns(mockSet.Object);

            this.employee.FirstName = "Update";

            this.service = new EmployeeService(mockContext.Object);
            var result = this.service.Update(this.employee);
            try
            {
                mockSet.Verify(m => m.Attach(It.IsAny<Employee>()), Times.Once());
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
        /// Tests the delete employee.
        /// </summary>
        [TestMethod]
        public void TestDeleteEmployee()
        {
            var mockSet = new Mock<DbSet<Employee>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Employee>()).Returns(mockSet.Object);

            this.service = new EmployeeService(mockContext.Object);
            this.service.Delete(this.employee);
            try
            {
                mockSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Tests the get all employees.
        /// </summary>
        [TestMethod]
        public void TestGetAllEmployees()
        {
            var data = new List<Employee>
            {
                this.employee,
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

            this.service = new EmployeeService(mockContext.Object);

            var pubs = this.service.GetAll();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }
    }
}
