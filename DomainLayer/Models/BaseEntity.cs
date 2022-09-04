using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public class BaseEntity
    {
        public DateTime CreatedTS { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifiedTS { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
