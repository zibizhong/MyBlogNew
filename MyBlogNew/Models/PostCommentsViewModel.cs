using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogNew.Models
{
    public class PostComments
    {
        public ViewCommentListModel viewCommentListModel { get; set; }
        public PostViewModel viewPost { get; set; }
    }
}