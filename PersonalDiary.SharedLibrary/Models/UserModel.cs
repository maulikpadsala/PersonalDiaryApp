using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalDiary.SharedLibrary.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string IdentityUserId { get; set; }
        public string Error { get; set; }

        public bool IsSuccess { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
    }
}
