using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure.IService
{
    public interface ICompanyService
    {
        Task<Domain.CompanyLister> GetAllAsync(Domain.CompanyLister mLister);

        Task<Domain.Company> CreateAsync(Domain.Company mCompany);

        Task<Domain.Company> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
