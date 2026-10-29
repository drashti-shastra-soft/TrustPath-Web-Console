using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustPathWebConsole.Domain;

namespace TrustPathWebConsole.Infrastructure.Helper
{
    public static class DefaultSetter
    {
        public static T ApplyDefaults<T>(T entity) where T : ICompanyScoped
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.CompanyId = HttpContextHelper.LoginUser.CompanyId;
            return entity;
        }
    }
}
