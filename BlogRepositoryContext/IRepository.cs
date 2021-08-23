using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogModel;

namespace BlogRepositoryContext
{
    public interface IRepository
    {
        void InsertPost(Post post);
        void DeletePost(Post post);
        void updatePost(Post post);
        List<Post> GetPosts(Boolean flag);
        Post GetPostByID(int id, Boolean flag);
        List<Comment> GetComments();
        List<Comment> GetCommentsByCommentID(int id);
        void AddComment(Comment comment);
        List<Comment> GetCommentsByPostID(int post_id);
        void addLike(Like like);
        void DelLike(string user, int commentid);
        Boolean IsLike(Like islike);
        int LikeNumber(int id);
        Post GetPostTop1();
       List<Comment> GetCommentsTop3();
        void DelCommentByPostId(int id);
    }
}
