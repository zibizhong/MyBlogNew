using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogModel;
using ViewClassLib;

namespace BlogBusinessLogic
{
    public interface IDBmanager
    {
        void InsertPost(Post post);
        void DeletePost(Post post);
        void updatePost(Post post);
        List<Post> GetPosts(Boolean flag);
        Post GetPostByID(int id, Boolean flag);
        List<Comment> GetComments();
        void AddComment(Comment comment);
        List<Comment> GetCommentsByCommentID(int id);
        int LikeNumber(int id);
        List<Comment> GetCommentsByPostID(int post_id);
        void addLike(Like like);
        void DelLike(string user, int commentid);
        Boolean IsLike(Like isLike);
        Post GetPostTop1();
        List<Comment> GetCommentsTop3();
        ViewForIndex GetIndexModel();
        ViewForPostIndexModel GetViewForPostIndex();
        IEnumerable<Comment> PostAddComment(string addcontent, int postid, string commentUser);
        
    }
}
