using System.Web.Routing;

namespace ZQNB.Common.Themes
{
    /// <summary>
    /// 主题选择器的返回结果
    /// </summary>
    public class ThemeSelectorResult
    {
        /// <summary>
        /// 优先级
        /// 越大越靠前
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 主题的名字
        /// </summary>
        public string ThemeName { get; set; }
    }

    /// <summary>
    /// 主题选择器
    /// </summary>
    public interface IThemeSelector
    {
        /// <summary>
        /// 获取主题
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        ThemeSelectorResult GetTheme(RequestContext requestContext);
    }
}
