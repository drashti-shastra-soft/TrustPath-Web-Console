using TrustPathWebConsole.Infrastructure;
using TrustPathWebConsole.filter;
using System.Web.Mvc;

namespace TrustPathWebConsole.Controllers
{
    [Authentication]
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            HttpContextHelper.LoginUser = null;
            return RedirectToAction("Index", "Authentication");
        }
    }
}