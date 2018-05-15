using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_5_ASP_MVC.AppCode
{
    public class VkTagHelper : TagHelper
    {
        private const string address = "https://vk.com/metanit";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // заменяет тег <vk> тегом <a>
                                     // присваивает атрибуту href значение из address
            output.Attributes.SetAttribute("href", address);
            output.Content.SetContent("Группа в ВК");
        }
    }
}
