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
    public class EmployeeTest
    {
        private EmployeeValidator _validator;

        [TestInitialize]
        public void Init()
        {
            _validator = new EmployeeValidator(); ;
        }

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

            var result = _validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            Assert.IsNotNull(employee.Info);
        }

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

            var result = _validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

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

            var result = _validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _validator.Validate(employee);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}
