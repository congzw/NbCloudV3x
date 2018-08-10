using System.Web;
using System.Web.Routing;

namespace ZQNB.Common.Themes
{
    /// <summary>
    /// 支持站的主题，优先顺序：Request => Site => Config(Sg) => Default
    /// </summary>
    public class DefaultThemeSelector : IThemeSelector
    {
        public const string DefaultThemeName = "Default";
        public const string DefaultThemeParamNameOrConfigKey = "Theme";

        /// <summary>
        /// 默认从配置文件和请求地址中获得主题
        /// web.config优先级最低 请求地址中的主题优先级最高
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public ThemeSelectorResult GetTheme(RequestContext requestContext)
        {
            var tsResult = new ThemeSelectorResult { Priority = -9999, ThemeName = DefaultThemeName };
            //优先顺序：Request => Config => Default
            //Request
            var requestTheme = GetRequestTheme(requestContext);
            if (!string.IsNullOrWhiteSpace(requestTheme))
            {
                tsResult.Priority = 9999;
                tsResult.ThemeName = requestTheme;
            }
            else
            {
                var configTheme = GetConfigTheme();
                if (!string.IsNullOrEmpty(configTheme))
                {
                    tsResult.Priority = -9998;
                    tsResult.ThemeName = configTheme;
                }
            }
            //Default
            return tsResult;
        }

        #region helpers
        private static string GetRequestTheme(RequestContext requestContext)
        {
            if (requestContext == null || requestContext.HttpContext == null)
            {
                return null;
            }
            var themeName = requestContext.HttpContext.Request.QueryString[DefaultThemeParamNameOrConfigKey];

            ////fix 嵌套 或者 angular 请求模板的时候 自动调整主题
            //var url = requestContext.HttpContext.Request.UrlReferrer;
            //if (url != null)
            //{
            //    themeName = url.Query(DefaultThemeParamNameOrConfigKey);
            //    if (!string.IsNullOrEmpty(themeName))
            //    {
            //        return themeName;
            //    }
            //}
            return themeName;
        }
        private static string _themeNameInConfig = null;
        private static string GetConfigTheme()
        {
            return _themeNameInConfig ?? (_themeNameInConfig = MyConfigHelper.GetAppSettingValue(DefaultThemeParamNameOrConfigKey, string.Empty));
        }
        #endregion

        public ThemeSelectorResult GetTheme()
        {
            return GetTheme(HttpContext.Current.Request.RequestContext);
        }
    }
}
