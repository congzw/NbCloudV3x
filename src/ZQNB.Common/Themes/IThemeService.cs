using System.Web.Routing;

namespace ZQNB.Common.Themes
{
    public interface IThemeManager
    {
        /// <summary>
        /// 获取当前主题
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        ThemeContext GetRequestTheme(RequestContext requestContext);
    }

    public class ThemeContext
    {
        /// <summary>
        /// 主题名称
        /// </summary>
        public string ThemeName { get; set; }
    }
}
