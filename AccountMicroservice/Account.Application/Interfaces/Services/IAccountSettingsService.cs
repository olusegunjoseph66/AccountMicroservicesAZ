using Account.Application.DTOs.APIDataFormatters;
using Account.Application.ViewModels.Requests;

namespace Account.Application.Interfaces.Services
{
    public interface IAccountSettingsService
    {
        Task<ApiResponse> InitiatePasswordReset(InitiateResetPasswordRequest request, CancellationToken cancellationToken);

        Task<ApiResponse> InitiateAdminPasswordReset(InitiateResetPasswordRequest request, CancellationToken cancellationToken);

        Task<ApiResponse> CompletePasswordReset(CompleteResetPasswordRequest request, CancellationToken cancellationToken);
    }
}