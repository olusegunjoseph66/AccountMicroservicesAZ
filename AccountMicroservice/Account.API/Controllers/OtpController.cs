using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class OtpController : BaseController
    {
        private readonly IOtpService _otpService;
        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }

       
        /// <summary>
        /// This resends Otp when initiated.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("resend")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest request, CancellationToken cancellationToken) => Response(await _otpService.ResendOtp(request, cancellationToken));
    }
}
