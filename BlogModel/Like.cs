using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogModel
{
    public class Like
    {
        [Key]
        public int ID { get; set; }
        public string LikeUser { get; set; }
        public bool IsLike { get; set; }
        public int CommentId { get; set; }
    }
}
