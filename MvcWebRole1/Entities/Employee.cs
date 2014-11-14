using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebRole1.Entities
{
    [Flags]
    public enum EmployeeRoles : int
    {
        Employee = 1,
        HR = 2
    }

    public class Employee : EntityBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public EmployeeRoles Roles { get; set; }
        public string Title { get; set; }
    }
}