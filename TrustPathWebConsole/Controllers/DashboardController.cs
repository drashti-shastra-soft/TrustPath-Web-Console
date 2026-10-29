using System;
using System.Web.Mvc;
using TrustPathWebConsole.filter;
using TrustPathWebConsole.Helper;
using TrustPathWebConsole.Infrastructure;
using TrustPathWebConsole.Infrastructure.IService;
using static TrustPathWebConsole.Infrastructure.Enums;

namespace TrustPathWebConsole.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View();
        }


    }
}