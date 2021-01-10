//---------------------------------------------------------------------
// <copyright file="DomainServiceTests.cs" company="Transilvania University of Brasov">
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
    using LibraryAdministration.Interfaces.Business;
    using LibraryAdministration.Startup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;

    /// <summary>
    /// DomainServiceTests class
    /// </summary>
    [TestClass]
    public class DomainServiceTests
    {
        /// <summary>
        /// The domain
        /// </summary>
        private Domain domain;

        /// <summary>
        /// The service
        /// </summary>
        private IDomainService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.domain = new Domain
            {
                Id = 1,
                Name = "Base Domain",
                EntireDomainId = null,
                ParentId = null
            };
        }

        /// <summary>
        /// Tests the insert domain.
        /// </summary>
        [TestMethod]
        public void TestInsertDomain()
        {
            var mockSet = new Mock<DbSet<Domain>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Domain>()).Returns(mockSet.Object);

            this.service = new DomainService(mockContext.Object);
            var result = this.service.Insert(this.domain);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<Domain>()), Times.Once());
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
        /// Tests the update domain.
        /// </summary>
        [TestMethod]
        public void TestUpdateDomain()
        {
            var mockSet = new Mock<DbSet<Domain>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Domain>()).Returns(mockSet.Object);

            this.domain.Name = "Update";

            this.service = new DomainService(mockContext.Object);
            var result = this.service.Update(this.domain);
            try
            {
                mockSet.Verify(m => m.Attach(It.IsAny<Domain>()), Times.Once());
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
        /// Tests the delete domain.
        /// </summary>
        [TestMethod]
        public void TestDeleteDomain()
        {
            var mockSet = new Mock<DbSet<Domain>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Domain>()).Returns(mockSet.Object);

            this.service = new DomainService(mockContext.Object);
            this.service.Delete(this.domain);
            try
            {
                mockSet.Verify(m => m.Remove(It.IsAny<Domain>()), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Tests the get all domains.
        /// </summary>
        [TestMethod]
        public void TestGetAllDomains()
        {
            var data = new List<Domain>
            {
                this.domain,
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

            this.service = new DomainService(mockContext.Object);

            var authors = this.service.GetAll();

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 3);
        }

        /// <summary>
        /// Tests the get all parent domains.
        /// </summary>
        [TestMethod]
        public void TestGetAllParentDomains()
        {
            var idToCheck = 3;
            var data = new List<Domain>
            {
                this.domain,
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

            this.service = new DomainService(mockContext.Object);

            var authors = this.service.GetAllParentDomains(idToCheck);

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 2);
        }

        /// <summary>
        /// Tests the check domains when adding book.
        /// </summary>
        [TestMethod]
        public void TestCheckDomainsWhenAddingBook()
        {
            var data = new List<Domain>
            {
                this.domain,
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

            this.service = new DomainService(mockContext.Object);

            var result = this.service.CheckDomainConstraint(data.ToList());

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check domains when adding book with parent ids success.
        /// </summary>
        [TestMethod]
        public void TestCheckDomainsWhenAddingBookWithParentIdsSuccess()
        {
            var data = new List<Domain>
            {
                this.domain,
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

            this.service = new DomainService(mockContext.Object);

            var result = this.service.CheckDomainConstraint(data.ToList());

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check domains when adding book with parent ids fail.
        /// </summary>
        [TestMethod]
        public void TestCheckDomainsWhenAddingBookWithParentIdsFail()
        {
            var data = new List<Domain>
            {
                this.domain,
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

            this.service = new DomainService(mockContext.Object);

            var result = this.service.CheckDomainConstraint(data.ToList());

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the check domain constraint wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckDomainConstraintWrongParam()
        {
            var context = new Mock<LibraryContext>();

            var service = new DomainService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckDomainConstraint(null));
        }

        /// <summary>
        /// Tests the get all parent domains wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestGetAllParentDomainsWrongParam()
        {
            const int Id = -1;

            var context = new Mock<LibraryContext>();

            var service = new DomainService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.GetAllParentDomains(Id));
        }
    }
}
