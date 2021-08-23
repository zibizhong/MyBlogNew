using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MyBlogNew.Validation;

namespace MyBlogNew.Models
{
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Key]
        [Display(Name = "用户名:")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "密码:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "记住密码")]
        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required]
        [MyValidation]
        [Display(Name = "用户名:")]
        [StringLength(10, ErrorMessage = "{0}的长度必须介于{2}到{1}", MinimumLength = 6)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="{0}的长度需要大于{1}")]
        [Display(Name = "密码:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "确认密码:")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}