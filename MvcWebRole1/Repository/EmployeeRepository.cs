using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcWebRole1.Entities;

namespace MvcWebRole1.Repository
{
    public static class EmployeeRepository
    {
        public static IRepository<Employee> Create()
        {
            return RepositoryFactory.Create<Employee>();
        }
    }
}