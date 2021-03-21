using System;
using System.Collections.Generic;

#nullable disable

namespace HrService.Models
{
    public partial class HrSpecialist
    {
        public HrSpecialist()
        {
            Hrtasks = new HashSet<Hrtask>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? IdPosition { get; set; }
        public int? IdDivision { get; set; }
        public int? IdUser { get; set; }

        public virtual Division IdDivisionNavigation { get; set; }
        public virtual Position IdPositionNavigation { get; set; }
        public virtual ICollection<Hrtask> Hrtasks { get; set; }
    }
}
