using Account.Application.ViewModels.Requests;
using FluentValidation;

namespace Account.Application.Validations.Request
{
    public class OtpRequestValidator : AbstractValidator<ValidateOtpRequest>
    {
        public OtpRequestValidator()
        {
            RuleFor(f => f.OtpDisplayId)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply a valid OtpId.");

            RuleFor(f => f.OtpCode)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply Otp Code.");
        }
    }
}
