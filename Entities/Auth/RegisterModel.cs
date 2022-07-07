﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Auth
{
    public class RegisterModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string RepeatPassword { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
        public string Avatar { get; set; }
        public string Company { get; set; }

    }
}
