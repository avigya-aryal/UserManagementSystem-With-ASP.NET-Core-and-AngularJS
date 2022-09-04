using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class User_UserRole: BaseEntity
    {
        public int UserID { get; set; }

        public int RoleID { get; set; }
    }
}
