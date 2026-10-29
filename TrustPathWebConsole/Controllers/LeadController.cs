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
    public class LeadController : Controller
    {
        private readonly ILeadService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ILeadCallService _leadCallService;


        public LeadController(ILeadService service, IDropdownService dropdownService, ILeadCallService leadCallService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _leadCallService = leadCallService;
        }

        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View(new Domain.LeadLister());
        }

        public ActionResult List(Domain.LeadLister mLister)
        {
            try
            {
                ViewBag.ShowCompany = false;
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                else
                    mLister.SearchCriteria.CompanyId = HttpContextHelper.LoginUser.CompanyId;
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
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                ViewBag.LeadStatus = _dropdownService.GetLeadStatusOptions();
            }

            return PartialView("_List", mLister);
        }


        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                var loginUser = HttpContextHelper.LoginUser;

                bool isSystemOwner = loginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner);
                ViewBag.ShowCompany = isSystemOwner;

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(loginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                ViewBag.BusinessType = _dropdownService.GetBusinessTypeOptions().GetAwaiter().GetResult();
                ViewBag.Source = _dropdownService.GetSourceOptions().GetAwaiter().GetResult();
                ViewBag.LeadStatus = _dropdownService.GetLeadStatusOptions();
                ViewBag.AssignLead = _dropdownService.GetActiveUsersOptionsAsync(loginUser.CompanyId).GetAwaiter().GetResult();
                ViewBag.State = _dropdownService.GetStateOptions().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                throw;
            }

            return PartialView("_Create", new Domain.Lead());
        }

        [HttpPost]
        public ActionResult Create(Domain.Lead mLead)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    mLead.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                Infrastructure.Helper.DefaultSetter.ApplyDefaults(mLead);
                mLead = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mLead);

                mLead = _service.CreateAsync(mLead).GetAwaiter().GetResult();

                bool success = mLead != null && mLead.Id > 0;
                ViewBag.NotificationType = success
                        ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                        : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                        ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Lead).StrToLower())
                        : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Lead).StrToLower());

                return base.PartialView("_Create", (object)mLead);
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
                return PartialView("_Create", mLead);
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                ViewBag.LeadStatus = _dropdownService.GetLeadStatusOptions();
                ViewBag.BusinessType = _dropdownService.GetBusinessTypeOptions().GetAwaiter().GetResult();
                ViewBag.Source = _dropdownService.GetSourceOptions().GetAwaiter().GetResult();
                ViewBag.AssignLead = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
                ViewBag.State = _dropdownService.GetStateOptions().GetAwaiter().GetResult();

            }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mLead = new Domain.Lead();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mLead = _service.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.BusinessType = _dropdownService.GetBusinessTypeOptions().GetAwaiter().GetResult();
                ViewBag.Source = _dropdownService.GetSourceOptions().GetAwaiter().GetResult();
                ViewBag.AssignLead = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
                ViewBag.State = _dropdownService.GetStateOptions().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mLead);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { Type = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower(), Message = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Lead).StrToLower()) };
            }
            catch (Exception)
            {
                resultData = new { Type = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower(), Message = "Internal server error!" };
                throw;
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #region WhatsApp Message

        [HttpGet]
        public ActionResult Message(int id)
        {
            var mLeadCall = new Domain.LeadCall();
            mLeadCall.LeadId = id;
            try
            {
                System.Threading.Thread.Sleep(1000);
                var lead = _service.GetAsync(id).GetAwaiter().GetResult();

                mLeadCall.LeadId = id;
                mLeadCall.CommunicationMode = "Whatsapp";
                ViewBag.PhoneNumber = lead.PhoneNumber;
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_Message", mLeadCall);
        }

        [HttpPost]
        public ActionResult SaveMessage(Domain.LeadCall mLeadCall)
        {
            try
            {
                mLeadCall.CommunicationMode = "Whatsapp";
                mLeadCall.DateTime = DateTime.Now;

                mLeadCall = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mLeadCall);
                _leadCallService.SendWhatupMessage(mLeadCall).GetAwaiter().GetResult();

                ViewBag.NotificationType = "success";
                ViewBag.NotificationMsg = "WhatsApp message saved successfully!";
            }
            catch
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }

            return PartialView("_Message", mLeadCall);
        }

        #endregion
    }


}