using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LinkHolder.Models {
    public class AppUser : IdentityUser {
    }
    public class CreateUserModel{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}