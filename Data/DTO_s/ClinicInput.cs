﻿using System.ComponentModel.DataAnnotations;

namespace HospitalSystemTeamTask.DTO_s
{
    public class ClinicInput
    {
       

        [Required]
        public int AssignDoctor { get; set; }

        [Required]
        public string ClincName { get; set; }  

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Slot Duration must be greater than 0.")]
        public int SlotDuration { get; set; } 

        [Required]
        public TimeSpan StartTime { get; set; } 

        [Required]
        public TimeSpan EndTime { get; set; } 

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be greater than 0.")]
        public decimal Cost { get; set; } 

    }
}