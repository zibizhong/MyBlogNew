using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModel
{
 
    public class PostList
    {
        public int ID { get; set; }
        public string Title { get; set; }

    }
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyTime { get; set; }
        public Boolean IsPublish { get; set; }
    }
}
