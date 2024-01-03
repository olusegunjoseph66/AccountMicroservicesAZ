using Account.Application.DTOs.APIDataFormatters;
using Account.Application.ViewModels.Requests;

namespace Account.Application.Interfaces.Services
{
    public interface IRegistrationService
    {
        Task<ApiResponse> InitiateRegistration(InitiateRegistrationRequest request, CancellationToken cancellationToken);

        Task<ApiResponse> CompleteRegistration(CompleteRegistrationRequest request, CancellationToken cancellationToken);

    }
}