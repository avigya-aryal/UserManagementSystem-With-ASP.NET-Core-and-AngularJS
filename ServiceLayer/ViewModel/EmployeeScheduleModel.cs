using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ViewModel
{
   public class EmployeeScheduleModel
    {
        public int ScheduleId { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime? EndHour { get; set; }
        //public int CreatedBy { get; set; }
        //public int ModifiedBy { get; set; }
        //public DateTime ModifiedTS { get; set; }
        //public DateTime CreatedTS { get; set; }
    }
}
