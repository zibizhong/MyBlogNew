using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBlogNew.Validation
{
    public class MyMinLengthAttribute : ValidationAttribute
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public MyMinLengthAttribute(int MaxLen,int MinLen)
        {
            MaxLength=MaxLen;
            MinLength=MinLen;
        }
        public override bool IsValid(object value)
        {
            var data = value as string;
            
            if (data.Length > MaxLength||data.Length<MinLength)
            {
                ErrorMessage = "{0}的长度为"+MinLength +"至"+ MaxLength+"之间";
                return false;
            }
            
            return true;
        }
    }
}