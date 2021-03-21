using System;
using System.Collections.Generic;

#nullable disable

namespace HrService.Models
{
    public partial class Hrtask
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime? Deadline { get; set; }
        public byte? Completed { get; set; }
        public int? IdHrSpecialist { get; set; }

        public virtual HrSpecialist IdHrSpecialistNavigation { get; set; }
    }
}
