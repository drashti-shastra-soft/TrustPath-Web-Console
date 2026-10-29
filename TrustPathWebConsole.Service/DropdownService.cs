using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrustPathWebConsole.Infrastructure;
using TrustPathWebConsole.Infrastructure.IService;

namespace TrustPathWebConsole.Service
{
    public class DropdownService : IDropdownService
    {
        #region Declaration

        private readonly IHttpClientService _httpClientService;
        private readonly string baseUrl;

        public DropdownService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }

        #endregion

        #region Status
        public dynamic GetStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Active", Value = "Active" },
                new SelectListItem { Text = "Disabled", Value = "Disabled" }
            };
        }
        #endregion

        #region Pagenation
        public dynamic GetPageSizeOptions(int selectedSize = 20)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "All", Value = "-1" }
            };

            list.ForEach(item => item.Selected = item.Value == selectedSize.ToString());
            return list;
        }
        #endregion

        #region Company
        public async Task<dynamic> GetCompanyOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mCompanies = await _httpClientService.GetAsync<List<Domain.Company>>(
                url: $"{baseUrl}/Company/GetActiveCompanies",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mCompany in mCompanies)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mCompany.Name, Value = Convert.ToString(mCompany.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Lead Status
        public dynamic GetLeadStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "New Lead", Value = "New Lead" },
                new SelectListItem { Text = "Follow Up", Value = "Follow Up" },
                new SelectListItem { Text = "Interested", Value = "Interested" },
                new SelectListItem { Text = "Not Interested", Value = "Not Interested" }
            };
        }
        #endregion

        #region CommunicationMode
        public dynamic GetCommunicationModeOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Whatsapp", Value = "Whatsapp" },
                new SelectListItem { Text = "Message", Value = "Message" },
                new SelectListItem { Text = "Call", Value = "Call" },
                new SelectListItem { Text = "One to One", Value = "One to One" }
            };
        }
        #endregion

        #region Business Types

        public async Task<dynamic> GetBusinessTypeOptions()
        {
            var mSelectListItems = new List<SelectListItem>();
            var mBusinessTypes = await _httpClientService.GetAsync<List<Domain.BusinessType>>(
                url: $"{baseUrl}/BusinessType",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mBusinessType in mBusinessTypes)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mBusinessType.Name, Value = Convert.ToString(mBusinessType.Id) });
            }
            return mSelectListItems;
        }

        #endregion

        #region Source

        public async Task<dynamic> GetSourceOptions()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mSources = await _httpClientService.GetAsync<List<Domain.Source>>(
                url: $"{baseUrl}/Source",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mSource in mSources)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mSource.Name, Value = Convert.ToString(mSource.Id) });
            }
            return mSelectListItems;
        }

        #endregion

        #region Active Users
        public async Task<dynamic> GetActiveUsersOptionsAsync(int companyId)
        {
            var mSelectListItems = new List<SelectListItem>();

            var mUsers = await _httpClientService.GetAsync<List<Domain.User>>(
                url: $"{baseUrl}/User/GetActiveUsers/CompanyId/{companyId}",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var user in mUsers)
            {
                mSelectListItems.Add(new SelectListItem()
                {
                    Text = $"{user.FirstName} {user.LastName}",
                    Value = Convert.ToString(user.Id)
                });
            }

            return mSelectListItems;
        }
        #endregion

        #region Roles
        public async Task<dynamic> GetRolesOptionsAsync(int roleId = 0)
        {
            var mSelectListItems = new List<SelectListItem>();
            var systemOwnerRoleId = EnumsHelper.GetEnumIndex(Enums.Role.System_Owner);
            var isSystemOwner = roleId == systemOwnerRoleId;

            var mRoles = await _httpClientService.GetAsync<List<Domain.Role>>(
                url: $"{baseUrl}/Role",
                token: HttpContextHelper.LoginUser?.Token).ConfigureAwait(false);

            var filteredRoles = mRoles.Where(role => isSystemOwner || role.Id != systemOwnerRoleId).ToList();

            foreach (var filteredRole in filteredRoles)
            {
                mSelectListItems.Add(new SelectListItem() { Text = filteredRole.Name, Value = Convert.ToString(filteredRole.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region State

        public async Task<dynamic> GetStateOptions()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mStates = await _httpClientService.GetAsync<List<Domain.State>>(
                    url: $"{baseUrl}/State",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mState in mStates)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mState.Name, Value = Convert.ToString(mState.Id) });
            }
            return mSelectListItems;
        }

        #endregion

        #region Call Status
        public dynamic GetCallStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Busy", Value = "Busy" },
                new SelectListItem { Text = "Connected", Value = "Connected" },
                new SelectListItem { Text = "Switch Off", Value = "Switch Off" },
                new SelectListItem { Text = "Not Reachable", Value = "Not Reachable" }
            };
        }
        #endregion

    }
}
