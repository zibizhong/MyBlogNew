using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModel
{
    public class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int PostID { get; set; }
        public DateTime CreateTime { get; set; }
        public string CommentUser { get; set; }
    }
}
