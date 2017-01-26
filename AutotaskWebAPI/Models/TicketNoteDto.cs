using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class TicketNoteDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> NoteType { get; set; }
        public Nullable<int> TicketID { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> LastActivityDate { get; set; }
    }
}