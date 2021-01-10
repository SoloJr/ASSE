//---------------------------------------------------------------------
// <copyright file="PersonalInfoServiceTest.cs" company="Transilvania University of Brasov">
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
    /// PersonalInfoServiceTest class
    /// </summary>
    [TestClass]
    public class PersonalInfoServiceTest
    {
        /// <summary>
        /// The personal information
        /// </summary>
        private PersonalInfo personalInfo;

        /// <summary>
        /// The service
        /// </summary>
        private PersonalInfoService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.personalInfo = new PersonalInfo
            {
                PhoneNumber = "0731233233",
                Email = "mircea.solo1995@gmail.com"
            };
        }

        /// <summary>
        /// Tests the insert personal information.
        /// </summary>
        [TestMethod]
        public void TestInsertPersonalInfo()
        {
            var mockSet = new Mock<DbSet<PersonalInfo>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<PersonalInfo>()).Returns(mockSet.Object);

            this.service = new PersonalInfoService(mockContext.Object);
            var result = this.service.Insert(this.personalInfo);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<PersonalInfo>()), Times.Once());
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
        /// Tests the update personal information.
        /// </summary>
        [TestMethod]
        public void TestUpdatePersonalInfo()
        {
            var mockSet = new Mock<DbSet<PersonalInfo>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<PersonalInfo>()).Returns(mockSet.Object);

            this.personalInfo.PhoneNumber = "0731233234";

            this.service = new PersonalInfoService(mockContext.Object);
            var result = this.service.Update(this.personalInfo);
            try
            {
                mockSet.Verify(m => m.Attach(It.IsAny<PersonalInfo>()), Times.Once());
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
        /// Tests the delete personal information.
        /// </summary>
        [TestMethod]
        public void TestDeletePersonalInfo()
        {
            var mockSet = new Mock<DbSet<PersonalInfo>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<PersonalInfo>()).Returns(mockSet.Object);

            this.service = new PersonalInfoService(mockContext.Object);
            this.service.Delete(this.personalInfo);
            try
            {
                mockSet.Verify(m => m.Remove(It.IsAny<PersonalInfo>()), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Tests the get all personal info.
        /// </summary>
        [TestMethod]
        public void TestGetAllPersonalInfos()
        {
            var data = new List<PersonalInfo>
            {
                this.personalInfo,
                new PersonalInfo
                {
                    Email = "cevaaa@mail.com",
                    PhoneNumber = "0722662278"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<PersonalInfo>>();
            mockSet.As<IQueryable<PersonalInfo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<PersonalInfo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PersonalInfo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PersonalInfo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<PersonalInfo>()).Returns(mockSet.Object);

            this.service = new PersonalInfoService(mockContext.Object);

            var authors = this.service.GetAll();

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 2);
        }
    }
}
