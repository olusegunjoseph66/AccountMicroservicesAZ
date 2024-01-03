using Account.Application.ViewModels.Requests;
using FluentValidation;

namespace Account.Application.Validations.Request
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {

            RuleFor(f => f.ChannelCode)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply ChannelCode.");

            RuleFor(f => f.IpAddress)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply IpAddress.");

            RuleFor(f => f.DeviceId)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply DeviceId.");

            RuleFor(f => f.Password)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply Password.");

            RuleFor(f => f.UserName)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply UserName.");
        }
    }
}
