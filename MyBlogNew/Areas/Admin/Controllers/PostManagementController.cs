using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogBusinessLogic;
using MyBlogNew.Areas.Admin.Models;
using BlogModel;


namespace MyBlogNew.Areas.Admin.Controllers
{
    [Authorize(Users ="Admin")]
    public class PostManagementController : Controller
    {
        private IDBmanager manager;
        public PostManagementController(IDBmanager blogManager)//注入有参构造函数
        {
            manager = blogManager;
        }
        // GET: Admin/PostManagement
        public ActionResult Index()//显示所有文章
        {
            var posts = manager.GetPosts(true).Select(post => new PostMaintainViewModel()
            {
                Content = post.Content,
                ID = post.ID,
                Title = post.Title
                
            }).ToList();
            var postLiveViewModel = new PostMaintainListViewModel()
            {
                Count = posts.Count,
                PageCount = 1,
                Pages = 1,
                Posts = posts
            };
            return View(postLiveViewModel);
        }
        public ActionResult Show(int? id)//显示文章内容
        {
            if (id != null)
            {
                var post = manager.GetPostByID((int)id, true);
                if (post == null)
                {
                    return Content("该文章不存在或未发布");
                }
                var postViewModel = new PostMaintainViewModel()
                {
                    Content = post.Content,
                    ID = post.ID,
                    Title = post.Title,
                    CreateTime=post.CreateDate,
                    Author=post.Author
                };
                return View(postViewModel);
            }
            else
            {
                return View("Error");
            }

        }
        public ActionResult Edit(int? ID)
        {
            if (ID != null){ 
                Post post = manager.GetPostByID((int)ID, true);
                EditAddNewPostViewModel viewEdit = new EditAddNewPostViewModel
                {
                    PostTarget = "Edit",
                    Post_ID = post.ID,
                    Title = post.Title,
                    Content = post.Content,
                    Author=post.Author
                };
                return View(viewEdit);
            }
            else{
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Edit(EditAddNewPostViewModel viewPost)
        {
            Post oldPost = manager.GetPostByID(viewPost.Post_ID, true);
            oldPost.Content = viewPost.Content;
            oldPost.Title = viewPost.Title;
            oldPost.Author = viewPost.Author;
            oldPost.IsPublish = viewPost.IsPublish;
            manager.updatePost(oldPost);
            return RedirectToAction("Index");
        }
        public ActionResult AddPost()
        {
            EditAddNewPostViewModel model = new  EditAddNewPostViewModel();
            model.PostTarget = "AddPost";
            return View("Edit", model);
        }
        [HttpPost]
        public ActionResult AddPost( EditAddNewPostViewModel viewPost)
        {
            Post post = new Post
            {
                Title = viewPost.Title,
                Content = viewPost.Content,
                Author = this.User.Identity.Name,
                IsPublish = viewPost.IsPublish,
                CreateDate = DateTime.Now,
                ModifyTime = DateTime.Now
            };
            manager.InsertPost(post);
            return RedirectToAction("Index");
        }
       public ActionResult DelPost(int? ID)
        {
            var post = manager.GetPostByID((int)ID,true);
            manager.DeletePost(post);
            return RedirectToAction("Index");
        }

    }
}