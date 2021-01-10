using LibraryAdministration.DomainModel;
using LibraryAdministration.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryAdministrationTest.DomainModelTests
{
    [TestClass]
    public class PersonalInfoTest
    {
        private PersonalInfoValidator _validator;

        [TestInitialize]
        public void Init()
        {
            _validator = new PersonalInfoValidator(); ;
        }

        [TestMethod]
        public void TestCreatePersonalInfoSuccess()
        {
            var info = new PersonalInfo
            {
                PhoneNumber = "0731233233",
                Email = "mircea.solo1995@gmail.com"
            };

            var result = _validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreatePersonalInfoSuccessPhoneNumberOnly()
        {
            var info = new PersonalInfo
            {
                PhoneNumber = "0731233233"
            };

            var result = _validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreatePersonalInfoSuccessEmailOnly()
        {
            var info = new PersonalInfo
            {
                Email = "mircea.solo1995@gmail.com"
            };

            var result = _validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreatePersonalInfoFailNull()
        {
            var info = new PersonalInfo();

            var result = _validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestCreatePersonalInfoFallEmpty()
        {
            var info = new PersonalInfo
            {
                PhoneNumber = "",
                Email = ""
            };

            var result = _validator.Validate(info);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
