//---------------------------------------------------------------------
// <copyright file="PersonalInfoTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// PersonalInfoTest for Domain Model
    /// </summary>
    [TestClass]
    public class PersonalInfoTest
    {
        /// <summary>
        /// The validator
        /// </summary>
        private PersonalInfoValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.validator = new PersonalInfoValidator();
        }

        /// <summary>
        /// Tests the create personal information success.
        /// </summary>
        [TestMethod]
        public void TestCreatePersonalInfoSuccess()
        {
            var info = new PersonalInfo
            {
                PhoneNumber = "0731233233",
                Email = "mircea.solo1995@gmail.com"
            };

            var result = this.validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create personal information success phone number only.
        /// </summary>
        [TestMethod]
        public void TestCreatePersonalInfoSuccessPhoneNumberOnly()
        {
            var info = new PersonalInfo
            {
                PhoneNumber = "0731233233"
            };

            var result = this.validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create personal information success email only.
        /// </summary>
        [TestMethod]
        public void TestCreatePersonalInfoSuccessEmailOnly()
        {
            var info = new PersonalInfo
            {
                Email = "mircea.solo1995@gmail.com"
            };

            var result = this.validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create personal information fail null.
        /// </summary>
        [TestMethod]
        public void TestCreatePersonalInfoFailNull()
        {
            var info = new PersonalInfo();

            var result = this.validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create personal information fall empty.
        /// </summary>
        [TestMethod]
        public void TestCreatePersonalInfoFallEmpty()
        {
            var info = new PersonalInfo
            {
                PhoneNumber = string.Empty,
                Email = string.Empty
            };

            var result = this.validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
