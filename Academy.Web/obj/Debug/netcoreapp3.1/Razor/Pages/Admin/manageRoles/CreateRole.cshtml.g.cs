#pragma checksum "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "881f54227a5b6e364953d5b1161c0fee79884dd1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_Admin_manageRoles_CreateRole), @"mvc.1.0.razor-page", @"/Pages/Admin/manageRoles/CreateRole.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Admin/manageRoles/CreateRole.cshtml", typeof(AspNetCore.Pages_Admin_manageRoles_CreateRole), null)]
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
#line 3 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
using Toplearn.DataLayer.Entities.Permission;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"881f54227a5b6e364953d5b1161c0fee79884dd1", @"/Pages/Admin/manageRoles/CreateRole.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Admin_manageRoles_CreateRole : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
  
    ViewData["Title"] = "افزودن نقش جدید";
    List<Permission> permissions = ViewData["Permissions"] as List<Permission>;

#line default
#line hidden
            BeginContext(248, 175, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-lg-12\">\r\n        <h1 class=\"page-header\">افزودن نقش جدید</h1>\r\n    </div>\r\n    <!-- /.col-lg-12 -->\r\n</div>\r\n\r\n<div class=\"row\">\r\n    ");
            EndContext();
            BeginContext(423, 3155, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "881f54227a5b6e364953d5b1161c0fee79884dd14998", async() => {
                BeginContext(443, 342, true);
                WriteLiteral(@"
        <div class=""col-lg-8"">
            <div class=""panel panel-primary"">
                <div class=""panel-heading"">
                    افزودن نقش
                </div>
                <div class=""panel-body"">
                    <div class=""form-group"">
                        <label>نام نقش</label>
                        ");
                EndContext();
                BeginContext(785, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "881f54227a5b6e364953d5b1161c0fee79884dd15730", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 26 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Roles.RoleTitle);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(851, 501, true);
                WriteLiteral(@"
                    </div>


                    <input type=""submit"" value=""ذخیره اطلاعات"" class=""btn btn-outline btn-primary btn-sm"" />

                </div>

            </div>
        </div>
        <div class=""col-md-4"">
            <div class=""panel panel-info"">
                <div class=""panel-heading"">
                    دسترسی های نقش
                </div>
                <!-- /.panel-heading -->
                <div class=""panel-body"">

                    <ul>
");
                EndContext();
#line 45 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                         foreach (var permission in permissions.Where(p => p.ParentId == null))
                        {

#line default
#line hidden
                BeginContext(1476, 115, true);
                WriteLiteral("                            <li>\r\n                                <input type=\"checkbox\" name=\"SelectedPermissions\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 1591, "\"", 1623, 1);
#line 48 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
WriteAttributeValue("", 1599, permission.PermissionId, 1599, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1624, 37, true);
                WriteLiteral(" />\r\n                                ");
                EndContext();
                BeginContext(1662, 26, false);
#line 49 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                           Write(permission.PermissionTitle);

#line default
#line hidden
                EndContext();
                BeginContext(1688, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
#line 51 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                 if (permissions.Any(p => p.ParentId == permission.ParentId))
                                {

#line default
#line hidden
                BeginContext(1822, 42, true);
                WriteLiteral("                                    <ul>\r\n");
                EndContext();
#line 54 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                         foreach (var sub in permissions.Where(p => p.ParentId == permission.PermissionId))
                                        {

#line default
#line hidden
                BeginContext(2032, 147, true);
                WriteLiteral("                                            <li>\r\n                                                <input type=\"checkbox\" name=\"SelectedPermissions\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2179, "\"", 2204, 1);
#line 57 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
WriteAttributeValue("", 2187, sub.PermissionId, 2187, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2205, 53, true);
                WriteLiteral(" />\r\n                                                ");
                EndContext();
                BeginContext(2259, 19, false);
#line 58 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                           Write(sub.PermissionTitle);

#line default
#line hidden
                EndContext();
                BeginContext(2278, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
#line 60 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                                 if (permissions.Any(p=>p.ParentId==sub.ParentId))
                                                {

#line default
#line hidden
                BeginContext(2433, 58, true);
                WriteLiteral("                                                    <ul>\r\n");
                EndContext();
#line 63 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                                         foreach (var sub2 in permissions.Where(p=>p.ParentId==sub.PermissionId))
                                                        {

#line default
#line hidden
                BeginContext(2681, 171, true);
                WriteLiteral("                                                        <li>\r\n                                                            <input type=\"checkbox\" name=\"SelectedPermissions\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2852, "\"", 2878, 1);
#line 66 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
WriteAttributeValue("", 2860, sub2.PermissionId, 2860, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2879, 65, true);
                WriteLiteral(" />\r\n                                                            ");
                EndContext();
                BeginContext(2945, 20, false);
#line 67 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                                       Write(sub2.PermissionTitle);

#line default
#line hidden
                EndContext();
                BeginContext(2965, 65, true);
                WriteLiteral("\r\n                                                        </li>\r\n");
                EndContext();
#line 69 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                                        }

#line default
#line hidden
                BeginContext(3089, 59, true);
                WriteLiteral("                                                    </ul>\r\n");
                EndContext();
#line 71 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                                }

#line default
#line hidden
                BeginContext(3199, 53, true);
                WriteLiteral("\r\n                                            </li>\r\n");
                EndContext();
#line 74 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                                        }

#line default
#line hidden
                BeginContext(3295, 43, true);
                WriteLiteral("                                    </ul>\r\n");
                EndContext();
#line 76 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"

                                }

#line default
#line hidden
                BeginContext(3375, 35, true);
                WriteLiteral("                            </li>\r\n");
                EndContext();
#line 79 "D:\Project2020\TopLearnWebsite-Core\TopLearnWebsite\TopLearn.Web\Pages\Admin\manageRoles\CreateRole.cshtml"
                        }

#line default
#line hidden
                BeginContext(3437, 134, true);
                WriteLiteral("                    </ul>\r\n\r\n                </div>\r\n                <!-- /.panel-body -->\r\n            </div>\r\n\r\n        </div>\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3578, 12, true);
            WriteLiteral("\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TopLearn.Web.Pages.Admin.manageRoles.CreateRoleModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TopLearn.Web.Pages.Admin.manageRoles.CreateRoleModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TopLearn.Web.Pages.Admin.manageRoles.CreateRoleModel>)PageContext?.ViewData;
        public TopLearn.Web.Pages.Admin.manageRoles.CreateRoleModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
