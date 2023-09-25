﻿using Microsoft.AspNetCore.Identity;

namespace APITask.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
    }
}
