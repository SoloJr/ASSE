//---------------------------------------------------------------------
// <copyright file="BookPublisherServiceTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.ServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Linq;
    using LibraryAdministration.BusinessLayer;
    using LibraryAdministration.DataMapper;
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Helper;
    using LibraryAdministration.Startup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;

    /// <summary>
    /// BookPublisherServiceTest class
    /// </summary>
    [TestClass]
    public class BookPublisherServiceTest
    {
        /// <summary>
        /// The service
        /// </summary>
        private BookPublisherService service;

        /// <summary>
        /// The book publisher
        /// </summary>
        private BookPublisher bookPublisher;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            this.bookPublisher = new BookPublisher
            {
                BookId = 1,
                RentCount = 10,
                Pages = 200,
                PublisherId = 1,
                ReleaseDate = DateTime.MaxValue,
                Type = BookType.Ebook,
                ForLecture = 10,
                ForRent = 4
            };
        }

        /// <summary>
        /// Tests the insert book publisher.
        /// </summary>
        [TestMethod]
        public void TestInsertBookPublisher()
        {
            var mockSet = new Mock<DbSet<BookPublisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            this.service = new BookPublisherService(mockContext.Object);
            var result = this.service.Insert(this.bookPublisher);
            try
            {
                mockSet.Verify(m => m.Add(It.IsAny<BookPublisher>()), Times.Once());
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
        /// Tests the update book publisher.
        /// </summary>
        [TestMethod]
        public void TestUpdateBookPublisher()
        {
            var mockSet = new Mock<DbSet<BookPublisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            this.bookPublisher.ReleaseDate = DateTime.Now;

            this.service = new BookPublisherService(mockContext.Object);
            var result = this.service.Update(this.bookPublisher);
            try
            {
                mockSet.Verify(m => m.Attach(It.IsAny<BookPublisher>()), Times.Once());
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
        /// Tests the delete book publisher.
        /// </summary>
        [TestMethod]
        public void TestDeleteBookPublisher()
        {
            var mockSet = new Mock<DbSet<BookPublisher>>();

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            this.service = new BookPublisherService(mockContext.Object);
            this.service.Delete(this.bookPublisher);
            try
            {
                mockSet.Verify(m => m.Remove(It.IsAny<BookPublisher>()), Times.Once());
                mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (MockException e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Tests the get all book publishers.
        /// </summary>
        [TestMethod]
        public void TestGetAllBookPublishers()
        {
            var data = new List<BookPublisher>
            {
                this.bookPublisher,
                new BookPublisher
                {
                    BookId = 1,
                    RentCount = 10,
                    Pages = 200,
                    PublisherId = 2,
                    ReleaseDate = DateTime.Now,
                    Type = BookType.Ebook
                },
                new BookPublisher
                {
                    BookId = 1,
                    RentCount = 10,
                    Pages = 200,
                    PublisherId = 3,
                    ReleaseDate = DateTime.Now,
                    Type = BookType.Ebook
                },
                new BookPublisher
                {
                    BookId = 2,
                    RentCount = 10,
                    Pages = 200,
                    PublisherId = 1,
                    ReleaseDate = DateTime.Now,
                    Type = BookType.Ebook
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<BookPublisher>>();
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Set<BookPublisher>()).Returns(mockSet.Object);

            this.service = new BookPublisherService(mockContext.Object);

            var bps = this.service.GetAll();

            Assert.IsNotNull(bps);
            Assert.AreEqual(bps.Count(), 4);
        }

        /// <summary>
        /// Tests the get all versions of book success.
        /// </summary>
        [TestMethod]
        public void TestGetAllVersionsOfBookSuccess()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 1
                },
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 2,
                },
                new BookPublisher
                {
                    BookId = 2,
                    PublisherId = 1
                }
            };

            var data = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Name = "Test Book",
                    Publishers = publisherData
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Books).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new BookPublisherService(mockContext.Object);

            var result = this.service.GetAllEditionsOfBook(data.ElementAt(0).Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        /// <summary>
        /// Tests the get all versions of book fails wrong book identifier.
        /// </summary>
        [TestMethod]
        public void TestGetAllVersionsOfBookFailsWrongBookId()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 1
                },
                new BookPublisher
                {
                    BookId = 1,
                    PublisherId = 2,
                },
                new BookPublisher
                {
                    BookId = 2,
                    PublisherId = 1
                }
            };

            var data = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Name = "Test Book",
                    Publishers = publisherData
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.Books).Returns(mockSet.Object);
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new BookPublisherService(mockContext.Object);

            Assert.ThrowsException<ObjectNotFoundException>(() => this.service.GetAllEditionsOfBook(123));
        }

        /// <summary>
        /// Tests the get all versions of book fails wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestGetAllVersionsOfBookFailsWrongParam()
        {
            var wrongParam = -1;

            var context = new Mock<LibraryContext>();

            var service = new BookPublisherService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.GetAllEditionsOfBook(wrongParam));
        }

        /// <summary>
        /// Tests the check book details for availability success.
        /// </summary>
        [TestMethod]
        public void TestCheckBookDetailsForAvailabilitySuccess()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 20,
                    RentCount = 1
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new BookPublisherService(mockContext.Object);

            var result = this.service.CheckBookDetailsForAvailability(publisherData[0].Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the check book details for availability wrong book identifier.
        /// </summary>
        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityWrongBookId()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 20,
                    RentCount = 1
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new BookPublisherService(mockContext.Object);

            Assert.ThrowsException<ObjectNotFoundException>(() => this.service.CheckBookDetailsForAvailability(123));
        }

        /// <summary>
        /// Tests the check book details for availability fails wrong parameter.
        /// </summary>
        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityFailsWrongParam()
        {
            var wrongParam = -1;

            var context = new Mock<LibraryContext>();

            var service = new BookPublisherService(context.Object);

            Assert.ThrowsException<LibraryArgumentException>(() => service.CheckBookDetailsForAvailability(wrongParam));
        }

        /// <summary>
        /// Tests the check book details for availability fail no rent.
        /// </summary>
        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityFailNoRent()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 0,
                    RentCount = 0
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new BookPublisherService(mockContext.Object);

            var result = this.service.CheckBookDetailsForAvailability(publisherData[0].Id);

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the check book details for availability fail already too many rented.
        /// </summary>
        [TestMethod]
        public void TestCheckBookDetailsForAvailabilityFailAlreadyTooManyRented()
        {
            var publisherData = new List<BookPublisher>
            {
                new BookPublisher
                {
                    Id = 1,
                    BookId = 1,
                    PublisherId = 1,
                    ForLecture = 20,
                    ForRent = 20,
                    RentCount = 18
                }
            };

            var publisherDataQueryable = publisherData.AsQueryable();

            var mockSetBp = new Mock<DbSet<BookPublisher>>();
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Provider).Returns(publisherDataQueryable.Provider);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.Expression).Returns(publisherDataQueryable.Expression);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.ElementType).Returns(publisherDataQueryable.ElementType);
            mockSetBp.As<IQueryable<BookPublisher>>().Setup(m => m.GetEnumerator()).Returns(publisherDataQueryable.GetEnumerator());

            var mockContext = new Mock<LibraryContext>();
            mockContext.Setup(x => x.BookPublisher).Returns(mockSetBp.Object);

            this.service = new BookPublisherService(mockContext.Object);

            var result = this.service.CheckBookDetailsForAvailability(publisherData[0].Id);

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
    }
}
