using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCeiba.Models
{
    public class Posts
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
