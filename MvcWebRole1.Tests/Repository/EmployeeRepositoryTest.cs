using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcWebRole1.Entities;
using MvcWebRole1.Repository;

namespace MvcWebRole1.Tests.Repository
{
    
    [TestClass]
    public class EmployeeRepositoryTest
    {
        [TestCategory("Repository")]
        [TestMethod]
        public void LoadFromFactory()
        {
            IRepository<Employee> repo = EmployeeRepository.Create();

            Debug.Assert(repo != null);
        }

        [TestCategory("Repository")]
        [TestMethod]
        public void SaveNewEmployee()
        {
            IRepository<Employee> repo = EmployeeRepository.Create();
            Employee jane = repo.Save(new Employee()
            {
                Name = "Jane Smith",
                Email = "j@s.com",
                Location = "US",
                Phone = "n/a",
                Roles = EmployeeRoles.Employee | EmployeeRoles.HR
            });
            Debug.Assert(repo != null);
            Debug.Assert(jane != null && jane.Id > 0);
        }

        [TestCategory("Repository")]
        [TestMethod]
        public void SaveNewAndUpdateEmployee()
        {
            IRepository<Employee> repo = EmployeeRepository.Create();
            Employee jane = repo.Save(new Employee()
            {
                Name = "Jane Smith",
                Email = "j@s.com",
                Location = "US",
                Phone = "n/a",
                Roles = EmployeeRoles.Employee | EmployeeRoles.HR
            });
            Debug.Assert(repo != null);
            Debug.Assert(jane != null && jane.Id > 0);

            jane.Phone = "111-222-3333";
            repo.Save(jane);
            Debug.Assert(jane != null && jane.Id > 0 && jane.Phone.Equals("111-222-3333"));
        }

        [TestCategory("Repository")]
        [TestMethod]
        public void GetEmployee()
        {
            IRepository<Employee> repo = EmployeeRepository.Create();
            Employee jane = repo.Get(new Employee()
            {
                Id = 6
            });

            Debug.Assert(repo != null);
            Debug.Assert(jane != null && jane.Id == 6);
        }

        [TestCategory("Repository")]
        [TestMethod]
        public void GetEmployeeById()
        {
            IRepository<Employee> repo = EmployeeRepository.Create();
            Employee jane = repo.Get(6);

            Debug.Assert(repo != null);
            Debug.Assert(jane != null && jane.Id == 6);
        }

        [TestCategory("Repository")]
        [TestMethod]
        public void SaveNewAndDeleteEmployee()
        {
            IRepository<Employee> repo = EmployeeRepository.Create();
            Employee jane = repo.Save(new Employee()
            {
                Name = "Jane Smith",
                Email = "j@s.com",
                Location = "US",
                Phone = "n/a",
                Roles = EmployeeRoles.Employee | EmployeeRoles.HR
            });
            Debug.Assert(repo != null);
            Debug.Assert(jane != null && jane.Id > 0);

            repo.Delete(jane);

            jane = repo.Get(jane.Id);
            Debug.Assert(jane == null);
        }
    }
}
