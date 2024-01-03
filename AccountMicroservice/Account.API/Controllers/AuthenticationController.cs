using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<AuthenticationResponse>))]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request, CancellationToken cancellationToken) => Ok(await _authenticationService.ValidateUser(request, cancellationToken));

        [HttpPost("admin/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<AuthenticationResponse>))]
        public async Task<IActionResult> AdminLogin([FromBody] AuthenticationRequest request, CancellationToken cancellationToken) => Ok(await _authenticationService.ValidateAdminUser(request, cancellationToken));

        [HttpPost("login/v2")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<AuthenticationResponse>))]
        public async Task<IActionResult> DistributorTwoFactorAuthentication([FromBody] TwoFactorLoginRequest request, CancellationToken cancellationToken) => Ok(await _authenticationService.DistributorTwoFactorAuthentication(request, cancellationToken));

        [HttpPost("admin/login/v2")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<AuthenticationResponse>))]
        public async Task<IActionResult> AdminTwoFactorAuthentication([FromBody] TwoFactorLoginRequest request, CancellationToken cancellationToken) => Ok(await _authenticationService.AdminTwoFactorAuthentication(request, cancellationToken));

        [HttpPost("login/v2/otp/validate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<AuthenticationResponse>))]
        public async Task<IActionResult> TwoFactorCompletion([FromBody] TwoFactorCompletionRequest request, CancellationToken cancellationToken) => Ok(await _authenticationService.TwoFactorCompletion(request, cancellationToken));
 
    }
}
