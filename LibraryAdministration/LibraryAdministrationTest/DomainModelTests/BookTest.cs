using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class BookTest
    {
        private Mock<IBookService> _bookServiceMock;

        private BookValidator _bookValidator;

        [TestInitialize]
        public void Init()
        {
            _bookServiceMock = new Mock<IBookService>();
            _bookValidator = new BookValidator();
        }

        [TestMethod]
        public void TestCreateBookWithoutAuthorsPublishersDomains()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie",
                Year = 1885
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, true);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestMethod]
        public void TestCreateBookWithoutName()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Year = 1885
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        [TestMethod]
        public void TestCreateBookWithoutYear()
        {
            var book = new Book()
            {
                Language = "Romanian",
                Name = "Amintiri din Copilarie"
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        [TestMethod]
        public void TestCreateBookWithoutLanguage()
        {
            var book = new Book()
            {
                Year = 1885,
                Name = "Amintiri din Copilarie"
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("must not be empty")));
        }

        [TestMethod]
        public void TestCreateBookWithTooManyDomains()
        {
            var domainOne = new Domain
            {
                Name = "Beletristica",
                ParentId = null,
                Id = 1
            };

            var domainTwo = new Domain
            {
                Name = "Stiinta",
                ParentId = null,
                Id = 2
            };

            var book = new Book()
            {
                Language = "Romana",
                Year = 1885,
                Name = "Amintiri din Copilarie",
                Domains = new List<Domain>
                {
                    domainOne,
                    domainTwo
                }
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("Too many domains")));
        }

        [TestMethod]
        public void TestCreateBookWithManyDomainsOfSameKind()
        {
            var domainOne = new Domain
            {
                Name = "Beletristica",
                ParentId = null,
                Id = 1
            };

            var domainTwo = new Domain
            {
                Name = "Stiinta",
                ParentId = 1,
                Id = 2
            };

            var book = new Book()
            {
                Language = "Romana",
                Year = 1885,
                Name = "Amintiri din Copilarie",
                Domains = new List<Domain>
                {
                    domainOne,
                    domainTwo
                }
            };

            var result = _bookValidator.Validate(book);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsValid, false);
            Assert.IsTrue(result.Errors.Any(x => x.ErrorMessage.Contains("Too many domains")));
        }
    }
}
