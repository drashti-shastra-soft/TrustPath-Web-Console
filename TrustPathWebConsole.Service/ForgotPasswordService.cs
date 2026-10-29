using TrustPathWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace TrustPathWebConsole.Service
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;

        public ForgotPasswordService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }

        public async Task Initiate(Domain.ForgotPassword mForgotPassword)
        {
            await _httpClientService.PostAsync<Domain.ForgotPassword>(url: $"{_baseUrl}/ForgotPassword/", mForgotPassword).ConfigureAwait(false);
        }
    }
}
