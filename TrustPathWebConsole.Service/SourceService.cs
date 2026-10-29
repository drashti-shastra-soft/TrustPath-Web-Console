using TrustPathWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace TrustPathWebConsole.Service
{
    public class SourceService : ISourceService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public SourceService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.SourceLister> GetAllAsync(Domain.SourceLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.SourceLister>(
               url: $"{_baseUrl}/Source/GetAll",
               obj: mLister,
               token: _token
             ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Source> CreateAsync(Domain.Source mSource)
        {
            return await _httpClientService.PostAsync<Domain.Source, Domain.Source>(
               url: $"{_baseUrl}/Source",
               obj: mSource,
               token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Source> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Source>(
             url: $"{_baseUrl}/Source/{id}",
                token: _token
             ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
               url: $"{_baseUrl}/Source/{id}/LoginUser/{_loginUserId}",
               token: _token
            ).ConfigureAwait(false);
        }

    }
}
