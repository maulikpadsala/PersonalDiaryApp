using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalDiary.SharedLibrary.Models
{
    public class LoginModel
    {
        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        [MaxLength(200, ErrorMessage = "Minimum Length is 200")]
        public string Username { get; set; }
        
        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        [MaxLength(200, ErrorMessage = "Minimum Length is 200")]
        public string Password { get; set; }

        [NotMapped]
        public bool IsSuccess { get; set; }
        [NotMapped]
        public string Error { get; set; }
    }
}
