using System.Collections.Generic;
using System.Web.Mvc;

namespace ZQNB.Common.Themes.Web.Mvc
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        //todo fix Unify/_Layout => _Layout bug!
        //public IList<string> FindLayoutsTrace = new List<string>();

        //public override string Layout
        //{
        //    get
        //    {
        //        var layout = base.Layout;
        //        if (!string.IsNullOrEmpty(layout))
        //        {
        //            var filename = System.IO.Path.GetFileNameWithoutExtension(layout);
        //            //ViewEngineResult viewResult = ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");
        //            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ViewContext.Controller.ControllerContext, filename);

        //            if (viewResult.View != null && viewResult.View is RazorView)
        //            {
        //                layout = ((RazorView) viewResult.View).ViewPath;
        //            }
        //        }

        //        UtilsLogger.LogMessage(string.Format("try find layout: {0}: {1} => {2}", this.VirtualPath, base.Layout, layout));
        //        return layout;
        //    }
        //    set
        //    {
        //        base.Layout = value;
        //    }
        //}
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}