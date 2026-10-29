using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Domain
{
    public interface ICompanyScoped
    {
        int CompanyId { get; set; }
    }
}
