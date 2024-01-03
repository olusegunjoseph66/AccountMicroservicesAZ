using Account.Application.ViewModels.Requests;
using FluentValidation;

namespace Account.Application.Validations.Request
{
    public class CompleteRegistrationRequestValidator : AbstractValidator<CompleteRegistrationRequest>
    {
        public CompleteRegistrationRequestValidator()
        {
            RuleFor(f => f.RegistrationId)
                .NotEmpty()
                .NotNull().GreaterThan(0).WithMessage("Kindly supply the RegistrationId.");

            RuleFor(f => f.UserName)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply your UserName.");

            RuleFor(f => f.Password)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply your Password.");
        }
    }
}
