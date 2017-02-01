using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class ResourceRoleDto
    {
        public long ResourceId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }

    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}