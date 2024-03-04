using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.InquiryRepository;
using Persistence.Repositories.UserRepository;

namespace Persistence
{
	public static class PersistenceServiceCollection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IInquiryRepository, InquiryRepository>();
            return services;
        }
    }
}
