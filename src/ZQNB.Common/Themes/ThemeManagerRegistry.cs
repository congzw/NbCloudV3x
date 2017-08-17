using System;
using System.Collections.Generic;
using System.Web;
using ZQNB.Common.Caching;
using ZQNB.Common.Ioc;

namespace ZQNB.Common.Themes
{
    public class ThemeManagerRegistry : NbServiceRegistryBase<ThemeManagerRegistry, IThemeManager>
    {
        public override Func<IThemeManager> GetDefaultServiceFunc()
        {
            return () => new ThemeManager(new List<IThemeSelector> { new DefaultThemeSelector() }, new PerRequestCacheManager(new HttpContextWrapper(HttpContext.Current)));
        }
    }
}