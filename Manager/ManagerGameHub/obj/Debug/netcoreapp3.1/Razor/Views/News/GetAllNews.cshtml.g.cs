#pragma checksum "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "381dffb21bf9ebba2c6eee286c5985746f1df855"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_News_GetAllNews), @"mvc.1.0.view", @"/Views/News/GetAllNews.cshtml")]
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
#nullable restore
#line 1 "G:\GameNews\Manager\ManagerGameHub\Views\_ViewImports.cshtml"
using ManagerGameHub;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\GameNews\Manager\ManagerGameHub\Views\_ViewImports.cshtml"
using ManagerGameHub.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "G:\GameNews\Manager\ManagerGameHub\Views\_ViewImports.cshtml"
using ManagerGameHub.Models.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"381dffb21bf9ebba2c6eee286c5985746f1df855", @"/Views/News/GetAllNews.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d5524cb38e7230cc7527471362c4d14216b4fc72", @"/Views/_ViewImports.cshtml")]
    public class Views_News_GetAllNews : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ManagerGameHub.Models.News>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-sm rounded-circle"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
  
    ViewData["Title"] = "GetAllNews";
    Layout = "~/Views/Shared/_ManagerLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""shadow"">
    <div class=""p-3"">
        <div class=""row"">
            <div class=""col-md-10 col-sm-6"">
                <h1 class=""fw-normal text-dark"">News</h1>
                <h3 class=""text-secondary fw-light"">You can see all the news in with their categories</h3>
            </div>
            <div class=""col-md-2 col-sm-6 text-end "">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "381dffb21bf9ebba2c6eee286c5985746f1df8554590", async() => {
                WriteLiteral("<i class=\"bi bi-plus-lg text-white\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <hr />\r\n        <div class=\"table-responsive\">\r\n            <table class=\"table\">\r\n                <thead>\r\n                    <tr>\r\n                        <th>\r\n                            ");
#nullable restore
#line 25 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                       Write(Html.DisplayNameFor(model => model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            ");
#nullable restore
#line 28 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                       Write(Html.DisplayNameFor(model => model.ImageOne));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            <label>Category</label>\r\n                        </th>\r\n                        <th>\r\n                            ");
#nullable restore
#line 34 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                       Write(Html.DisplayNameFor(model => model.Source));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            ");
#nullable restore
#line 37 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                       Write(Html.DisplayNameFor(model => model.Trend));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th></th>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n");
#nullable restore
#line 43 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>\r\n                                ");
#nullable restore
#line 47 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td class=\"col-md-2\">\r\n                                <img");
            BeginWriteAttribute("src", " src=\"", 1915, "\"", 1964, 2);
            WriteAttributeValue("", 1921, "https://localhost:44352/News/", 1921, 29, true);
#nullable restore
#line 50 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
WriteAttributeValue("", 1950, item.ImageOne, 1950, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid shadow rounded-3 shadow\" />\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 53 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Categories.CategoryName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                <a");
            BeginWriteAttribute("href", " href=\"", 2277, "\"", 2296, 1);
#nullable restore
#line 56 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
WriteAttributeValue("", 2284, item.Source, 2284, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"text-decoration-none text-info\">View source</a>\r\n                            </td>\r\n                            <td>\r\n");
#nullable restore
#line 59 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                                 if (item.Trend)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <i class=\"bi bi-check-circle-fill text-success\"></i>\r\n");
#nullable restore
#line 62 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <i class=\"bi bi-x-circle-fill text-danger\"></i>\r\n");
#nullable restore
#line 66 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 69 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                           Write(Html.ActionLink("Edit", "Edit", new { id = item.Id }, new { @class = "text-decoration-none link-warning" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                ");
#nullable restore
#line 70 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                           Write(Html.ActionLink("Delete", "Delete", new { id = item.Id }, new { @class = "text-decoration-none link-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                               \r\n                                ");
#nullable restore
#line 72 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                           Write(Html.ActionLink("Details", "Details", new { id = item.Id }, new { @class = "text-decoration-none link-info" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n                             \r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 76 "G:\GameNews\Manager\ManagerGameHub\Views\News\GetAllNews.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ManagerGameHub.Models.News>> Html { get; private set; }
    }
}
#pragma warning restore 1591
