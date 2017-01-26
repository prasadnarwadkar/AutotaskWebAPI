using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class AttachmentDto
    {
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public long Id { get; set; }
        public string FullPath { get; set; }
        public DateTime AttachDate { get; set; }
    }
}