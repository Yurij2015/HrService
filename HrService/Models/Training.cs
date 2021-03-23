using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HrService.Models
{
    public partial class Training
    {
        public int Id { get; set; }
        [Display(Name = "Учебная задачи")]
        public string LearnTask { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        [Display(Name = "Срок выполнения")]
        public DateTime? Deadline { get; set; }
        [Display(Name = "Статус выполнения")]
        public byte? Completed { get; set; }
        [Display(Name = "Сотрудник")]
        public int? IdEmployee { get; set; }

        [Display(Name = "Сотрудник")]
        public virtual Employee IdEmployeeNavigation { get; set; }
    }
}
