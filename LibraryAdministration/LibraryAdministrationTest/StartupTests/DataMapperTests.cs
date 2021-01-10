//---------------------------------------------------------------------
// <copyright file="DataMapperTests.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.StartupTests
{
    using System.Data.Entity;
    using LibraryAdministration.DataMapper;
    using LibraryAdministration.DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// DataMapper tests
    /// </summary>
    [TestClass]
    public class DataMapperTests
    {
        /// <summary>
        /// Tests the data mapper.
        /// </summary>
        [TestMethod]
        public void TestDataMapper()
        {
            var mockContext = new Mock<LibraryContext>();
            var mockSet = new Mock<DbSet<Book>>();

            mockContext.Setup(x => x.Books).Returns(mockSet.Object);

            Assert.IsNotNull(mockContext);
            Assert.IsNotNull(mockContext.Object);
            Assert.IsNotNull(mockContext.Object.Books);
        }
    }
}
