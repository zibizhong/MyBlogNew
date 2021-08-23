using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogModel;
using System.Web.Mvc;
namespace MyBlogNew.Models
{
    public class ViewPostListModel
    {
        public int counter { get; set; }
        public List<Post> posts { get; set; }

    }
    public class PostViewModel
    {
        public string PostTarget { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
       [AllowHtml]
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public Boolean IsPublish { get; set; }

    }

}