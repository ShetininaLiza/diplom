#pragma checksum "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d7baf31c769960d16351c1b01dd2f7d9d8d380b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reviewer_Index), @"mvc.1.0.view", @"/Views/Reviewer/Index.cshtml")]
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
#line 1 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\_ViewImports.cshtml"
using WebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\_ViewImports.cshtml"
using WebApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d7baf31c769960d16351c1b01dd2f7d9d8d380b", @"/Views/Reviewer/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa0ef8da47a84ffb33e8bc853509aa4fa5703a26", @"/Views/_ViewImports.cshtml")]
    public class Views_Reviewer_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BisnessLogic.Models.Publication>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("navbar-brand"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<script>
    function DownloadFile(id) {
        //Swal.fire(""id: ""+id)
        $.post(""/Reviewer/DownloadAndUpdate"", { ID: id }, function (data) {
            let a = document.createElement(""a"");
            let blob = new Blob([data]);
            a.href = URL.createObjectURL(blob);
            a.download = ""publication_""+id+"".txt"";
            a.click();
        });
    }
    
</script>
<script>
    
</script>
<div class=""text-center"">
    <header>
        <nav class=""navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d7baf31c769960d16351c1b01dd2f7d9d8d380b4972", async() => {
                WriteLiteral("Научный журнал");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target="".navbar-collapse"" aria-controls=""navbarSupportedContent""
                    aria-expanded=""false"" aria-label=""Toggle navigation"">
                <span class=""navbar-toggler-icon""></span>
            </button>
            <ul class=""navbar-nav flex-grow-1 topmenu"">
                <li class=""nav-item"">
                    <a class=""submenu-link nav-link text-dark"" href=""../User/Enter"">Выход</a>
                </li>
            </ul>
        </nav>
    </header>
    <h1>Добро пожаловать</h1>
    <h2>Рецензент</h2>
    <table class=""table table-primary"">
        <thead>
            <tr>
                <th>№</th>
                <th>Название статьи</th>
                <th>Дата подачи</th>
                <th>Статус</th>
                <th> </th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 48 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
             foreach (var str in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 51 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
                   Write(str.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><a");
            BeginWriteAttribute("href", " href=\"", 1824, "\"", 1857, 2);
            WriteAttributeValue("", 1831, "/Home/GetPublic?Id=", 1831, 19, true);
#nullable restore
#line 52 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
WriteAttributeValue("", 1850, str.Id, 1850, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />");
#nullable restore
#line 52 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
                                                          Write(str.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 53 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
                   Write(str.DateCreate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 54 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
                   Write(str.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 55 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
                     if (str.Status == BisnessLogic.Models.Status.Отправлена_рецензенту.ToString())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>\r\n                            <button class=\"btn btn-lg btn-primary\" type=\"button\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2200, "\"", 2231, 3);
            WriteAttributeValue("", 2210, "DownloadFile(", 2210, 13, true);
#nullable restore
#line 58 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
WriteAttributeValue("", 2223, str.Id, 2223, 7, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2230, ")", 2230, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Скачать</button>\r\n                        </td>\r\n");
#nullable restore
#line 60 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
                    }
                    else if (str.Status == BisnessLogic.Models.Status.Находится_на_рецензировании.ToString())
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 2499, "\"", 2539, 2);
            WriteAttributeValue("", 2506, "/Reviewer/CreateReview?Id=", 2506, 26, true);
#nullable restore
#line 64 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
WriteAttributeValue("", 2532, str.Id, 2532, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Написать рецензию</a>\r\n                        </td>\r\n");
#nullable restore
#line 66 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tr>\r\n");
#nullable restore
#line 68 "C:\Users\sheti\source\repos\diplom\WebApplication\Views\Reviewer\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BisnessLogic.Models.Publication>> Html { get; private set; }
    }
}
#pragma warning restore 1591
