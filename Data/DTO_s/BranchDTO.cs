﻿using System.ComponentModel.DataAnnotations;

namespace HospitalSystemTeamTask.DTO_s
{
    public class BranchDTO
    {
        public int BID { get; set; } // Add this property for the Branch ID

        [Required(ErrorMessage = "Branch name is required.")]
        [StringLength(100, ErrorMessage = "Branch name must be between 3 and 100 characters.", MinimumLength = 3)]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Location { get; set; }

        public bool BranchStatus { get; set; }
    }

    public class UpdateBranchDTO
    {
        public string BranchName { get; set; }
        public string Location { get; set; }
        public bool BranchStatus { get; set; }

    }

}
