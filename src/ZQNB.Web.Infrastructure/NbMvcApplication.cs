using System;
using System.Web.Mvc;
using System.Web.Routing;
using ZQNB.Common;
using ZQNB.Common.Themes.Web.Mvc;
using ZQNB.Common.Web.Mvc;

namespace ZQNB.Web
{
    public class NbMvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ////filters
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //GlobalConfiguration.Configure(WebApiConfig.RegisterGlobalFilters);

            //routes for mvc
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Area级别的注册(webapi and mvc)
            NbAreaRegistration.RegisterAllAreasInvoke();
            ////routes for webapi
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            //remove all view engines
            //except the themeable razor view engine we use
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeViewEngine());

            //Log("====Application_Start Tasks Begin!====");
            //var tasks = CoreServiceProvider.Current.GetAllInstances<IApplicationStartTask>();
            //foreach (var task in tasks)
            //{
            //    task.Execute();
            //}
            //Log("====Application_Start Tasks End!====");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (hasRequestBeginTasks == null)
            //{
            //    var count = CoreServiceProvider.Current.GetAllInstances<IRequestBeginTask>().Count();
            //    hasRequestBeginTasks = count > 0;
            //    Log(string.Format("====IRequestBeginTask Impl Count: {0} ====", count));
            //}

            //if (hasRequestBeginTasks.Value)
            //{
            //    Log("====Application_BeginRequest Tasks Begin!====");
            //    var tasks = CoreServiceProvider.Current.GetAllInstances<IRequestBeginTask>();
            //    foreach (var task in tasks)
            //    {
            //        task.Execute();
            //    }
            //    Log("====Application_BeginRequest Tasks End!====");
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //todo
        }

        protected void Application_EndRequest()
        {
            //if (hasRequestEndTasks == null)
            //{
            //    var count = CoreServiceProvider.Current.GetAllInstances<IRequestEndTask>().Count();
            //    hasRequestEndTasks = count > 0;
            //    Log(string.Format("====IRequestEndTask Impl Count: {0} ====", count));
            //}

            //if (hasRequestEndTasks.Value)
            //{
            //    Log("====Application_EndRequest Tasks Begin!====");
            //    var tasks = CoreServiceProvider.Current.GetAllInstances<IRequestEndTask>();
            //    foreach (var task in tasks)
            //    {
            //        task.Execute();
            //    }
            //    Log("====Application_EndRequest Tasks End!====");
            //}
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //todo
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //todo
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //todo
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //todo
        }

        private static void Log(string message)
        {
            UtilsLogger.LogMessage("Global", message);
        }

        private static bool? hasRequestBeginTasks = null;
        private static bool? hasRequestEndTasks = null;
    }
}
