using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogNew.Models
{
    public class ShowViewModel
    {
        public PostViewModel PostViewModel { get; set; }
        public ViewCommentList viewCommentList { get; set; }
    }
}