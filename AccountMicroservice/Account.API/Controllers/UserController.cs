using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.DTO.Pagination;

namespace Account.API.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService  _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<PaginatedList<UserResponse>>))]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryFilter filter, CancellationToken cancellationToken = default) => Response(await _userService.GetUsers(filter, cancellationToken));

        [HttpPost("profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request, CancellationToken cancellationToken) => Response(await _userService.UpdateProfile(request, cancellationToken));

        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<UserDetailResponse>))]
        public async Task<IActionResult> GetMyProfile( CancellationToken cancellationToken = default) => Response(await _userService.GetUserProfile(cancellationToken));
        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken) => Response(await _userService.AddUsers(request, cancellationToken));

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> UpdateUser( int userId, [FromBody] UpdateUserRequest request,  CancellationToken cancellationToken) => Response(await _userService.UpdateUsers(request, userId,cancellationToken));

        [HttpPost("activate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateDeactivateUserRequest request, CancellationToken cancellationToken) => Response(await _userService.ActivateDeactivateUsers(request, cancellationToken));

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> DeleteUser([FromQuery] int userId, CancellationToken cancellationToken) => Response(await _userService.DeleteUsers( userId, cancellationToken));
               
        [HttpGet("export")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<FileContentResult>))]
        public async Task<IActionResult> ExportUsers([FromQuery] ExportUsersDataQueryFilter filter , CancellationToken cancellationToken = default) => Response(await _userService.ExportUsers(filter, cancellationToken));
    }
}
