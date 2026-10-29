using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure.IService
{
    public interface ISourceService
    {
        Task<Domain.SourceLister> GetAllAsync(Domain.SourceLister mLister);

        Task<Domain.Source> CreateAsync(Domain.Source mSource);

        Task<Domain.Source> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
