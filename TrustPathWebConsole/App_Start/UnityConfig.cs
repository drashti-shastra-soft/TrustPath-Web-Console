using System.Web.Mvc;
using TrustPathWebConsole.Infrastructure;
using TrustPathWebConsole.Infrastructure.IService;
using TrustPathWebConsole.Service;
using Unity;
using Unity.Mvc5;

namespace TrustPathWebConsole
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // Service
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IBusinessTypeService, BusinessTypeService>();
            container.RegisterType<IChangePasswordService, ChangePasswordService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IDropdownService, DropdownService>();
            container.RegisterType<IForgotPasswordService, ForgotPasswordService>();
            container.RegisterType<IHttpClientService, HttpClientService>();
            container.RegisterType<ILeadCallService, LeadCallService>();
            container.RegisterType<ILeadService, LeadService>();
            container.RegisterType<ISourceService, SourceService>();
            container.RegisterType<IStateService, StateService>();
            container.RegisterType<IUserService, UserService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}