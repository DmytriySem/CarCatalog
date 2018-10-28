using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCatalog.Helpers
{
    public static class SelectHelper
    {
        public static MvcHtmlString CreateSelectList(this HtmlHelper html, string[] items)
        {
            TagBuilder select = new TagBuilder("select");
            foreach (string item in items)
            {
                TagBuilder option = new TagBuilder("option");
                option.SetInnerText(item);
                select.InnerHtml += option.ToString();
            }

            return new MvcHtmlString(select.ToString());
        }
    }
}