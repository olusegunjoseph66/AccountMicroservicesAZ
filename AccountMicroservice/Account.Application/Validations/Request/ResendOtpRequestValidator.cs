using Account.Application.ViewModels.Requests;
using FluentValidation;

namespace Account.Application.Validations.Request
{
    public class ResendOtpRequestValidator : AbstractValidator<ResendOtpRequest>
    {
        public ResendOtpRequestValidator()
        {
            RuleFor(f => f.OtpDisplayId)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply a valid OtpDisplayId.");
        }
    }
}
