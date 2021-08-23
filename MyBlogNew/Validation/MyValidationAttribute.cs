using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBlogNew.Validation
{
    public class MyValidationAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string data = value.ToString();
            if (string.IsNullOrEmpty(data))
            {
                ErrorMessage = "{0}字符不能为空";
                return false;
            }
            if (!data.Equals("Admin"))
            {
                ErrorMessage = "{0}不能为Admin";
                return false;
            }
            if (data.Length < 6 || data.Length > 20)
            {
                ErrorMessage = "{0}的长度应应介于{1}至{2}之间";
                return false;
            }
            return true;
        }
    }
}