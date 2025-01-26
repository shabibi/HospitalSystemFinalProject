namespace HospitalSystemTeamTask.DTO_s
{
    public class DoctorOutPutDTO
    {
        public int UID { get; set; }
        public string UserName { get; set; } // Added
        public string Email { get; set; } // Added
        public string Phone { get; set; } // Added
        public int CurrentBrunch { get; set; }
        public string Level { get; set; }
        public string Degree { get; set; }
        public int WorkingYear { get; set; }
        public DateOnly JoiningDate { get; set; }
        public int DepId { get; set; }
        public int? CID { get; set; }
        public string Image { get; set; } // Added property

    }
}
