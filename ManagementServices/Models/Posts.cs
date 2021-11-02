using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementServices.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
