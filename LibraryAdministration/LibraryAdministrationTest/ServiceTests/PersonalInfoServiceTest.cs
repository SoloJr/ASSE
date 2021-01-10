using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
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
    public class PersonalInfoServiceTest
    {
        private PersonalInfo _personalInfo;

        private PersonalInfoService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _personalInfo = new PersonalInfo
            {
                PhoneNumber = "0731233233",
                Email = "mircea.solo1995@gmail.com"
            };
        }

        [TestMethod]
        public void TestInsertPersonalInfo()
        {
            var mockSet = new Mock<DbSet<PersonalInfo>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<PersonalInfo>()).Returns(mockSet.Object);

            _service = new PersonalInfoService(mockContext.Object);
            var result = _service.Insert(_personalInfo);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<PersonalInfo>())), Times.Once());
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
        public void TestUpdatePersonalInfo()
        {
            var mockSet = new Mock<DbSet<PersonalInfo>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<PersonalInfo>()).Returns(mockSet.Object);

            _personalInfo.PhoneNumber = "0731233234";

            _service = new PersonalInfoService(mockContext.Object);
            var result = _service.Update(_personalInfo);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<PersonalInfo>())), Times.Once());
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
        public void TestDeletePersonalInfo()
        {
            var mockSet = new Mock<DbSet<PersonalInfo>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<PersonalInfo>()).Returns(mockSet.Object);

            _service = new PersonalInfoService(mockContext.Object);
            _service.Delete(_personalInfo);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<PersonalInfo>())), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetAllPersonalInfos()
        {
            var data = new List<PersonalInfo>
            {
                _personalInfo,
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

            _service = new PersonalInfoService(mockContext.Object);

            var authors = _service.GetAll();

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 2);
        }
    }
}
