using TrustPathWebConsole.Infrastructure.IService;
using TrustPathWebConsole.Infrastructure;
using TrustPathWebConsole.Helper;
using TrustPathWebConsole.filter;
using System.Web.Mvc;
using System.Dynamic;
using System;

namespace TrustPathWebConsole.Controllers
{
    [Authentication]
    public class UserController : Controller
    {

        private readonly IUserService _service;
        private readonly IDropdownService _dropdownService;

        public UserController(IUserService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            return View(new Domain.UserLister());
        }

        public ActionResult List(Domain.UserLister mLister)
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
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();

            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            return PartialView("_Create", new Domain.User());
        }

        [HttpPost]
        public ActionResult Create(Domain.User mUser)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    Infrastructure.Helper.DefaultSetter.ApplyDefaults(mUser);

                mUser = StaticMethods.SetAuditOnUpsert(mUser);
                mUser = _service.CreateAsync(mUser).GetAwaiter().GetResult();

                
                bool success = mUser != null && mUser.Id > 0;
                if (mUser != null && mUser.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "User has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving user!";
                }
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Status = _dropdownService.GetStatusOptions();
            }

            return PartialView("_Create", mUser);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mUser = new Domain.User();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mUser = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            }

            return PartialView("_Create", mUser);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "User has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting user!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckEmailAddressExist(Domain.User mUser)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                var isExist = _service.CheckEmailAddressExistAsync(mUser).GetAwaiter().GetResult();
                resultData = new { StatusCode = "Ok", IsExist = isExist };
            }
            catch (UnauthorizedAccessException)
            {
                //resultData = new { StatusCode = "Unauthorized" };
                return PartialView("_LoginPrompt");
            }
            catch (BadRequestException ex)
            {
                //resultData = new { StatusCode = "BadRequest", NotificationType = "error", NotificationMsg = ex.Message };
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = ex.Message;
            }
            catch (Exception)
            {
                //resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting User!" };
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
    }
}