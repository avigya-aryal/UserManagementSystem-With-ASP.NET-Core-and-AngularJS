using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ViewModel
{
    public class LeaveLogViewModel
    {
        public int LeaveLogID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public string LeaveReason { get; set; }
        public int EmployeeID { get; set; }
        public string LeaveStatus { get; set; }
        public int ApprovedBy { get; set; } 
        public DateTime ApprovedDate { get; set; } 
        public int NoticePeriod { get; set; } 
    }

    public class LeaveLogListViewModel
    {
        public int LeaveLogID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public string LeaveReason { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveStatus { get; set; }
        public int ApprovedBy { get; set; } 
        public DateTime ApprovedDate { get; set; } 
        public int NoticePeriod { get; set; }
    }
}
