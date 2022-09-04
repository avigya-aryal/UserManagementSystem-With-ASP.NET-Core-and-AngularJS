using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ViewModel
{
    public class UserViewModel
    {
        public int UserID { get; set; }

        public int EmployeeID { get; set; }

        public string Role { get; set; }

        public bool? Status { get; set; }
    }

    public class UserListViewModel
    {
        public int UserID { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public bool? Status { get; set; }

    }
}
