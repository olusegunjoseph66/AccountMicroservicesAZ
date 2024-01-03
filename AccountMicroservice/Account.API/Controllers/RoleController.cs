using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<RoleResponse>))]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken = default) => Response(await _roleService.GetRoles(cancellationToken));

    }
}
