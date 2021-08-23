using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogModel;
using BlogRepositoryContext;
using ViewClassLib;
using MyBlogHelperClass;


namespace BlogBusinessLogic
{
    public class DBManager : IDBmanager
    {
        private IRepository _postRepository;
        public DBManager(IRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        public void InsertPost(Post post)
        {
            _postRepository.InsertPost(post);
        }

        public void DeletePost(Post post)
        {
            _postRepository.DeletePost(post);
            _postRepository.DelCommentByPostId(post.ID);
            
        }

        public List<Post> GetPosts(bool flag)
        {
            var post = _postRepository.GetPosts(flag);
            return post;
        }

        public Post GetPostByID(int id, bool flag)
        {
            var post = _postRepository.GetPostByID(id, flag);
            return post;
        }

        public void updatePost(Post post)
        {
            _postRepository.updatePost(post);
        }
        public void AddComment(Comment comment)
        {
            _postRepository.AddComment(comment);
        }
        public List<Comment> GetCommentsByPostID(int post_id)
        {
            var comment=_postRepository.GetCommentsByPostID(post_id).OrderByDescending(m=>m.CreateTime).ToList();
            return comment;
        }
        public List<Comment> GetCommentsByCommentID(int id)
        {
            var comment = _postRepository.GetCommentsByCommentID(id);
            return comment;
        }
        public void addLike(Like like)
        {
             _postRepository.addLike(like);
        }
        public void DelLike(string user,int commentid)
        {
            _postRepository.DelLike(user, commentid);
        }
        public Boolean IsLike(Like isLike)
        {
            var IsLike = _postRepository.IsLike(isLike);
            return IsLike;
        }
        public int LikeNumber(int id)
        {
            var LikeNumber = _postRepository.LikeNumber(id);
            return LikeNumber;
        }

        public List<Comment> GetComments()
        {
            return _postRepository.GetComments();
        }
        public Post GetPostTop1()
        {
            return _postRepository.GetPostTop1();
        }

        public List<Comment> GetCommentsTop3()
        {
            return _postRepository.GetCommentsTop3();
        }
        public ViewForIndex GetIndexModel()
        {
            ViewForIndex viewForIndex = new ViewForIndex();
            //取最新的文章，并组合到视图模型中
            Post post = _postRepository.GetPostTop1();
            string postcontent = BlogHelper.NoHTML(post.Content);
            viewForIndex.viewPost = new ViewPostModel
            {
                Title = post.Title,
                ID = post.ID,
                Content = postcontent.Length > 100 ? postcontent.Substring(0, 100) + "...." : postcontent
            };
            //取3条最新评论
            List<Post> posts = _postRepository.GetPosts(false).OrderByDescending(x => x.CreateDate).Take(3).ToList();
            viewForIndex.postList = new ViewPostListForIndex();
            viewForIndex.postList.viewPostList = new List<ViewPostModel>();
            foreach (var x in posts)
            {
                viewForIndex.postList.viewPostList.Add(new ViewPostModel
                {
                    Title = x.Title.Length>10?x.Title.Substring(0,10)+"...":x.Title,
                    ID = x.ID,
                });
            }
            List<Comment> comments = _postRepository.GetCommentsTop3();
            viewForIndex.commentList = new List<ViewCommentListForIndex>();
            foreach (var y in comments)
            {
                y.Content = BlogHelper.NoHTML(y.Content);
                viewForIndex.commentList.Add(new ViewCommentListForIndex
                {

                    PostID = y.PostID,
                    Comment =y.Content.Length>8?y.Content.Substring(0,8)+"....":y.Content
                    
                });
            }
            return viewForIndex;
        }
        public ViewForPostIndexModel GetViewForPostIndex()//文章列表
        {
            Boolean flag = false;
            List<Post> posts = _postRepository.GetPosts(flag);
            
            ViewForPostIndexModel viewPostListModel = new ViewForPostIndexModel
            {
                counter = posts.Count,
                posts = posts
            };
            return viewPostListModel;
        }
        public IEnumerable<Comment> PostAddComment(string addcontent,int postid,string commentUser)
        {
            Comment viewComment = new Comment()
            {
                Content = addcontent,
                PostID = postid,
                CommentUser = commentUser,
                CreateTime = DateTime.Now
            };
            AddComment(viewComment);
            var jsoncomment = GetCommentsByPostID(postid).OrderByDescending(m => m.CreateTime).Take(1);
            return jsoncomment;
            
        }

        

       
    }
}
