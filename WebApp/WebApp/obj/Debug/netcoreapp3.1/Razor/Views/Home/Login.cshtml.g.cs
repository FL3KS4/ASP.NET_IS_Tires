#pragma checksum "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9ea1c8c4530e94cdff96d2fcd670641af32ab966"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Login), @"mvc.1.0.view", @"/Views/Home/Login.cshtml")]
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
#line 1 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\_ViewImports.cshtml"
using WebApp_project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
using WebApp_project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9ea1c8c4530e94cdff96d2fcd670641af32ab966", @"/Views/Home/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b2e586b046cf11980a80c37d4e6b43f7989c0eea", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Login>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
  
    ViewData["Title"] = "Login";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<div class=""container d-flex justify-content-center "">
    <div class=""row"">

        <div class=""card"" style=""width: 21rem; margin-top:2%;"">
            <div class=""card-body"">
                <h5 class=""card-title"">Přihlášení</h5>
                <p class=""card-text"">
");
#nullable restore
#line 17 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                     using (Html.BeginForm())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"mb-3 row\">\r\n                            ");
#nullable restore
#line 20 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                       Write(Html.LabelFor(x => x.login));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <div class=\"col-sm-10\">\r\n                                ");
#nullable restore
#line 22 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                           Write(Html.TextBoxFor(x => x.login));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                <div style=\"color:red;margin-top:5px;\">");
#nullable restore
#line 23 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                                                                  Write(Html.ValidationMessageFor(x => x.login));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                            </div>\r\n                        </div>\r\n");
            WriteLiteral("                        <div class=\"mb-3 row\">\r\n                            ");
#nullable restore
#line 28 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                       Write(Html.LabelFor(x => x.heslo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <div class=\"col-sm-10\">\r\n                                ");
#nullable restore
#line 30 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                           Write(Html.TextBoxFor(x => x.heslo, new { @type = "password" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                <div style=\"color:red;margin-top:5px;\">");
#nullable restore
#line 31 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                                                                  Write(Html.ValidationMessageFor(x => x.heslo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                            </div>\r\n                        </div>\r\n                        <br />\r\n                        <button type=\"submit\" class=\"btn btn-success\">Přihlásit </button>\r\n");
#nullable restore
#line 36 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </p>\r\n            </div>\r\n            <div style=\"width: 19rem; margin-top: 2%;\">\r\n");
#nullable restore
#line 41 "E:\Programming\C# 2\WebApp_project\WebApp_project\Views\Home\Login.cshtml"
               Write(Html.Raw(TempData["msg"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n       \r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Login> Html { get; private set; }
    }
}
#pragma warning restore 1591
