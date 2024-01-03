using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class RegisterController : BaseController
    {
        private readonly IRegistrationService _registrationService;
        private readonly IOtpService _otpService;
        public RegisterController(IRegistrationService registrationService, IOtpService otpService)
        {
            _registrationService = registrationService;
            _otpService = otpService;
        }

        /// <summary>
        /// This initiates the registration process into DMS of an existing SAP Customer.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<OtpResponse>))]
        public async Task<IActionResult> InitiateRegistration([FromBody]InitiateRegistrationRequest request, CancellationToken cancellationToken) => Response(await _registrationService.InitiateRegistration(request, cancellationToken));

        /// <summary>
        /// To validate the Otp sent during the Registration initiation process.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("otp/validate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<ValidateOtpResponse>))]
        public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpRequest request, CancellationToken cancellationToken) => Response(await _otpService.ValidateOtp(request, cancellationToken));

        /// <summary>
        /// This completes the registration process into DMS of an existing SAP Customer.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("complete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> CompleteRegistration([FromBody] CompleteRegistrationRequest request, CancellationToken cancellationToken) => Response(await _registrationService.CompleteRegistration(request, cancellationToken));

    }
}
