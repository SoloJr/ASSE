//---------------------------------------------------------------------
// <copyright file="EmployeeTest.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministrationTest.DomainModelTests
{
    using LibraryAdministration.DomainModel;
    using LibraryAdministration.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// EmployeeTest for Domain Model
    /// </summary>
    [TestClass]
    public class EmployeeTest
    {
        /// <summary>
        /// The validator
        /// </summary>
        private EmployeeValidator validator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.validator = new EmployeeValidator();
        }

        /// <summary>
        /// Tests the create employee success.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeSuccess()
        {
            var employee = new Employee
            {
                Id = 1,
                Info = new PersonalInfo
                {
                    Id = 1,
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
                LastName = "Solovastru",
                EmployeePersonalInfoId = 1
            };

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsNotNull(employee.Info);
        }

        /// <summary>
        /// Tests the create employee success but with only one personal information.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeSuccessButWithOnlyOnePersonalInfo()
        {
            var employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
                LastName = "Solovastru"
            };

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create employee fail with no address.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithNoAddress()
        {
            var employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                FirstName = "Mircea",
                LastName = "Solovastru"
            };

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the first name of the create employee fail with no.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithNoFirstName()
        {
            var employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                LastName = "Solovastru"
            };

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the last name of the create employee fail with.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithLastName()
        {
            var employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
            };

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create employee fail with address too long.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithAddressTooLong()
        {
            var employee = new Employee
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

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create employee fail with address too short.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithAddressTooShort()
        {
            var employee = new Employee
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

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create employee fail with first name too long.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithFirstNameTooLong()
        {
            var employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mirceaaaaaaaaaaaaaaaaaaaa",
                LastName = "Solovastru"
            };

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create employee fail with first name too short.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithFirstNameTooShort()
        {
            var employee = new Employee
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

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create employee fail with last name too long.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithLastNameTooLong()
        {
            var employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112 Vila 18 Ap. 4",
                FirstName = "Mircea",
                LastName = "Solovastruuuuuuuuuuuuuuuuu"
            };

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

        /// <summary>
        /// Tests the create employee fail with last name too short.
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeFailWithLastNameTooShort()
        {
            var employee = new Employee
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

            var result = this.validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
