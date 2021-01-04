﻿using System;
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
    public class ReaderTest
    {
        private ReaderValidator _readerValidator;

        [TestInitialize]
        public void Init()
        {
            _readerValidator = new ReaderValidator();;
        }

        [TestMethod]
        public void TestCreateReaderSuccess()
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
                LastName = "Solovastru"
            };

            var result = _readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

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

            var result = _readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

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

            var result = _readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }

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

            var result = _readerValidator.Validate(reader);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsFalse(result.Errors.Count == 0);
        }
    }
}