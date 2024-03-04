using FluentValidation;
using Persistence.Models.Enums;

namespace RestApi.Controllers.v1.InquiryController.Requests;

public class CreateInquiryRequest
{
    public string? Body { get; set; }
    public InquiryType? Type {  get; set; } 
}

public class CreateInquiryRequestValidator : AbstractValidator<CreateInquiryRequest>
{
    public CreateInquiryRequestValidator()
    {
        RuleFor(x => x.Body)
            .NotEmpty().WithErrorCode("error.validation.field.empty");
        RuleFor(x => x.Type)
            .NotNull().WithErrorCode("error.validation.field.null")
            .IsInEnum().WithErrorCode("error.validation.field.invalid_enum");
    }
}