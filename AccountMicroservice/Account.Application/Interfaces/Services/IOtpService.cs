using Account.Application.DTOs.APIDataFormatters;
using Account.Application.DTOs.Features.Otp;
using Account.Application.ViewModels.Requests;
using Shared.Data.Models;

namespace Account.Application.Interfaces.Services
{
    public interface IOtpService
    {
        Task<OtpDto> GenerateOtp(string emailAddress, CancellationToken cancellationToken, bool isNewOtp = true, string? phoneNumber = null, int? registrationId = 0, int? userId = 0, Otp? existingOtp = null);

        Task<ApiResponse> ValidateOtp(ValidateOtpRequest request, CancellationToken cancellationToken);

        Task<ApiResponse> ResendOtp(ResendOtpRequest request, CancellationToken cancellationToken);

        Task<ApiResponse> ValidateResetOtp(ValidateOtpRequest request, CancellationToken cancellationToken);
        Task<OtpDetailDto> ValidateLinkAccountOtp(ValidateOtpRequest request, CancellationToken cancellationToken);

    }
}