using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalDiary.SharedLibrary.Models
{
    public class EventModel
    {
        [Key]
        [Column(TypeName = "INT")]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Event Title")]
        [Column(TypeName = "NVARCHAR(200)")]
        [MaxLength(200, ErrorMessage = "Minimum Length is 200")]        
        public string EventTitle { get; set; }

        [Required]
        [Display(Name = "Event Description")]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string EventDescription { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Event Image")]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string EventImage { get; set; }

        [Display(Name = "Favourite")]
        [Column(TypeName = "BIT")]
        public bool IsFavourite { get; set; }

        [Column(TypeName = "INT")]
        [ForeignKey("UserModel")]
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public bool IsSuccess { get; set; }

    }
}
