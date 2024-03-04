using FluentValidation;

namespace RestApi.Controllers.v1.AuthController.Requests;

public class RegistrationRequest
{
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? RepeatPassword { get; set; }
}

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithErrorCode("error.validation.field.empty");
        RuleFor(x => x.Password)
            .NotEmpty().WithErrorCode("error.validation.field.empty");
        RuleFor(x => x.RepeatPassword)
            .NotEmpty().WithErrorCode("error.validation.field.empty")
            .Equal(x => x.Password, StringComparer.Ordinal).WithErrorCode("error.validation.password.mismatch");
    }
}
