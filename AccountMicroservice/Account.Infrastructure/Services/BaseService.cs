using Account.Application.Enums;
using Account.Application.Interfaces.Services;
using Shared.Utilities.Helpers;

namespace Account.Infrastructure.Services
{
    public class BaseService
    {
        internal readonly IAuthenticatedUserService _authenticatedUserService;

        public BaseService(IAuthenticatedUserService authenticatedUserService)
        {
            _authenticatedUserService = authenticatedUserService;
        }

        public int LoggedInUserId { get; set; }
        public string LoggedInUserRole { get; set; }

        public void GetUserId()
        {
            if (_authenticatedUserService.UserId == 0) throw new UnauthorizedAccessException($"Access Denied. Kindly login to continue with this request.");
            LoggedInUserId = _authenticatedUserService.UserId;
            LoggedInUserRole = _authenticatedUserService.Role;
        }

        public void GetSuperAdminCredentials()
        {
            GetUserId();

            if(LoggedInUserRole.ToLower() != RoleStatusEnum.SuperAdministrator.ToDescription().ToLower())
                throw new UnauthorizedAccessException($"Access Denied. You do not have the permission to perform this function.");

            LoggedInUserRole = _authenticatedUserService.Role;
        }

        public void GetAnyAdminCredentials()
        {
            GetUserId();

            if (LoggedInUserRole.ToLower() != RoleStatusEnum.SuperAdministrator.ToDescription().ToLower() || LoggedInUserRole.ToLower() != RoleStatusEnum.Administrator.ToDescription().ToLower())
                throw new UnauthorizedAccessException($"Access Denied. You do not have the permission to perform this function.");

            LoggedInUserRole = _authenticatedUserService.Role;
        }
    }
}
