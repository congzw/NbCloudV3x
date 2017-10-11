using System;
using System.Web.Mvc;

namespace ZQNB.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string view = null)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                return View();
            }
            return View(view);
        }

        public ActionResult Error()
        {
            throw new ApplicationException("some error");
        }
    }
}
