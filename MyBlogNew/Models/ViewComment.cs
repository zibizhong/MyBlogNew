using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyBlogNew.Models
{
    public class ViewComment
    {
        public string CommentUser { get; set; }
         [AllowHtml]
        public string Content { get; set; }
        public int ID { set; get; }
        public int PostID { get; set; }
        public DateTime CreateTime { get; set; }
        public int LikeNumber { get; set; }

    }
    public class IsLike
    {
        public Boolean Like { get; set; }
        public string LikeUser { get; set; }
        public int CommentId { get; set; }
    }
    public class ViewCommentList
    {
        public List<ViewComment> comments { get; set; }
        public List<IsLike> isLikes { get; set; }
    }
}