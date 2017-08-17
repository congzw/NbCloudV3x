using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using ZQNB.Common.Caching;

namespace ZQNB.Common.Themes
{
    public class ThemeManager : IThemeManager
    {
        /// <summary>
        /// 主题缓存Key
        /// </summary>
        public const string ThemeCacheKey = "ZQNB.Common.Themes.ThemeManager.ThemeCacheKey";
        private readonly IEnumerable<IThemeSelector> _themeSelectors;
        private readonly IPerRequestCacheManager _perRequestCacheManager;

        public ThemeManager(IEnumerable<IThemeSelector> themeSelectors, IPerRequestCacheManager perRequestCacheManager)
        {
            _themeSelectors = themeSelectors;
            _perRequestCacheManager = perRequestCacheManager;
        }

        public ThemeContext GetRequestTheme(RequestContext requestContext)
        {
            if (!_perRequestCacheManager.IsSet(ThemeCacheKey))
            {
                var orderedThemeSelectors = _themeSelectors
                    .Select(x => x.GetTheme(requestContext))
                    .Where(x => x != null)
                    .OrderByDescending(x => x.Priority);

                if (!orderedThemeSelectors.Any())
                    return new ThemeContext { ThemeName = "" };

                var themeContext = new ThemeContext { ThemeName = orderedThemeSelectors.First().ThemeName };
                _perRequestCacheManager.Set(ThemeCacheKey, themeContext);
                return themeContext;
            }
            return _perRequestCacheManager.Get<ThemeContext>(ThemeCacheKey);
        }
    }
}
