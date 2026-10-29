using System;
using System.Dynamic;
using System.Web.Mvc;
using TrustPathWebConsole.filter;
using TrustPathWebConsole.Helper;
using TrustPathWebConsole.Infrastructure;
using TrustPathWebConsole.Infrastructure.IService;

namespace TrustPathWebConsole.Controllers
{
    [Authentication]
    public class SourceController : Controller
    {
        private readonly ISourceService _service;
        private readonly IDropdownService _dropdownService;

        public SourceController(ISourceService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            return View(new Domain.SourceLister());
        }

        public ActionResult List(Domain.SourceLister mLister)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                else
                    mLister.SearchCriteria.CompanyId = HttpContextHelper.LoginUser.CompanyId;
                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();
            }
            catch
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Something went wrong!";
            }
            finally
            {
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            System.Threading.Thread.Sleep(1000);

            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
            {
                ViewBag.ShowCompany = true;
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", new Domain.Source());
        }

        [HttpPost]
        public ActionResult Create(Domain.Source mSource)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    mSource.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                Infrastructure.Helper.DefaultSetter.ApplyDefaults(mSource);
                mSource = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mSource);

                mSource = _service.CreateAsync(mSource).GetAwaiter().GetResult();

                bool success = mSource != null && mSource.Id > 0;
                ViewBag.NotificationType = success
                        ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                        : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                        ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Source).StrToLower())
                        : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Source).StrToLower());

                return base.PartialView("_Create", (object)mSource);
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
                return PartialView("_Create", mSource);
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mSource = new Domain.Source();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mSource = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.ShowCompany = false;
                ViewBag.Companies = string.Empty;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                {
                    ViewBag.ShowCompany = true;
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                }

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            }
            return PartialView("_Create", mSource);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Source has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting source!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

    }
}