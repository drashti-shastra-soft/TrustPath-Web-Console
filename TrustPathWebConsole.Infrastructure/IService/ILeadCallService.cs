using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure.IService
{
    public interface ILeadCallService
    {
        Task<Domain.LeadCallLister> GetAllAsync(Domain.LeadCallLister mLister);

        Task<Domain.LeadCall> CreateAsync(Domain.LeadCall mLeadCall);

        Task<Domain.LeadCall> GetAsync(int id);

        Task<Domain.LeadCall> SendWhatupMessage(Domain.LeadCall mLeadCall);

        Task DeleteAsync(int id);
    }
}
