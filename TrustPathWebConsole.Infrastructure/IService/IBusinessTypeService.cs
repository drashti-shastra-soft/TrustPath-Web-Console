using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure.IService
{
    public interface IBusinessTypeService
    {
        Task<Domain.BusinessTypeLister> GetAllAsync(Domain.BusinessTypeLister mLister);

        Task<Domain.BusinessType> CreateAsync(Domain.BusinessType mBusinessType);

        Task<Domain.BusinessType> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
