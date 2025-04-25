﻿using System.ComponentModel.DataAnnotations;

namespace Group13iFinanceFix.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
