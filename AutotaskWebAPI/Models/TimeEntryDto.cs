using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public Nullable<int> TicketID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> TaskID { get; set; }
        public Nullable<System.DateTime> startdatetime { get; set; }
        public Nullable<System.DateTime> enddatetime { get; set; }
        public Nullable<decimal> HoursToBill { get; set; }
        public Nullable<decimal> HoursWorked { get; set; }
        public string SummaryNotes { get; set; }
    }
}