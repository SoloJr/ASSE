using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using LibraryAdministrationTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace LibraryAdministrationTest.ServiceTests
{
    [TestClass]
    public class EmployeeServiceTest
    {
        private Employee _employee;

        [TestInitialize]
        public void Init()
        {
            Injector.Inject(new MockBindings());
            _employee = new Employee
            {
                Info = new PersonalInfo
                {
                    PhoneNumber = "0731233233",
                    Email = "mircea.solo1995@gmail.com"
                },
                Address = "Str. Drumul cu Plopi Nr. 112",
                FirstName = "Mircea",
                LastName = "Solovastru"
            };
        }

        [TestMethod]
        public void TestInsertEmployee()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IEmployeeService>();

            var result = service.Insert(_employee);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestUpdateEmployee()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IEmployeeService>();

            var result = service.Update(_employee);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            var kernel = Injector.Kernel;
            var service = kernel.Get<IEmployeeService>();

           // Assert.ThrowsException<DeleteItemException>(() => service.Delete(_employee));
        }
    }
}
