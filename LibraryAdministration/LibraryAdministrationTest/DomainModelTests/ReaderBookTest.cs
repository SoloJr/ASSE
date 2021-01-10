using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class ReaderBookTest
    {
        private ReaderBookValidator _readerBookValidator;

        [TestInitialize]
        public void Init()
        {
            _readerBookValidator = new ReaderBookValidator();
        }

        [TestMethod]
        public void TestCreateSuccessReaderBook()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                BookPublisherId = book.Id,
                ReaderId = reader.Id,
                Id = 1,
                ExtensionDays = 0
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderSuccessBookWithObjects()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new BookPublisher()
            {
                ForRent = 120,
                RentCount = 100,
                Pages = 240,
                PublisherId = 1,
                BookId = 1,
                ReleaseDate = DateTime.Now,
                Type = BookType.Hardback,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                BookPublisherId = book.Id,
                ReaderId = reader.Id,
                Id = 1,
                BookPublisher = book,
                Reader = reader,
                ExtensionDays = 0
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsNotNull(readerBook.BookPublisher);
            Assert.IsNotNull(readerBook.Reader);
        }

        [TestMethod]
        public void TestCreateReaderBookFailsDueDate()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                BookPublisherId = book.Id,
                ReaderId = reader.Id,
                Id = 1
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderBookFailsLoanDate()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                DueDate = DateTime.Now,
                BookPublisherId = book.Id,
                ReaderId = reader.Id,
                Id = 1
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderBookFailsNoPublisherId()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                DueDate = DateTime.Now,
                LoanDate = DateTime.Now.AddDays(14),
                ReaderId = reader.Id,
                Id = 1
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderBookFailsNoReaderId()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                DueDate = DateTime.Now,
                LoanDate = DateTime.Now.AddDays(14),
                BookPublisherId = int.MinValue,
                Id = 1
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderBookSuccessWithNoLoanReturnDate()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                DueDate = DateTime.Now.AddDays(14),
                LoanDate = DateTime.Now,
                BookPublisherId = int.MinValue,
                ExtensionDays = 0,
                ReaderId = 1,
                Id = 1
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderBookFailsDueDateNotFourteenDays()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(4),
                ExtensionDays = 0,
                BookPublisherId = book.Id,
                ReaderId = reader.Id,
                Id = 1
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreateReaderBookFailsExtensionDaysNotZero()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "0",
                FirstName = "Mircea",
                LastName = "Solovastru",
                Id = 1
            };

            var book = new Book
            {
                Name = "Arta Subtila a Nepasarii",
                Language = "Romana",
                Year = 2017,
                Id = 1
            };

            var readerBook = new ReaderBook
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                ExtensionDays = 120,
                BookPublisherId = book.Id,
                ReaderId = reader.Id,
                Id = 1
            };

            var result = _readerBookValidator.Validate(readerBook);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
