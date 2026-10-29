using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure.IService
{
    public interface ILeadService
    {
        Task<Domain.LeadLister> GetAllAsync(Domain.LeadLister mLister);

        Task<Domain.Lead> CreateAsync(Domain.Lead mLead);

        Task<Domain.Lead> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
