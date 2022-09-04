using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class User: BaseEntity
    {
        public int UserID { get; set; }

        public int EmployeeID { get; set; }

        public int ContactID { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }
        
        public bool? Status { get; set; }
    }
}
