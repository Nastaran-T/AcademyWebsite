using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Core.Convertor
{
    public class FixedText
    {
        //برای حذف فضای خالی و اینکه ایمیل باید به حروف کوچک باشد
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
