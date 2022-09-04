using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
   public class EmployeeSchedule:BaseEntity
    {
        public int ScheduleId { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime? EndHour { get; set; }
    }
}
