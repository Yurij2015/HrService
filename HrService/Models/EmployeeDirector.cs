using System;
using System.Collections.Generic;

#nullable disable

namespace HrService.Models
{
    public partial class EmployeeDirector
    {
        public EmployeeDirector()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthData { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? IdPosition { get; set; }
        public int? IdDivision { get; set; }
        public int? IdUser { get; set; }

        public virtual Division IdDivisionNavigation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
