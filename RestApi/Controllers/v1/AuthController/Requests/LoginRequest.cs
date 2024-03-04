using FluentValidation;

namespace RestApi.Controllers.v1.AuthController.Requests
{
	public class LoginRequest
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithErrorCode("error.validation.field.empty");
            RuleFor(x => x.Password)
                .NotEmpty().WithErrorCode("error.validation.field.empty");
        }
    }
}
