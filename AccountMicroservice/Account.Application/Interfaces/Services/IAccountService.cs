using Account.Application.DTOs.APIDataFormatters;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;
using Shared.ExternalServices.ViewModels.Response;

namespace Account.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<ApiResponse> GetSapAccounts(DistributorUserQueryFilter filter, CancellationToken cancellationToken);
        Task<ApiResponse> GetSapAccountsByAdmin(int userId, CancellationToken cancellationToken);
        Task<ApiResponse> RenameFriendlyName(RenameSapAccountRequest request, int sapAccountId, CancellationToken cancellationToken);
        Task<ApiResponse> RequestDeleteSapAccount(SapAccountDeletionRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> GetDeletionRequestsByAdmin(CancellationToken cancellationToken = default);
        Task<ApiResponse> LinkDistributorAccount(LinkDistributorAccountRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> ValidateLinkAccountOtp(ValidateOtpRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> UnlinkAccount(UnLinkSapAccountRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> AutoExpireAccount(CancellationToken cancellationToken);
    }
}
