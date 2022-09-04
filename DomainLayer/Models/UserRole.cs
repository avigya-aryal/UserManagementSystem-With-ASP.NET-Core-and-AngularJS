using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class UserRole: BaseEntity
    {
        public int RoleID { get; set; }

        public string RoleType { get; set; }
    }
}
