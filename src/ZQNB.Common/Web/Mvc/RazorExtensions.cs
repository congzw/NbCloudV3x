using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ZQNB.Common.Web.Mvc
{
    public static class RazorExtensions
    {
        //how to use?
        //@*<link href="~/Content/css/bootstrap.css" rel="stylesheet"/>*@
        //@Html.CssTag("~/Content/css/bootstrap.css")
        //@Url.CssTag("~/Content/css/bootstrap.css")

        public static IHtmlString ScriptTag(this HtmlHelper htmlHelper, string url, bool render = true)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return ScriptTag(urlHelper, url, render);
        }

        public static IHtmlString ScriptTag(this UrlHelper urlHelper, string url, bool render = true)
        {
            if (!render || string.IsNullOrWhiteSpace(url))
            {
                return new HtmlString(string.Empty);
            }

            var script = new TagBuilder("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = ProcessUrl(url);
            return new HtmlString(script.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString CssTag(this HtmlHelper htmlHelper, string url, bool render = true)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return CssTag(urlHelper, url, render);
        }

        public static IHtmlString CssTag(this UrlHelper urlHelper, string url, bool render = true)
        {
            if (!render || string.IsNullOrWhiteSpace(url))
            {
                return new HtmlString(string.Empty);
            }

            var script = new TagBuilder("link");
            script.Attributes["rel"] = "stylesheet";
            script.Attributes["href"] = ProcessUrl(url);
            return new HtmlString(script.ToString(TagRenderMode.SelfClosing));
        }

        public static HelperResult List<T>(this IEnumerable<T> items, Func<T, HelperResult> template)
        {
            return new HelperResult(writer =>
            {
                foreach (var item in items)
                {
                    template(item).WriteTo(writer);
                }
            });
        }

        private static string ProcessUrl(string url)
        {
            if (url.StartsWith("~"))
            {
                return url;
            }
            if (url.StartsWith("/"))
            {
                return "~" + url;
            }
            return url;
        }
    }
}
