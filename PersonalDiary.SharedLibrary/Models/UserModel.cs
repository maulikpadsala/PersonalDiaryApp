using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalDiary.SharedLibrary.Models
{
    public class UserModel
    {
        [Key]
        [Column(TypeName = "INT")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [Column(TypeName = "NVARCHAR(200)")]
        [MaxLength(200, ErrorMessage = "Minimum Length is 200")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        [MaxLength(200, ErrorMessage = "Minimum Length is 200")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        [MaxLength(200, ErrorMessage = "Minimum Length is 200")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(200)")]
        [MaxLength(200, ErrorMessage = "Minimum Length is 200")]
        public string Password { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string IdentityUserId { get; set; }        

        [Column(TypeName = "BIT")]
        public bool IsActive { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public bool IsSuccess { get; set; }

        //RelationShip
        public List<EventModel> EventList { get; set; }

    }
}
