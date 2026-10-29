using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustPathWebConsole.Domain;
using TrustPathWebConsole.Infrastructure.IService;

namespace TrustPathWebConsole.Service
{
    public class BusinessTypeService : IBusinessTypeService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public BusinessTypeService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.BusinessTypeLister> GetAllAsync(Domain.BusinessTypeLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.BusinessTypeLister>(
                url: $"{_baseUrl}/BusinessType/GetAll",
                obj: mLister,
                token: _token
             ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.BusinessType> CreateAsync(Domain.BusinessType mBusinessType)
        {
            return await _httpClientService.PostAsync<Domain.BusinessType, Domain.BusinessType>(
              url: $"{_baseUrl}/BusinessType",
              obj: mBusinessType,
              token: _token
           ).ConfigureAwait(false);
        }

        public async Task<Domain.BusinessType> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.BusinessType>(
            url: $"{_baseUrl}/BusinessType/{id}",
               token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
               url: $"{_baseUrl}/BusinessType/{id}/LoginUser/{_loginUserId}",
               token: _token
            ).ConfigureAwait(false);
        }

    }
}
