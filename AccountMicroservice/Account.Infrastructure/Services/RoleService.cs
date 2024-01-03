using Account.Application.Constants;
using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Responses;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Models;
using Shared.Data.Repository;

namespace Account.Infrastructure.Services
{
    public class RoleService : BaseService, IRoleService
    {

        private readonly IAsyncRepository<Role> _roleRepository;
        public RoleService(IAsyncRepository<Role> roleRepository, IAuthenticatedUserService authenticatedUserService) : base(authenticatedUserService)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ApiResponse> GetRoles(CancellationToken cancellationToken)
        {
            GetSuperAdminCredentials();

            var roles = await _roleRepository.Table.Select(x => new Role
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_RETRIEVAL_OF_ROLE_LIST, new RoleResponse(roles.Select(x => x.Name).ToList()));
        }
    }
}
