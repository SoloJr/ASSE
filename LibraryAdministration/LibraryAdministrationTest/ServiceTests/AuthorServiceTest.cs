﻿using System;
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
    public class AuthorServiceTest
    {
        private Author _author;

        private IAuthorService _service;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _author = new Author
            {
                Name = "Mark Manson",
                BirthDate = new DateTime(1970, 1, 1),
                Country = "USA",
                Id = 1
            };
        }

        [TestMethod]
        public void TestInsertAuthor()
        {
            var mockSet = new Mock<DbSet<Author>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);

            _service = new AuthorService(mockContext.Object);
            var result = _service.Insert(_author);
            try
            {
                mockSet.Verify(m => m.Add((It.IsAny<Author>())), Times.Once());
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
        public void TestUpdateAuthor()
        {
            var mockSet = new Mock<DbSet<Author>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);

            _author.DeathDate = DateTime.Now;

            _service = new AuthorService(mockContext.Object);
            var result = _service.Update(_author);
            try
            {
                mockSet.Verify(m => m.Attach((It.IsAny<Author>())), Times.Once());
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
        public void TestDeleteAuthor()
        {
            var mockSet = new Mock<DbSet<Author>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<Author>()).Returns(mockSet.Object);

            _service = new AuthorService(mockContext.Object);
            _service.Delete(_author);
            try
            {
                mockSet.Verify(m => m.Remove((It.IsAny<Author>())), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
