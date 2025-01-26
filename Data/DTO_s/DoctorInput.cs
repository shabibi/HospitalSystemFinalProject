using System.ComponentModel.DataAnnotations;

namespace HospitalSystemTeamTask.DTO_s
{
    public class DoctorInput
    {
        public int DID { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public int WorkingYear { get; set; }

        [Required]
        public int CurrentBrunch { get; set; }
        [Required]
        public int DepID { get; set; }

    }
}
