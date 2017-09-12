using System.Web;
using ZQNB.Common;

[assembly: PreApplicationStartMethod(typeof(ZQNB.Web.DemoModule), "RegisterModules")]

namespace ZQNB.Web
{
    public class DemoModule : IHttpModule
    {
        public static void RegisterModules()
        {
            HttpApplication.RegisterModule(typeof(DemoModule));
        }

        public void Init(HttpApplication context)
        {
            UtilsLogger.LogMessage("DemoModule Init");
        }

        public void Dispose()
        {
            UtilsLogger.LogMessage("DemoModule Dispose");
        }
    }
}
