using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiaryApp.Models
{
    public class EventModel
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string EventTitle { get; set; }
        [Required]
        public string EventDescription { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        
        public string EventImage { get; set; }

        public int UserId { get; set; }

    }
}
