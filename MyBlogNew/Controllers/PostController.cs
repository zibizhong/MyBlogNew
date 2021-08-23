using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogModel;
using MyBlogNew.Models;
using BlogBusinessLogic;
using ViewClassLib;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace MyBlogNewPost.Controllers
{
    public class PostController : Controller
    {

        private IDBmanager manager;
        public PostController(IDBmanager blogManager)//传入有参构造函数
        {
            manager = blogManager;
        }

        // GET: Post
        
        public ActionResult Index()//显示文章列表
        {
            ViewForPostIndexModel viewForPostIndexModel=  manager.GetViewForPostIndex();
            return View(viewForPostIndexModel);
        }
        public ActionResult Show(int? ID)//查看文章详情
        {
            Post post = manager.GetPostByID((int)ID, false);
            PostViewModel viewPostModel = null;
            if (post != null)
            {
                viewPostModel = new PostViewModel();
                viewPostModel.ID = post.ID;
                viewPostModel.Author = post.Author;
                viewPostModel.Content = post.Content;
                viewPostModel.Title = post.Title;
                viewPostModel.CreateTime = post.CreateDate;
                viewPostModel.IsPublish = post.IsPublish;
            }

            List<Comment> comments = manager.GetCommentsByPostID((int)ID);
            ViewCommentList commentList = new ViewCommentList();
            commentList.comments = new List<ViewComment>();
            foreach (var item in comments)
            {
                ViewComment c = new ViewComment
                {
                    
                    ID = item.ID,
                    Content = item.Content,
                    PostID = item.PostID,
                    CreateTime=item.CreateTime,
                    CommentUser=item.CommentUser,
                    LikeNumber=manager.LikeNumber(item.ID)
                };
                commentList.comments.Add(c);
            }
            ShowViewModel viewShowModel = new ShowViewModel
            {
                viewCommentList = commentList,
                PostViewModel = viewPostModel

            };

            return View(viewShowModel);
        }
        public ActionResult AddComment()
        {
            var Addcontent = Request["Content"];
            int postid = int.Parse(Request["postID"]);
            var user = this.User.Identity.Name;
            //将内容文章id评论用户传给管理员负责整合
            var jsoncomment = manager.PostAddComment(Addcontent, postid, user);
            string json = JsonConvert.SerializeObject(jsoncomment);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
       
        [Authorize]
        [HttpPost]
        public ActionResult  Like()
        {

            string user = this.User.Identity.Name;
            int id = int.Parse(Request["CommentID"]);
            Boolean isLike = bool.Parse(Request["IsLike"]);
            var like = new Like()
            {
                LikeUser = user,
                CommentId = id,
                IsLike=isLike
            };
            if (isLike)
            {
                var IsLike = manager.IsLike(like);//查看点赞状态
                if (IsLike)//可以点赞
                {
                    
                    
                        var nowLike = new Like()
                        {
                            CommentId = id,
                            LikeUser = user,
                            IsLike = true
                        };
                        manager.addLike(nowLike);
                        return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (isLike)//已经点过赞继续点赞取消点赞
                    {
                        manager.DelLike(user, id);
                    }
                    return Json(-1, JsonRequestBehavior.AllowGet);
                }
            }
            //点不点赞
            else
            {
                var IsLike = manager.IsLike(like);//查询是否点过赞
                //没点过赞返回0
                if (!IsLike)
                {
                    manager.DelLike(user, id);
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                //点过赞返回-1
                else
                {
                    return Json(-1, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}