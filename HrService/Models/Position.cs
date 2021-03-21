using System;
using System.Collections.Generic;

#nullable disable

namespace HrService.Models
{
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
            HrSpecialists = new HashSet<HrSpecialist>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<HrSpecialist> HrSpecialists { get; set; }
    }
}
