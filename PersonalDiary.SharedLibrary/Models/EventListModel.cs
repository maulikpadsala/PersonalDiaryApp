using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalDiary.SharedLibrary.Models
{
    public class EventListModel
    {
        public List<EventModel> eventList { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public bool IsSuccess { get; set; }
    }
}
