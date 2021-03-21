using System;
using System.Collections.Generic;

#nullable disable

namespace HrService.Models
{
    public partial class Employee
    {
        public Employee()
        {
            WorkPlans = new HashSet<WorkPlan>();
            Training = new HashSet<Training>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Skils { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? IdPosition { get; set; }
        public int? IdUser { get; set; }
        public int? IdDivision { get; set; }
        public int? IdDirector { get; set; }
        public int? Satus { get; set; }

        public virtual EmployeeDirector IdDirectorNavigation { get; set; }
        public virtual Division IdDivisionNavigation { get; set; }
        public virtual Position IdPositionNavigation { get; set; }
        public virtual ICollection<WorkPlan> WorkPlans { get; set; }
        public virtual ICollection<Training> Training { get; set; }
    }
}
