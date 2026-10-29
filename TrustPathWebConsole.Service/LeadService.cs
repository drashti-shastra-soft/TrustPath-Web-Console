using TrustPathWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace TrustPathWebConsole.Service
{
    public class LeadService : ILeadService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public LeadService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }

        }

        public async Task<Domain.LeadLister> GetAllAsync(Domain.LeadLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.LeadLister, Domain.LeadLister>(
                url: $"{_baseUrl}/Lead/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Lead> CreateAsync(Domain.Lead mLead)
        {
            return await _httpClientService.PostAsync<Domain.Lead, Domain.Lead>(
                url: $"{_baseUrl}/Lead",
                obj: mLead,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Lead> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Lead>(
                url: $"{_baseUrl}/Lead/{id}",
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Lead/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }
    }
}
