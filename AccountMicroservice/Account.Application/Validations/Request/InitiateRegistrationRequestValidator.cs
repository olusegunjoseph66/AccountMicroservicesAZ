using Account.Application.ViewModels.Requests;
using FluentValidation;

namespace Account.Application.Validations.Request
{
    public  class InitiateRegistrationRequestValidator : AbstractValidator<InitiateRegistrationRequest>
    {
        public InitiateRegistrationRequestValidator()
        {
            RuleFor(f => f.DistributorNumber)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply Distributor Number.");

            RuleFor(f => f.CountryCode)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply Country Code.");

            RuleFor(f => f.CompanyCode)
                .NotEmpty()
                .NotNull().WithMessage("Kindly supply Company Code.");

            RuleFor(f => f.ChannelCode)
               .NotEmpty()
               .NotNull().WithMessage("Kindly supply Channel Code.");
        }
    }
}
