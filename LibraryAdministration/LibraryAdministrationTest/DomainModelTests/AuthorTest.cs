using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class AuthorTest
    {
        private AuthorValidator _validator;

        [TestInitialize]
        public void Init()
        {
            _validator = new AuthorValidator(); ;
        }

        [TestMethod]
        public void TestCreateReaderSuccess()
        {
            var author = new Author
            {
                Name = "Ion Creanga",
                BirthDate = new DateTime(1850, 1, 1),
                Country = "Romania"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderFailNoName()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderFailNoCountry()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Name = "Ion Creanga"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderFailNoBirthDate()
        {
            var author = new Author
            {
                Country = "Romania",
                Name = "Ion Creanga"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderFailNameTooShort()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania",
                Name = "IC"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderFailNameTooLong()
        {
            var author = new Author
            {
                BirthDate = DateTime.MinValue,
                Country = "Romania",
                Name = "Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania Ion Creanga Romania"
            };

            var result = _validator.Validate(author);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
