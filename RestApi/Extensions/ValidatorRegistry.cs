using FluentValidation;
using RestApi.Controllers.v1.AuthController.Requests;
using RestApi.Controllers.v1.InquiryController.Requests;

namespace RestApi.Extensions;

public static class ValidatorRegistry
{
	public static IServiceCollection AddValidators(this IServiceCollection services)
	{

		services.AddTransient<IValidator<RegistrationRequest>, RegistrationRequestValidator>();
		services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddTransient<IValidator<CreateInquiryRequest>, CreateInquiryRequestValidator>();
        return services;
	}
}
