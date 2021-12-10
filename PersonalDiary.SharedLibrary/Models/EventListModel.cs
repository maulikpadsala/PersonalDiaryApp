using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalDiary.SharedLibrary.Models
{
    public class EventListModel
    {
        public List<EventModel> eventList { get; set; }

        public string Error { get; set; }
        public bool IsSuccess { get; set; }
    }
}
