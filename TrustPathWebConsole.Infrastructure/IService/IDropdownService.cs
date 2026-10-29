using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure.IService
{
    public interface IDropdownService
    {
        dynamic GetStatusOptions();

        dynamic GetPageSizeOptions(int selectedSize = 20);

        Task<dynamic> GetCompanyOptionsAsync();

        dynamic GetLeadStatusOptions();

        dynamic GetCommunicationModeOptions();

        Task<dynamic> GetBusinessTypeOptions();

        Task<dynamic> GetSourceOptions();

        Task<dynamic> GetActiveUsersOptionsAsync(int companyId);

        Task<dynamic> GetRolesOptionsAsync(int roleId = 0);

        Task<dynamic> GetStateOptions();

        dynamic GetCallStatusOptions();

    }
}
