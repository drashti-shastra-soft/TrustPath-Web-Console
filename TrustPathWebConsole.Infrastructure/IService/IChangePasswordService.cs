using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure.IService
{
    public interface IChangePasswordService
    {
        Task Initiate(Domain.ChangePassword mChangePassword);
    }
}
