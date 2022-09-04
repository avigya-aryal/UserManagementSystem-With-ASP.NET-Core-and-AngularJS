using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DomainLayer.Models
{
    public class Employee : BaseEntity
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
}
