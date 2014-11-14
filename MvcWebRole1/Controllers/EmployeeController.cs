using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcWebRole1.Entities;
using MvcWebRole1.Repository;

namespace MvcWebRole1.Controllers
{
    [Authorize]
    public class EmployeeController : ApiController
    {
        // GET api/employee
        public IEnumerable<Employee> Get()
        {
            var repo = EmployeeRepository.Create();
            IEnumerable<Employee> result = repo.List(null).ToArray();
            return result;
        }

        // GET api/employee/5
        public Employee Get(int id)
        {
            var repo = EmployeeRepository.Create();
            return repo.Get(id);
        }

        // POST api/employee
        public Employee Post([FromBody]Employee item)
        {
            var repo = EmployeeRepository.Create();
            return repo.Save(item);
        }

        // DELETE api/employee/5
        public void Delete(int id)
        {
            var repo = EmployeeRepository.Create();
            repo.Delete(id);
        }
    }
}
