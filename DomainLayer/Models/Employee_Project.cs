using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class Employee_Project:BaseEntity
    {
        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }
    }
}
