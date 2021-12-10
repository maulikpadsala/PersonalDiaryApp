using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalDiary.SharedLibrary.Models
{
    public class EventModel
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Event Title")]
        public string EventTitle { get; set; }

        [Required]
        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Event Image")]
        public string EventImage { get; set; }

        public int UserId { get; set; }

        public string Error { get; set; }
        public string IsSuccess { get; set; }

    }
}
