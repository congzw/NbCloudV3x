using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ZQNB.Common.Themes.Web.Mvc
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public override void ExecutePageHierarchy()
        {
            var showVirtualPath = WebViewPageHelper.ShowVirtualPath();
            if (showVirtualPath)
            {
                //just for debug
                this.WriteLiteral("\r\n<div style='color:red'>" + this.VirtualPath + "</div>");
                this.WriteLiteral("<!--Begin-ViewPath: " + this.VirtualPath + "-->\r\n");
            }

            base.ExecutePageHierarchy();
            
            if (showVirtualPath)
            {
                this.WriteLiteral("\r\n<!--End-ViewPath: " + this.VirtualPath + "-->");
            }
        }
        
        public override string Layout
        {
            get
            {
                return ProcessThemeLayout(base.Layout);
            }
            set
            {
                base.Layout = value;
            }
        }
        
        private string _themeLayout = null;
        private string ProcessThemeLayout(string layout)
        {
            //没有指定模板的不需要处理
            if (string.IsNullOrWhiteSpace(layout))
            {
                return layout;
            }

            //已经处理了,避免重复处理
            if (!string.IsNullOrWhiteSpace(_themeLayout))
            {
                return _themeLayout;
            }

            var filename = ThemeLayoutProcessor.GuessLayout(layout);
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");
            if (viewResult.View != null && viewResult.View is RazorView)
            {
                layout = ((RazorView) viewResult.View).ViewPath;
            }
            LogMessage(string.Format("try find theme layout: {0}: {1} => {2}", this.VirtualPath, base.Layout, layout));
            _themeLayout = layout;
            return _themeLayout;
        }
        private void LogMessage(string message)
        {
            UtilsLogger.LogMessage(message);
        }
    }
    
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }

    public class ThemeLayoutProcessor
    {
        public static string GuessLayout(string layout)
        {
            return GuessLayoutFunc(layout);
        }

        static ThemeLayoutProcessor()
        {
            GuessLayoutFunc = GuessLayoutPath;
        }

        public static Func<string, string> GuessLayoutFunc { get; set; }


        private static string GuessLayoutPath(string layout)
        {
            //~/Views/Home/_Layout.cshtml => _Layout 【OK】
            //~/Views/Shared/_Layout.cshtml => _Layout 【OK】
            //~/Views/Shared/Unify/_Layout.cshtml => _Layout【KO】 => Unify/_Layout 
            //~/Views/Shared/Unify/A/B/_Layout.cshtml => _Layout【KO】 => Unify/A/B/_Layout 
            //~/Themes/Theme02/Views/Shared/_Layout.cshtml => Views/Shared/_Layout 【KO】
            //~/Themes/Theme01/Areas/Demos/Views/Home/Index.cshtml: ~/Views/Shared/_Layout.cshtml => ~/Views/Home/_Layout.cshtml 

            //Layout = "~/Areas/Demos/Views/Shared/_Layout.cshtml";
            //Layout = "~/Areas/Demos/Views/Home/_Layout.cshtml";

            if (string.IsNullOrWhiteSpace(layout))
            {
                return layout;
            }

            if (layout.StartsWith("~/Themes", StringComparison.OrdinalIgnoreCase))
            {
                //违反了主题独立运行的约定！
                throw new InvalidOperationException("非法的操作，不能直接指定主题中的Layout路径！");
            }

            var filename = System.IO.Path.GetFileNameWithoutExtension(layout);
            var splits = layout.Split('/');
            if (splits.Length < 5)
            {
                return filename;
            }

            //[Unify, _Layout.cshtml] => [Unify] 
            var middles = splits.Skip(3).Take(splits.Length - 3 - 1).ToList();
            var result = middles.Aggregate(string.Empty, (current, split) => current + split + "/");
            result += filename;
            LogMessage(string.Format("GuessLayout: {0} => {1}", layout, result));
            return result;
        }
        private static void LogMessage(string message)
        {
            UtilsLogger.LogMessage(message);
        }
    }

    internal static class WebViewPageHelper
    {
        private static string Config_Common_ShowVirtualPath = "Config.Common.ShowVirtualPath";
        private static bool? _showVirtualPath = null;

        public static bool ShowVirtualPath()
        {
            if (_showVirtualPath == null)
            {
                _showVirtualPath = MyConfigHelper.GetAppSettingValueAsBool(Config_Common_ShowVirtualPath, false);
            }
            return _showVirtualPath.Value;
        }
    }
}