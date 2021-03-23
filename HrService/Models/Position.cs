using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Наименование должности")]
        public string Name { get; set; }

        [Display(Name = "Сотрудник")]
        public virtual ICollection<Employee> Employees { get; set; }
        [Display(Name = "HR-специалист")]
        public virtual ICollection<HrSpecialist> HrSpecialists { get; set; }
    }
}
