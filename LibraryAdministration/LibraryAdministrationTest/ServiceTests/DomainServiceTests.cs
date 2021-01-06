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
    public class DomainServiceTests
    {
        private Domain _domain;

        private IDomainService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _domain = new Domain
            {
                Id = 1,
                Name = "Base Domain",
                EntireDomainId = null,
                ParentId = null
            };
        }

        [TestMethod]
        public void TestInsertDomain()
        {
            var mockSet = new Mock<DbSet<Domain>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Domain>()).Returns(mockSet.Object);

            _service = new DomainService(mockContext.Object);
            var result = _service.Insert(_domain);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Domain>())), Times.Once());
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
        public void TestUpdateDomain()
        {
            var mockSet = new Mock<DbSet<Domain>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Domain>()).Returns(mockSet.Object);

            _domain.Name = "Update";

            _service = new DomainService(mockContext.Object);
            var result = _service.Update(_domain);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<Domain>())), Times.Once());
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
        public void TestDeleteDomain()
        {
            var mockSet = new Mock<DbSet<Domain>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Domain>()).Returns(mockSet.Object);

            _service = new DomainService(mockContext.Object);
            _service.Delete(_domain);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<Domain>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetAllDomains()
        {
            var data = new List<Domain>
            {
                _domain,
                new Domain
                {
                    Id = 2,
                    Name = "Other Domain",
                    EntireDomainId = null,
                    ParentId = null
                },
                new Domain
                {
                    Id = 3,
                    Name = "Derived Domain",
                    EntireDomainId = 1,
                    ParentId = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Domain>>();
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Domain>()).Returns(mockSet.Object);

            _service = new DomainService(mockContext.Object);

            var authors = _service.GetAll();

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 3);
        }

        [TestMethod]
        public void TestGetAllParentDomains()
        {
            var idToCheck = 3;
            var data = new List<Domain>
            {
                _domain,
                new Domain
                {
                    Id = 2,
                    Name = "Other Domain",
                    EntireDomainId = null,
                    ParentId = 1
                },
                new Domain
                {
                    Id = 3,
                    Name = "Derived Domain",
                    EntireDomainId = 1,
                    ParentId = 2
                },
                new Domain
                {
                    Id = 3,
                    Name = "Derived Domain",
                    EntireDomainId = null,
                    ParentId = null
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Domain>>();
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSet.Object);

            _service = new DomainService(mockContext.Object);

            var authors = _service.GetAllParentDomains(idToCheck);

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 2);
        }

        [TestMethod]
        public void TestCheckDomainsWhenAddingBook()
        {
            var data = new List<Domain>
            {
                _domain,
                new Domain
                {
                    Id = 2,
                    Name = "Other Domain",
                    EntireDomainId = null,
                    ParentId = null
                },
                new Domain
                {
                    Id = 3,
                    Name = "Derived Domain",
                    EntireDomainId = null,
                    ParentId = null
                },
                new Domain
                {
                    Id = 4,
                    Name = "Derived Domain",
                    EntireDomainId = null,
                    ParentId = null
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Domain>>();
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSet.Object);

            _service = new DomainService(mockContext.Object);

            var result = _service.CheckDomainConstraint(data.ToList());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckDomainsWhenAddingBookWithParentIdsSuccess()
        {
            var data = new List<Domain>
            {
                _domain,
                new Domain
                {
                    Id = 2,
                    Name = "Other Domain",
                    EntireDomainId = null,
                    ParentId = 5
                },
                new Domain
                {
                    Id = 3,
                    Name = "Derived Domain",
                    EntireDomainId = null,
                    ParentId = null
                },
                new Domain
                {
                    Id = 4,
                    Name = "Derived Domain",
                    EntireDomainId = null,
                    ParentId = 6
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Domain>>();
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSet.Object);

            _service = new DomainService(mockContext.Object);

            var result = _service.CheckDomainConstraint(data.ToList());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckDomainsWhenAddingBookWithParentIdsFail()
        {
            var data = new List<Domain>
            {
                _domain,
                new Domain
                {
                    Id = 2,
                    Name = "Other Domain",
                    EntireDomainId = null,
                    ParentId = 1
                },
                new Domain
                {
                    Id = 3,
                    Name = "Derived Domain",
                    EntireDomainId = null,
                    ParentId = null
                },
                new Domain
                {
                    Id = 4,
                    Name = "Derived Domain",
                    EntireDomainId = null,
                    ParentId = 2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Domain>>();
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Domains).Returns(mockSet.Object);

            _service = new DomainService(mockContext.Object);

            var result = _service.CheckDomainConstraint(data.ToList());

            Assert.IsFalse(result);
        }
    }
}
