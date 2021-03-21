using System;
using System.Collections.Generic;

#nullable disable

namespace HrService.Models
{
    public partial class Division
    {
        public Division()
        {
            EmployeeDirectors = new HashSet<EmployeeDirector>();
            Employees = new HashSet<Employee>();
            HrSpecialists = new HashSet<HrSpecialist>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeDirector> EmployeeDirectors { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<HrSpecialist> HrSpecialists { get; set; }
    }
}
