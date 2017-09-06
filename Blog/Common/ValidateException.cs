using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Common
{
    public class ValidateException : Exception
    {
        public int Code { get; set; }

        public ValidateException(int code, string msg) : base(msg)
        {
            this.Code = code;
        }

    }
}