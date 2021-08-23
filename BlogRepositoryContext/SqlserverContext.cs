using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogModel;
namespace BlogRepositoryContext
{
    public class SqlserverContext : DbContext
    {
        public SqlserverContext() : base("sqlserverDB")
        {

        }
        public DbSet<Post> Posts { get; set; }//传入post表
        public DbSet<Comment> Comment { get; set; }//传入comment表
        public DbSet<User> User { get; set; }//传入用户表
        public DbSet<Like> Like { get; set; }
    }
    public class SqlserverCDB : IRepository
    {

        public List<Post> GetPosts(Boolean flag)//判断是否为管理员
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                List<Post> posts = new List<Post>();//全部返回
                if (flag == true)
                {
                    return context.Posts.OrderByDescending(m=>m.CreateDate).ToList();
                }
                else//返回ispublish为true文章
                {
                    return context.Posts.Where(s => s.IsPublish == true).OrderByDescending(m=>m.CreateDate).ToList();
                }
            }
        }//获取所有文章

        public Post GetPostByID(int id, Boolean flag)
        {
            if (flag)
            {
                using (SqlserverContext context = new SqlserverContext())
                {
                    return context.Posts.Where(x => x.ID == id).FirstOrDefault();
                }
            }
            using (SqlserverContext context = new SqlserverContext())
            {
                return context.Posts.Where(x => x.IsPublish == true && x.ID == id).FirstOrDefault();
            }
        }//获取对应ID文章
        public List<Comment> GetCommentsByPostId(int post_id)
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                return context.Comment.Where(s => s.ID == post_id).ToList();
            }
        }//获取对应ID评论
        public void AddComment(Comment comment)
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                context.Comment.Add(comment);
                context.SaveChanges();//save data
            }
        }//添加评论
        public List<Comment> GetCommentsByPostID(int post_id)
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                return context.Comment.Where(x => x.PostID == post_id).ToList();
            }
        }
        public void updatePost(Post post)//更新文章
        {
            using (var dbcontext = new SqlserverContext())
            {
                dbcontext.Entry(post).State = EntityState.Modified;
                dbcontext.SaveChanges();
            }
        }
        public void InsertPost(Post post)//插入文章
        {
            using (var dbcontext = new SqlserverContext())
            {
                dbcontext.Posts.Add(post);
                dbcontext.SaveChanges();
            }
        }
        public List<Comment> GetCommentsByCommentID(int id)
        {
            using (var dbcontext = new SqlserverContext())
            {
                return dbcontext.Comment.Where(m => m.ID == id).ToList();
                
            }
        }
        public List<Comment> GetComments()
        {
            using(var dbcontext=new SqlserverContext())
            {
                return dbcontext.Comment.OrderByDescending(m=>m.CreateTime).ToList();
            }
        }
        public void DeletePost(Post post)//删除文章
        {
            using (var dbcontext = new SqlserverContext())
            {

                dbcontext.Entry(post).State = EntityState.Deleted;
                dbcontext.SaveChanges();
            }
        }
        public int LikeNumber(int id)
        {
            using (var dbcontext = new SqlserverContext())
            {
                var Like=dbcontext.Like.Where(m => m.CommentId == id).ToList();
                int LikeNumber=Like.Count();
                return LikeNumber;
            }
        }
       public void addLike(Like addLike)
        {
            using (var dbcontext = new SqlserverContext())
            {
                var like = new Like()
                {
                    LikeUser = addLike.LikeUser,
                    CommentId = addLike.CommentId,
                    IsLike = addLike.IsLike
                };
                dbcontext.Like.Add(like);
                dbcontext.SaveChanges();
            }
        }
        
        
        public Boolean IsLike(Like isLike)
        {
            using (var dbcontext = new SqlserverContext())
            {
                if (isLike.IsLike)//点赞
                {
                    var like = dbcontext.Like.Where(m => m.LikeUser == isLike.LikeUser && m.CommentId == isLike.CommentId && m.IsLike == isLike.IsLike).ToList();
                    if (like.Count==0)//数据库中是false可以点赞
                    {
                        return true;
                    }
                    return false; //数据库中是true不能点赞   
                }
                var liKe = dbcontext.Like.Where(m => m.LikeUser == isLike.LikeUser && m.CommentId == isLike.CommentId && m.IsLike == isLike.IsLike).ToList();
                if (liKe.Count!=0)//数据库中是true
                {
                    return false;
                }
                return true;//数据库中为false
                
            }
        }
        public Post GetPostTop1()
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                return context.Posts.OrderByDescending(x => x.CreateDate).FirstOrDefault();
            }
        }
        public List<Comment> GetCommentsTop3()
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                return context.Comment.OrderByDescending(x => x.CreateTime).Take(3).ToList();
            }
        }
        public void DelLike(string user,int commentid)
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                var delLike = context.Like.FirstOrDefault(m => m.LikeUser == user && m.CommentId == commentid);
                if (delLike!=null){
                    context.Like.Remove(delLike);
                    context.SaveChanges();
                }
            }
        }
        public void DelCommentByPostId(int id)
        {
            using (SqlserverContext context = new SqlserverContext())
            {
                var delComment = context.Comment.Where(m => m.PostID == id).ToList();
                if (delComment != null)
                {
                    context.Comment.RemoveRange(delComment);
                }
                context.SaveChanges();
            }
        }
    }

}
