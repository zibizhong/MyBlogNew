using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogNew.Models
{
    public class ViewCommentListModel
    {
        public int PostID { get; set; }
        public List<ViewCommentModel> CommentLists { get; set; }
    }
    public class ViewCommentModel
    {
        public int ID { get; set; }
         [AllowHtml]
        public string Content { get; set; }
    }
}