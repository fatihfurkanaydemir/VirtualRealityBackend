﻿using System.ComponentModel.DataAnnotations;

namespace VirtualReality.DTOs.Account
{
    public class GetUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
