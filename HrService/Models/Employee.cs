using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Навыки")]
        public string Skils { get; set; }
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Display(Name = "Электронный адрес")]
        public string Email { get; set; }
        [Display(Name = "Должность")]
        public int? IdPosition { get; set; }
        [Display(Name = "")]
        public int? IdUser { get; set; }
        [Display(Name = "Отдел")]
        public int? IdDivision { get; set; }
        [Display(Name = "Руководитель сотрудника")]
        public int? IdDirector { get; set; }
        [Display(Name = "Статус")]
        public int? Satus { get; set; }

        [Display(Name = "Руководитель сотрудника")]
        public virtual EmployeeDirector IdDirectorNavigation { get; set; }
        [Display(Name = "Отдел")]
        public virtual Division IdDivisionNavigation { get; set; }
        [Display(Name = "Должность")]
        public virtual Position IdPositionNavigation { get; set; }
        [Display(Name = "Рабочая задача")]
        public virtual ICollection<WorkPlan> WorkPlans { get; set; }
        [Display(Name = "Обучение")]
        public virtual ICollection<Training> Training { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.MiddleName + " " + this.SecondName;
            }
        }
    }
}
