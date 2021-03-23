using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? BirthData { get; set; }
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Display(Name = "Электронный адрес")]
        public string Email { get; set; }
        [Display(Name = "Должность")]
        public int? IdPosition { get; set; }
        [Display(Name = "Отдел")]
        public int? IdDivision { get; set; }
        [Display(Name = "")]
        public int? IdUser { get; set; }

        [Display(Name = "Отдел")]
        public virtual Division IdDivisionNavigation { get; set; }
        [Display(Name = "Сотрудник")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
