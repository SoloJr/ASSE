//---------------------------------------------------------------------
// <copyright file="AuthorServiceTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.ServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using LibraryAdministration.BusinessLayer;
    using LibraryAdministration.DataMapper;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Interfaces.Business;
    using LibraryAdministration.Startup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;

    /// <summary>
    /// AuthorServiceTest class
    /// </summary>
    [TestClass]
    public class AuthorServiceTest
    {
        /// <summary>
        /// The author
        /// </summary>
        private Author author;

        /// <summary>
        /// The service
        /// </summary>
        private IAuthorService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.author = new Author
            {
                Name = "Mark Manson",
                BirthDate = new DateTime(1970, 1, 1),
                Country = "USA",
                Id = 1
            };
        }

        /// <summary>
        /// Tests the insert author.
        /// </summary>
        [TestMethod]
        public void TestInsertAuthor()
        {
            var mockSet = new Mock<DbSet<Author>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);

            this.service = new AuthorService(mockContext.Object);
            var result = this.service.Insert(this.author);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<Author>()), Times.Once());
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
        /// Tests the update author.
        /// </summary>
        [TestMethod]
        public void TestUpdateAuthor()
        {
            var mockSet = new Mock<DbSet<Author>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);

            this.author.DeathDate = DateTime.Now;

            this.service = new AuthorService(mockContext.Object);
            var result = this.service.Update(this.author);
            try
            {
                mockSet.Verify(m => m.Attach(It.IsAny<Author>()), Times.Once());
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
        /// Tests the delete author.
        /// </summary>
        [TestMethod]
        public void TestDeleteAuthor()
        {
            var mockSet = new Mock<DbSet<Author>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);

            this.service = new AuthorService(mockContext.Object);
            this.service.Delete(this.author);
            try
            {
                mockSet.Verify(m => m.Remove(It.IsAny<Author>()), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Tests the get all authors.
        /// </summary>
        [TestMethod]
        public void TestGetAllAuthors()
        {
            var data = new List<Author>
            {
                this.author,
                new Author
                {
                    Name = "Mark Manson(2)",
                    BirthDate = new DateTime(1970, 1, 1),
                    Country = "USA",
                    Id = 2
                },
                new Author
                {
                    Name = "Mark Manson(3)",
                    BirthDate = new DateTime(1970, 1, 1),
                    Country = "USA",
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Author>>();
            mockSet.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);

            this.service = new AuthorService(mockContext.Object);

            var authors = this.service.GetAll();

            Assert.IsNotNull(authors);
            Assert.AreEqual(authors.Count(), 3);
        }

        /// <summary>
        /// Tests the get author by identifier.
        /// </summary>
        [TestMethod]
        public void TestGetAuthorById()
        {
            var data = new List<Author>
            {
                this.author,
                new Author
                {
                    Name = "Mark Manson(2)",
                    BirthDate = new DateTime(1970, 1, 1),
                    Country = "USA",
                    Id = 2
                },
                new Author
                {
                    Name = "Mark Manson(3)",
                    BirthDate = new DateTime(1970, 1, 1),
                    Country = "USA",
                    Id = 3
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Author>>();
            mockSet.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Authors).Returns(mockSet.Object);

            this.service = new AuthorService(mockContext.Object);

            var authors = this.service.GetById(2);

            Assert.IsNull(authors);
        }
    }
}
