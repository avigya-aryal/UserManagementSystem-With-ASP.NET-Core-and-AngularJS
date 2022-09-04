using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class Employee_Department : BaseEntity
    {
        public int EmployeeID { get; set; }

        public int DepartmentID { get; set; }
    }
}
