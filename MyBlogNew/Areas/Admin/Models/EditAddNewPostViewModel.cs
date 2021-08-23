using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyBlogNew.Areas.Admin.Models
{
    public class EditAddNewPostViewModel
    {
        [Display(Name ="发布")]
        public Boolean IsPublish { get; set; }
        public string PostTarget { get; set; }
        [Key]
        public int Post_ID { get; set; }
        [Display(Name ="标题")]
        public string Title { get; set; }
        [AllowHtml]
        [Display(Name ="内容")]
        public string Content { get; set; }
        [Display(Name ="作者")]
        public string Author { get; set; }
        [Display(Name ="添加时间")]
        public DateTime CreateTime { get; set; }
    }
}