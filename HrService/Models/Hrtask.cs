using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HrService.Models
{
    public partial class Hrtask
    {
        public int Id { get; set; }
        [Display(Name = "Название задачи")]
        public string Task { get; set; }
        [Display(Name = "Срок выполнения")]
        public DateTime? Deadline { get; set; }
        [Display(Name = "Статус выполнения")]
        public byte? Completed { get; set; }
        [Display(Name = "HR-специалист")]
        public int? IdHrSpecialist { get; set; }

        [Display(Name = "HR-специалист")]
        public virtual HrSpecialist IdHrSpecialistNavigation { get; set; }
    }
}
