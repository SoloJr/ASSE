//---------------------------------------------------------------------
// <copyright file="ReaderServiceTest.cs" company="Transilvania University of Brasov">
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
    using LibraryAdministration.Helper;
    using LibraryAdministration.Startup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;

    /// <summary>
    /// ReaderServiceTest class
    /// </summary>
    [TestClass]
    public class ReaderServiceTest
    {
        /// <summary>
        /// The this.reader
        /// </summary>
        private Reader reader;

        /// <summary>
        /// The this.service
        /// </summary>
        private ReaderService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.reader = new Reader
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

        /// <summary>
        /// Tests the insert reader.
        /// </summary>
        [TestMethod]
        public void TestInsertReader()
        {
            var mockSet = new Mock<DbSet<Reader>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Reader>()).Returns(mockSet.Object);

            this.service = new ReaderService(mockContext.Object);
            var result = this.service.Insert(this.reader);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<Reader>()), Times.Once());
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
        /// Tests the update reader.
        /// </summary>
        [TestMethod]
        public void TestUpdateReader()
        {
            var mockSet = new Mock<DbSet<Reader>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Reader>()).Returns(mockSet.Object);

            this.reader.FirstName = "Update";

            this.service = new ReaderService(mockContext.Object);
            var result = this.service.Update(this.reader);
            try
            {
                mockSet.Verify(m => m.Attach(It.IsAny<Reader>()), Times.Once());
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
        /// Tests the delete reader.
        /// </summary>
        [TestMethod]
        public void TestDeleteReader()
        {
            var mockSet = new Mock<DbSet<Reader>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Reader>()).Returns(mockSet.Object);

            this.service = new ReaderService(mockContext.Object);
            this.service.Delete(this.reader);
            try
            {
                mockSet.Verify(m => m.Remove(It.IsAny<Reader>()), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Tests the get all readers.
        /// </summary>
        [TestMethod]
        public void TestGetAllReaders()
        {
            var data = new List<Reader>
            {
                this.reader,
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

            this.service = new ReaderService(mockContext.Object);

            var pubs = this.service.GetAll();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }

        /// <summary>
        /// Tests the reader is employee success.
        /// </summary>
        [TestMethod]
        public void TestReaderIsEmployeeSuccess()
        {
            var data = new List<Reader>
            {
                this.reader
            }.AsQueryable();

            var employee = new Employee
            {
                Address = this.reader.Address,
                EmployeePersonalInfoId = this.reader.ReaderPersonalInfoId,
                FirstName = this.reader.FirstName,
                LastName = this.reader.LastName,
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

            this.service = new ReaderService(mockContext.Object);

            var pubs = this.service.CheckEmployeeStatus(this.reader.Id, employee.Id);

            Assert.IsTrue(pubs);
        }

        /// <summary>
        /// Tests the reader is employee fail.
        /// </summary>
        [TestMethod]
        public void TestReaderIsEmployeeFail()
        {
            var data = new List<Reader>
            {
                this.reader
            }.AsQueryable();

            var employee = new Employee
            {
                Address = this.reader.Address,
                EmployeePersonalInfoId = 3,
                FirstName = this.reader.FirstName,
                LastName = this.reader.LastName,
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

            this.service = new ReaderService(mockContext.Object);

            var pubs = this.service.CheckEmployeeStatus(this.reader.Id, employee.Id);

            Assert.IsFalse(pubs);
        }

        /// <summary>
        /// Tests the check employee status wrong parameter employee identifier.
        /// </summary>
        [TestMethod]
        public void TestCheckEmployeeStatusWrongParamEmployeeId()
        {
            const int EmployeeId = -1;

            const int ReaderId = 1;

            var context = new Mock<LibraryContext>();

            this.service = new ReaderService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => this.service.CheckEmployeeStatus(ReaderId, EmployeeId));
        }

        /// <summary>
        /// Tests the check employee status wrong parameter reader identifier.
        /// </summary>
        [TestMethod]
        public void TestCheckEmployeeStatusWrongParamReaderId()
        {
            const int EmployeeId = 1;

            const int ReaderId = -1;

            var context = new Mock<LibraryContext>();

            this.service = new ReaderService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => this.service.CheckEmployeeStatus(ReaderId, EmployeeId));
        }

        /// <summary>
        /// Tests the get all employees that have phone number set.
        /// </summary>
        [TestMethod]
        public void TestGetAllEmployeesThatHavePhoneNumberSet()
        {
            var data = new List<Reader>
            {
                this.reader,
                new Reader()
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    ReaderPersonalInfoId = 1,
                    Info = new PersonalInfo
                    {
                        Email = "mircea@google.com",
                        PhoneNumber = ""
                    }
                },
                new Reader()
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    ReaderPersonalInfoId = 1,
                    Info = new PersonalInfo
                    {
                        PhoneNumber = "0752365981",
                        Email = ""
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reader>>();
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Readers).Returns(mockSet.Object);

            this.service = new ReaderService(mockContext.Object);

            var pubs = this.service.GetAllEmployeesThatHavePhoneNumbers();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }

        /// <summary>
        /// Tests the get all employees that have email set.
        /// </summary>
        [TestMethod]
        public void TestGetAllEmployeesThatHaveEmailSet()
        {
            var data = new List<Reader>
            {
                this.reader,
                new Reader
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    ReaderPersonalInfoId = 1,
                    Info = new PersonalInfo
                    {
                        Email = "mircea@google.com",
                        PhoneNumber = ""
                    }
                },
                new Reader
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    ReaderPersonalInfoId = 1,
                    Info = new PersonalInfo
                    {
                        PhoneNumber = "0752365981",
                        Email = ""
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reader>>();
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Readers).Returns(mockSet.Object);

            this.service = new ReaderService(mockContext.Object);

            var pubs = this.service.GetAllEmployeesThatHaveEmails();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 2);
        }

        /// <summary>
        /// Tests the get all employees that have both set.
        /// </summary>
        [TestMethod]
        public void TestGetAllEmployeesThatHaveBothSet()
        {
            var data = new List<Reader>
            {
                this.reader,
                new Reader
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    ReaderPersonalInfoId = 1,
                    Info = new PersonalInfo
                    {
                        Email = "mircea@google.com",
                        PhoneNumber = ""
                    }
                },
                new Reader
                {
                    Address = "str 124521332",
                    FirstName = "aaa",
                    LastName = "bbb",
                    Id = 1,
                    ReaderPersonalInfoId = 1,
                    Info = new PersonalInfo
                    {
                        PhoneNumber = "0752365981",
                        Email = ""
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Reader>>();
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Readers).Returns(mockSet.Object);

            this.service = new ReaderService(mockContext.Object);

            var pubs = this.service.GetAllEmployeesThatHavePhoneNumbers();

            Assert.IsNotNull(pubs);
            Assert.AreEqual(pubs.Count(), 1);
        }
    }
}
