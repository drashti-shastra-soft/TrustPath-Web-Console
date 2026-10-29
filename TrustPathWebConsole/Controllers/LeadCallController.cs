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
    public class LeadCallController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeadCallService _service;

        public LeadCallController(IDropdownService dropdownService, ILeadCallService leadCallService)
        {
            _dropdownService = dropdownService;
            _service = leadCallService;
        }

        public ActionResult Index(int id)
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            if (id <= default(int))
                return RedirectToAction("Index", "Lead");

            var mLister = new Domain.LeadCallLister();
            mLister.SearchCriteria.LeadId = id;
            TempData["LeadId"] = id;
            return View(mLister);
        }

        public ActionResult List(Domain.LeadCallLister mLister)
        {
            try
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                else
                    mLister.SearchCriteria.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                if (Convert.ToInt32(TempData.Peek("LeadId")) <= 0)
                {
                    return RedirectToAction("Index", "Lead");
                }

                mLister.SearchCriteria.LeadId = Convert.ToInt32(TempData.Peek("LeadId"));

                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }
            finally
            {
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                ViewBag.CommunicationModes = _dropdownService.GetCommunicationModeOptions();

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }

            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create(int leadId)
        {
            var leadCall = new Domain.LeadCall();
            leadCall.LeadId = leadId;

            ViewBag.CommunicationModes = _dropdownService.GetCommunicationModeOptions();
            ViewBag.CallStatus = _dropdownService.GetCallStatusOptions();

            return PartialView("_Create", leadCall);
        }

        [HttpPost]
        public ActionResult Create(Domain.LeadCall mLeadCall)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    mLeadCall.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                Infrastructure.Helper.DefaultSetter.ApplyDefaults(mLeadCall);
                mLeadCall = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mLeadCall);

                mLeadCall = _service.CreateAsync(mLeadCall).GetAwaiter().GetResult();

                bool success = mLeadCall != null && mLeadCall.Id > 0;
                ViewBag.NotificationType = success
                        ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                        : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                        ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Call).StrToLower())
                        : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Call).StrToLower());

                return base.PartialView("_Create", (object)mLeadCall);
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
                return PartialView("_Create", mLeadCall);
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                ViewBag.CommunicationModes = _dropdownService.GetCommunicationModeOptions();
                ViewBag.CallStatus = _dropdownService.GetCallStatusOptions();
            }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mLeadCall = new Domain.LeadCall();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mLeadCall = _service.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.LeadStatus = _dropdownService.GetLeadStatusOptions();
                ViewBag.CallStatus = _dropdownService.GetCallStatusOptions();
                ViewBag.CommunicationModes = _dropdownService.GetCommunicationModeOptions();
            }
            return PartialView("_Create", mLeadCall);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Lead call has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting lead call!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

    }

}