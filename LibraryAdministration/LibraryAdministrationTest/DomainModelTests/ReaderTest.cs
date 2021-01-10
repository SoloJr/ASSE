//---------------------------------------------------------------------
// <copyright file="ReaderTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Reader Domain Model Tests
    /// </summary>
    [TestClass]
    public class ReaderTest
    {
        /// <summary>
        /// The reader validator
        /// </summary>
        private ReaderValidator readerValidator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.readerValidator = new ReaderValidator();
        }

        /// <summary>
        /// Tests the create reader success.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderSuccess()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    Id = 1,
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
                LastName = "Solovastru"
            };

            reader.ReaderPersonalInfoId = reader.Info.Id;

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsNotNull(reader.Info);
        }

        /// <summary>
        /// Tests the create reader success but with only one personal information.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderSuccessButWithOnlyOnePersonalInfo()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
                LastName = "Solovastru"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create reader fail with no address.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithNoAddress()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                FirstName = "Mircea",
                LastName = "Solovastru"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the first name of the create reader fail with no.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithNoFirstName()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                LastName = "Solovastru"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the last name of the create reader fail with.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithLastName()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create reader fail with address too long.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithAddressTooLong()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4, Cod Postal 500265, Judet Brasov, Tara Romania, Continent Europa, Lumea Intreaga",
                FirstName = "Mircea",
                LastName = "Solovastru"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create reader fail with address too short.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithAddressTooShort()
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
                LastName = "Solovastru"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create reader fail with first name too long.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithFirstNameTooLong()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mirceaaaaaaaaaaaaaaaaaaaaaaaa",
                LastName = "Solovastru"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create reader fail with first name too short.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithFirstNameTooShort()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "M",
                LastName = "Solovastru"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create reader fail with last name too long.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithLastNameTooLong()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
                LastName = "Solovastruuuuuuuuuuuuuuu"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create reader fail with last name too short.
        /// </summary>
        [TestMethod]
        public void TestCreateReaderFailWithLastNameTooShort()
        {
            var reader = new Reader
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
                LastName = "S"
            };

            var result = this.readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
