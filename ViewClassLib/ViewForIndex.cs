using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewClassLib
{
    public class ViewForIndex
    {
        public ViewPostModel viewPost { get; set; }//文章
        public ViewPostListForIndex postList { get; set; }//文章列表
        public List<ViewCommentListForIndex> commentList { get; set; }//评论列表
    }
}