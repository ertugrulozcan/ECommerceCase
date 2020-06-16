#pragma checksum "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "11fbaa1af94c37a8013c79d9e16016ce494d1fc7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Header), @"mvc.1.0.view", @"/Views/Shared/_Header.cshtml")]
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
#line 1 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/_ViewImports.cshtml"
using ECommerceCase.WebClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/_ViewImports.cshtml"
using ECommerceCase.WebClient.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
using ECommerceCase.WebClient.Models.Auth;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"11fbaa1af94c37a8013c79d9e16016ce494d1fc7", @"/Views/Shared/_Header.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e128f1caa22b9b985272840e5ee7ca7d85ffc6c1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Header : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("icon icon-profile"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Auth", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Logout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-left: 10px; font-family: \'Roboto\', sans-serif; font-size: 18px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Login", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
  
	UserModel LoggedInUser = null;
	

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
     if (User.Identity.IsAuthenticated)
	{
		LoggedInUser = new UserModel
		{
			Id = User.Claims.FirstOrDefault(p => p.Type == "id")?.Value,
			Username = User.Claims.FirstOrDefault(p => p.Type == "username")?.Value,
			Email = User.Claims.FirstOrDefault(p => p.Type == "email")?.Value,
			FirstName = User.Claims.FirstOrDefault(p => p.Type == "firstName")?.Value,
			LastName = User.Claims.FirstOrDefault(p => p.Type == "lastName")?.Value
		};
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<header class=\"w3-container w3-xlarge\" style=\"margin-top: 30px; margin-bottom: 20px;\">\n\t<p class=\"w3-left\">ECommerceCase</p>\n\t\n\t<div class=\"w3-right\">\n\t\t<div style=\"display: inline-block;\">\n\t\t\t");
#nullable restore
#line 23 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
       Write(await Component.InvokeAsync("ShoppingCartButton"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\n        </div>\n\t\t\n\t\t<div style=\"display: inline-block;\">\n");
#nullable restore
#line 27 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
             if (User.Identity.IsAuthenticated)
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<i class=\"fa fa-user\"><span style=\"margin-left: 10px; font-family: \'Roboto\', sans-serif; font-size: 18px;\">");
#nullable restore
#line 29 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
                                                                                                                      Write(LoggedInUser.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></i>\n");
            WriteLiteral("\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "11fbaa1af94c37a8013c79d9e16016ce494d1fc77088", async() => {
                WriteLiteral("\n\t\t\t\t\t<span>Logout</span>\n\t\t\t\t");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 34 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
			}
			else
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<i class=\"fa fa-user\">\n\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "11fbaa1af94c37a8013c79d9e16016ce494d1fc78883", async() => {
                WriteLiteral("\n\t\t\t\t\t\t<span>Login</span>\n\t\t\t\t\t");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\t\t\t\t</i>\n");
#nullable restore
#line 42 "/Users/admin/Projects/ECommerceCase/ECommerceCase.WebClient/Views/Shared/_Header.cshtml"
			}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t</div>\n\t</div>\n</header>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
