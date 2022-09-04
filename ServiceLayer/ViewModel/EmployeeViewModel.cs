using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLayer.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public string Designation { get; set; }
        public bool? Status { get; set; }
        public int ContactID { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? LeftDate { get; set; }
        public string CitizenshipImage { get; set; }
        public string ImageOwnerUniqueName { get; set; }

    }

    public class EmployeeListViewModel
    {
        public int EmployeeID { get; set; }
        public string Designation { get; set; }
        public bool? Status { get; set; }
        public string EmployeeName { get; set; }
        public DateTime JoinedDate { get; set; }
        public string CitizenshipImage { get; set; } //comment this
        public string ImageOwnerUniqueName { get; set; } //comment this
    }
   
}
