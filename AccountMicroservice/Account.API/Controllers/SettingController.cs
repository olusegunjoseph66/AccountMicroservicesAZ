using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Account.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [AllowAnonymous]
    public class SettingController : BaseController
    {
        private readonly IOtpService _otpService;
        private readonly IAccountSettingsService _accountSettingService;

        public SettingController(IOtpService otpService, IAccountSettingsService accountSettingService)
        {
            _otpService = otpService;
            _accountSettingService = accountSettingService;
        }

        /// <summary>
        /// To initiate the Reset Password process for Distributor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("reset")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<OtpResponse>))]
        public async Task<IActionResult> InitiatePasswordReset([FromBody] InitiateResetPasswordRequest request, CancellationToken cancellationToken) => Response(await _accountSettingService.InitiatePasswordReset(request, cancellationToken));

        /// <summary>
        /// To initiate the Reset Password process for Admin
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("reset/admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<OtpResponse>))]
        public async Task<IActionResult> InitiateAdminPasswordReset([FromBody] InitiateResetPasswordRequest request, CancellationToken cancellationToken) => Response(await _accountSettingService.InitiateAdminPasswordReset(request, cancellationToken));

        /// <summary>
        /// To validate the Reset Password initiation process using an OTP
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("reset/otp/validate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<ValidateResetOtpResponse>))]
        public async Task<IActionResult> ValidateResetOtp([FromBody] ValidateOtpRequest request, CancellationToken cancellationToken) => Response(await _otpService.ValidateResetOtp(request, cancellationToken));

        /// <summary>
        /// To complete the Reset Password process
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("reset/complete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> CompletePasswordReset([FromBody] CompleteResetPasswordRequest request, CancellationToken cancellationToken) => Response(await _accountSettingService.CompletePasswordReset(request, cancellationToken));
    }
}
