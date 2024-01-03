using Account.Application.DTOs.APIDataFormatters;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;

namespace Account.Application.Interfaces.Services
{

    public interface IUserService
    {
        Task<ApiResponse> GetUsers(UserQueryFilter filter, CancellationToken cancellationToken);
        Task<ApiResponse> AddUsers(CreateUserRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> UpdateUsers(UpdateUserRequest request, int userId, CancellationToken cancellationToken);
        Task<ApiResponse> ActivateDeactivateUsers(ActivateDeactivateUserRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> DeleteUsers(int userId, CancellationToken cancellationToken);
        Task<ApiResponse> ExportUsers(ExportUsersDataQueryFilter filter, CancellationToken cancellationToken);
        Task<ApiResponse> GetUserProfile(CancellationToken cancellationToken);
        Task<ApiResponse> UpdateProfile(UpdateProfileRequest request, CancellationToken cancellationToken);
    }
}