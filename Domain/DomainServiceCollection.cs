using Domain.Services;
using Domain.Services.AuthServices;
using Domain.Services.InquiryServices;
using Domain.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
	public static class DomainServiceCollection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IInquiryService, InquiryService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
