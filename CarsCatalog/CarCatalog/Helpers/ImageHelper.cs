using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCatalog.Helpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString GetImageByte(this HtmlHelper html, byte[] photo)
        {
            TagBuilder img = new TagBuilder("img");
            img.AddCssClass("data-brand-img");

            if (photo != null && photo.Length > 0)
            {
                img.MergeAttribute("src", "data:image/jpeg;base64," + Convert.ToBase64String(photo));
            }
            else
            {
                img.MergeAttribute("src", "/Content/Images/download.png");
            }

            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString GetImageString(this HtmlHelper html, string photoUrl)
        {
            TagBuilder img = new TagBuilder("img");
            img.AddCssClass("data-model-img");

            if (photoUrl != null && photoUrl.Length > 0)
            {
                img.MergeAttribute("src", "/Content/Images/Models/" + photoUrl);
            }
            else
            {
                img.MergeAttribute("src", "/Content/Images/noModelImage.png");
            }

            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString GetImageByteHtml(byte[] photo)
        {
            TagBuilder img = new TagBuilder("img");
            img.AddCssClass("data-brand-img");

            if (photo != null && photo.Length > 0)
            {
                img.MergeAttribute("src", "data:image/jpeg;base64," + Convert.ToBase64String(photo));
            }
            else
            {
                img.MergeAttribute("src", "/Content/Images/download.png");
            }

            return new MvcHtmlString(img.ToString());
        }
    }
}