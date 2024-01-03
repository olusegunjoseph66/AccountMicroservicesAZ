using Account.Application.DTOs.APIDataFormatters;

namespace Account.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<ApiResponse> GetRoles(CancellationToken cancellationToken);
    }
}