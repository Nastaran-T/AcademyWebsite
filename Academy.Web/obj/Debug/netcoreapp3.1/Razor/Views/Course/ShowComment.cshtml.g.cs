#pragma checksum "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79adb46ac9db818e7efae0187e46b7557c8d5838"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Course_ShowComment), @"mvc.1.0.view", @"/Views/Course/ShowComment.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Course/ShowComment.cshtml", typeof(AspNetCore.Views_Course_ShowComment))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
using Toplearn.Core.Convertors;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79adb46ac9db818e7efae0187e46b7557c8d5838", @"/Views/Course/ShowComment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/Views/_ViewImports.cshtml")]
    public class Views_Course_ShowComment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<List<Toplearn.DataLayer.Entities.Course.CourseComment>, int>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(136, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 7 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
 foreach (var item in Model.Item1)
{

#line default
#line hidden
            BeginContext(177, 61, true);
            WriteLiteral("    <!-- row -->\r\n    <div class=\"comment-row\">\r\n        <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 238, "\"", 277, 2);
            WriteAttributeValue("", 244, "/AvatorUser/", 244, 12, true);
#line 11 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
WriteAttributeValue("", 256, item.User.UserAvatar, 256, 21, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(278, 51, true);
            WriteLiteral(">\r\n        <div class=\"left-col\">\r\n            <h3>");
            EndContext();
            BeginContext(330, 18, false);
#line 13 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
           Write(item.User.UserName);

#line default
#line hidden
            EndContext();
            BeginContext(348, 25, true);
            WriteLiteral("</h3>\r\n            <span>");
            EndContext();
            BeginContext(374, 26, false);
#line 14 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
             Write(item.CreateDate.ToShamsi());

#line default
#line hidden
            EndContext();
            BeginContext(400, 42, true);
            WriteLiteral("</span>\r\n            <p>\r\n                ");
            EndContext();
            BeginContext(443, 12, false);
#line 16 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
           Write(item.Comment);

#line default
#line hidden
            EndContext();
            BeginContext(455, 67, true);
            WriteLiteral("\r\n            </p>\r\n        </div>\r\n    </div>\r\n    <!-- /row -->\r\n");
            EndContext();
#line 21 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
}

#line default
#line hidden
            BeginContext(525, 94, true);
            WriteLiteral("\r\n\r\n<nav aria-label=\"Page navigation\">\r\n    <ul class=\"pagination justify-content-center\">\r\n\r\n");
            EndContext();
#line 27 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
         for (int i = 1; i <=Model.Item2 ; i++)
        {

#line default
#line hidden
            BeginContext(679, 54, true);
            WriteLiteral("            <li class=\"page-item\"><a class=\"page-link\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 733, "\"", 758, 3);
            WriteAttributeValue("", 743, "PageComment(", 743, 12, true);
#line 29 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
WriteAttributeValue("", 755, i, 755, 2, false);

#line default
#line hidden
            WriteAttributeValue("", 757, ")", 757, 1, true);
            EndWriteAttribute();
            BeginContext(759, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(761, 1, false);
#line 29 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
                                                                            Write(i);

#line default
#line hidden
            EndContext();
            BeginContext(762, 11, true);
            WriteLiteral("</a></li>\r\n");
            EndContext();
#line 30 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Views\Course\ShowComment.cshtml"
        }

#line default
#line hidden
            BeginContext(784, 21, true);
            WriteLiteral("\r\n    </ul>\r\n</nav>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<List<Toplearn.DataLayer.Entities.Course.CourseComment>, int>> Html { get; private set; }
    }
}
#pragma warning restore 1591
