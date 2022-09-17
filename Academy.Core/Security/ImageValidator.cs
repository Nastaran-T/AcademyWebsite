using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Core.Security
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                //اینجا در واقع فایلمون انتخابی را تبدیل میکنیم ب ایمیج که مطمين بشیم عکس باشه
                var img = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
