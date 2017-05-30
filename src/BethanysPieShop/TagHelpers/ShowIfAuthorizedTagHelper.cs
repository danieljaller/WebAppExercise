using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers
{
    public class ShowIfAuthorizedTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShowIfAuthorizedTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                output.Content.SetHtmlContent("<img src=\"\\Images\\happy.png\" style=\"width:150px;height:150px;\"><br/><p>You´re logged in!</p>");
                output.TagMode = TagMode.StartTagAndEndTag;
            }
            else
            {
                output.Content.SetHtmlContent("<img src=\"\\Images\\tired.png\" style=\"width:150px;height:150px;\"><br/><p>You´re not logged in!</p>");
                output.TagMode = TagMode.StartTagAndEndTag;
            }
        }
    }
}
