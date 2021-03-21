using System;
using System.Collections.Generic;

#nullable disable

namespace HrService.Models
{
    public partial class WorkPlan
    {
        public int Id { get; set; }
        public string WorkTask { get; set; }
        public DateTime? Deadline { get; set; }
        public byte? Completed { get; set; }
        public string Comment { get; set; }
        public int? IdEmployee { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
    }
}
