using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private IDbContextGenerator contextGenerator;

        private Author _author;

        private IKernel _kernel;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _kernel = Injector.Kernel;
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
            var service = _kernel.Get<IAuthorService>();

            var result = service.Insert(_author);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateAuthor()
        {
            var service = _kernel.Get<IAuthorService>();

            var result = service.Update(_author);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteAuthor()
        {
            var service = _kernel.Get<IAuthorService>();

            Assert.ThrowsException<DeleteItemException>(() => service.Delete(_author));
        }

        [TestMethod]
        public void TestGetAuthorWithBooks()
        {
            var service = _kernel.Get<IAuthorService>();

            var result = service.GetAuthorsWithBooks();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateBlog_saves_a_blog_via_context()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(m => m.Books).Returns(mockSet.Object);
            var service = _kernel.Get<IAuthorService>();

            mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
