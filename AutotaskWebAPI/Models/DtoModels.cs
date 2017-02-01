using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class TicketDetails
    {
        public long AccountID { get; set; }
        public string Title { get; set; }
        public long id { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string DueDateTime { get; set; }
        public long AssignedResourceID { get; set; }
        public long AssignedResourceRoleID { get; set; }
        public long CreatorResourceID { get; set; }
        public int Priority { get; set; }
    }

    public class TaskDetails
    {
        public long ProjectID { get; set; }
        public string Title { get; set; }
        public long id { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public long AssignedResourceID { get; set; }
        public long AssignedResourceRoleID { get; set; }
        public long CreatorResourceID { get; set; }
        public int TaskType { get; set; }
    }

}