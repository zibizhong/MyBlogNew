using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogNew.Areas.Admin.Models
{
    public class PostMaintainViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
    }
}