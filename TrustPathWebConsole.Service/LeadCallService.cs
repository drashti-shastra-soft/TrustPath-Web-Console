using TrustPathWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Service
{
    public class LeadCallService : ILeadCallService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public LeadCallService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }

        }

        public async Task<Domain.LeadCallLister> GetAllAsync(Domain.LeadCallLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.LeadCallLister, Domain.LeadCallLister>(
                url: $"{_baseUrl}/LeadCall/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.LeadCall> CreateAsync(Domain.LeadCall mLeadCall)
        {
            return await _httpClientService.PostAsync<Domain.LeadCall, Domain.LeadCall>(
                url: $"{_baseUrl}/LeadCall",
                obj: mLeadCall,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.LeadCall> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.LeadCall>(
                url: $"{_baseUrl}/LeadCall/{id}",
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/LeadCall/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.LeadCall> SendWhatupMessage(Domain.LeadCall mLeadCall)
        {
            var Result = await _httpClientService.PostAsync<Domain.LeadCall, Domain.LeadCall>(
                    url: $"{_baseUrl}/LeadCall/WhatupMessage",
                    obj: mLeadCall,
                    token: _token
            ).ConfigureAwait(false);

            return Result;
        }
    }
}
