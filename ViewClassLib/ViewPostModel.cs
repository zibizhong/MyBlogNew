using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogModel;

namespace ViewClassLib
{
    public class ViewForPostIndexModel
    {
        public int counter { get; set; }
        public List<Post> posts { get; set; }

    }
    public class ViewPostModel
    {
        public int ID { get; set; }
        
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public Boolean IsPublish { get; set; }
    }

}