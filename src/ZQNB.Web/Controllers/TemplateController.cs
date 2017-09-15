using System.Web.Mvc;

namespace ZQNB.Web.Controllers
{
    public class TemplateController : Controller
    {
        public ActionResult Index(string view = null)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                return View();
            }
            return View(view);
        }
    }
}