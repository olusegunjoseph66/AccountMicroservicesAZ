using Account.Application.Configurations;
using Account.Application.Constants;
using Account.Application.DTOs;
using Account.Application.DTOs.APIDataFormatters;
using Account.Application.DTOs.Events;
using Account.Application.DTOs.Features.Otp;
using Account.Application.Enums;
using Account.Application.Exceptions;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Account.Application.ViewModels.Responses.ResponseDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Data.Models;
using Shared.Data.Repository;
using Shared.ExternalServices.Interfaces;
using Shared.Utilities.Handlers;
using Shared.Utilities.Helpers;

namespace Account.Infrastructure.Services
{
    public class OtpService : IOtpService
    {
        private readonly IAsyncRepository<Otp> _otpRepository;
        private readonly IAsyncRepository<User> _userRepository;

        private readonly OtpSettings _otpSetting;
        private readonly TokenSettings _tokenSetting;

        public readonly IMessagingService _messageBus;

        public OtpService(IAuthenticatedUserService authenticatedUserService, IAsyncRepository<Otp> otpRepository, IAsyncRepository<User> userRepository, IOptions<OtpSettings> otpSetting, IOptions<TokenSettings> tokenSetting, IMessagingService messageBus)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;

            _otpSetting = otpSetting.Value;
            _tokenSetting = tokenSetting.Value;
            _messageBus = messageBus;
        }

        public async Task<OtpDto> GenerateOtp(string emailAddress, CancellationToken cancellationToken, bool isNewOtp = true, string? phoneNumber = null, int? registrationId = 0, int? userId = 0, Otp? existingOtp = null)
        {
            var otpCode = RandomValueGenerator.GenerateRandomDigits(_otpSetting.OtpCodeLength);
            Otp otp = new();

            if (!isNewOtp)
            {
                if (existingOtp == null)
                    throw new InternalServerException();

                var selectedOtp = await _otpRepository.Table.FirstOrDefaultAsync(x => x.Id == existingOtp.Id, cancellationToken) ?? throw new NotFoundException();
                existingOtp.Code = otpCode;
                existingOtp.DateCreated = DateTime.UtcNow;
                existingOtp.DateExpiry = DateTime.UtcNow.AddMinutes(_otpSetting.OtpExpiryInMinutes);
                existingOtp.NumberOfRetries = 0;
                existingOtp.OtpStatusId = (byte)OtpStatusEnum.New;
                otp = existingOtp;
                _otpRepository.Update(existingOtp);
            }
            else
            {
                otp = new Otp
                {
                    Code = otpCode,
                    DateCreated = DateTime.UtcNow,
                    DateExpiry = DateTime.UtcNow.AddMinutes(_otpSetting.OtpExpiryInMinutes),
                    EmailAddress = emailAddress,
                    NumberOfRetries = 0,
                    OtpStatusId = (byte)OtpStatusEnum.New, 
                    Reference = Guid.NewGuid().ToString()
                };
                if (phoneNumber != null)
                    otp.PhoneNumber = phoneNumber;

                if (registrationId > 0)
                    otp.RegistrationId = registrationId;

                if (userId > 0)
                    otp.UserId = userId;

                await _otpRepository.AddAsync(otp, cancellationToken);
            }
            await _otpRepository.CommitAsync(cancellationToken);
            return new OtpDto(otp.Id, otpCode, otp.DateExpiry, otp.DateCreated, otp.PhoneNumber, otp.EmailAddress, otp.Reference, Convert.ToInt32((otp.DateExpiry - DateTime.UtcNow).TotalSeconds));
        }

        public async Task<ApiResponse> ValidateOtp(ValidateOtpRequest request, CancellationToken cancellationToken)
        {
            var otpResponse = await OtpValidation(request, cancellationToken);

            var response = new ValidateOtpResponse(new RegistrationResponseDto(otpResponse.RegistrationId ?? 0));
            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_OTP_VALIDATION, response);
        }

        public async Task<OtpDetailDto> ValidateLinkAccountOtp(ValidateOtpRequest request, CancellationToken cancellationToken)
        {
            return await OtpValidation(request, cancellationToken);
        }

        public async Task<ApiResponse> ResendOtp(ResendOtpRequest request, CancellationToken cancellationToken)
        {
            var otpResponse = await _otpRepository.Table.Where(x => x.Reference == request.OtpDisplayId).Include(x => x.OtpStatus).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(ErrorMessages.OTP_INVALID, ErrorCodes.OTP_NOTFOUND_CODE);
            if (otpResponse.OtpStatus.Name == OtpStatusEnum.Validated.ToDescription())
                throw new ConflictException(ErrorMessages.OTP_INVALID, ErrorCodes.OTP_ALREADY_VALIDATED_CODE);

            if (otpResponse.OtpStatus.Name == OtpStatusEnum.Invalidated.ToDescription())
                throw new ConflictException(ErrorMessages.OTP_INVALID, ErrorCodes.OTP_ALREADY_INVALIDATED_CODE);

            if (otpResponse.DateCreated.AddMinutes(_otpSetting.RetryIntervalInMinutes) > DateTime.UtcNow)
                throw new ConflictException(ErrorMessages.RESEND_OTP_TIME_NOT_ELAPSED.Replace("{n}", _otpSetting.RetryIntervalInMinutes.ToString()), ErrorCodes.RESEND_OTP_TIME_NOT_ELAPSED_CODE);

            var newOtp = await GenerateOtp(otpResponse.EmailAddress, cancellationToken, isNewOtp: false, otpResponse.PhoneNumber, otpResponse.RegistrationId, existingOtp: otpResponse);

            var messageObject = new OtpPublishMessage
            {
                DateCreated = DateTime.UtcNow,
                EmailAddress = otpResponse.EmailAddress,
                DateExpiry = newOtp.ExpiryTime,
                OtpId = newOtp.Id,
                OtpCode = newOtp.Code,
                PhoneNumber = otpResponse.PhoneNumber
            };
            await _messageBus.PublishTopicMessage(messageObject, EventMessages.ACCOUNT_OTP_GENERATED);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_OTP_RESENDING.Replace("{EmailAddress}", newOtp.EmailAddress.MaskString(50)).Replace("{PhoneNumber}", newOtp.PhoneNumber.MaskString(50)), newOtp.countDownInSeconds);
        }

        public async Task<ApiResponse> ValidateResetOtp(ValidateOtpRequest request, CancellationToken cancellationToken)
        {
            var otpResponse = await OtpValidation(request, cancellationToken, authenticationRequired: true);
            var user = await _userRepository.Table.Where(x => x.Id == otpResponse.UserId.Value).FirstOrDefaultAsync(cancellationToken);

            user.ResetTokenExpiryDate = DateTime.UtcNow.AddMinutes(_tokenSetting.TokenExpiryInMinutes);

            _userRepository.Update(user);
            await _userRepository.CommitAsync(cancellationToken);

            var response = new ValidateResetOtpResponse(new ResetResponseDto(user.ResetToken));
            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_OTP_VALIDATION, response);
        }

        #region Private Methods

        private async Task UpdateOtp(Otp otp, CancellationToken cancellationToken)
        {
            _otpRepository.Update(otp);
            await _otpRepository.CommitAsync(cancellationToken);
        }

        private async Task<OtpDetailDto> OtpValidation(ValidateOtpRequest request, CancellationToken cancellationToken, bool authenticationRequired = false)
        {
            var otpResponse = await _otpRepository.Table.Where(x => x.Reference == request.OtpDisplayId).Include(x => x.OtpStatus).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(ErrorMessages.OTP_INVALID, ErrorCodes.OTP_NOTFOUND_CODE);
            if (otpResponse.OtpStatus.Name == OtpStatusEnum.Validated.ToDescription())
                throw new ConflictException(ErrorMessages.OTP_INVALID, ErrorCodes.OTP_ALREADY_VALIDATED_CODE);

            if (otpResponse.OtpStatus.Name == OtpStatusEnum.Invalidated.ToDescription())
                throw new ConflictException(ErrorMessages.OTP_INVALID, ErrorCodes.OTP_ALREADY_INVALIDATED_CODE);
            
            if (otpResponse.DateExpiry < DateTime.UtcNow)
                throw new ConflictException(ErrorMessages.OTP_EXPIRED, ErrorCodes.OTP_EXPIRED_CODE);

            if (authenticationRequired)
            {
                if (!otpResponse.UserId.HasValue)
                    throw new ConflictException(ErrorMessages.OTP_AUTHORIZED_USER_REQUIRED, ErrorCodes.OTP_AUTHORIZED_USER_REQUIRED_CODE);
            }

            bool isMatch = request.OtpCode == otpResponse.Code;
            if (!isMatch)
            {
                var numberOfRetries = otpResponse.NumberOfRetries.HasValue ? (otpResponse.NumberOfRetries.Value + 1) : 1;
                otpResponse.NumberOfRetries = (short)numberOfRetries;
                if (numberOfRetries > _otpSetting.MaximumRequiredRetries)
                    otpResponse.OtpStatusId = (byte)OtpStatusEnum.Invalidated;

                await UpdateOtp(otpResponse, cancellationToken);
                throw new ConflictException(ErrorMessages.OTP_INVALID, ErrorCodes.OTP_MISMATCH_CODE);
            }
            else
            {
                otpResponse.OtpStatusId = (byte)OtpStatusEnum.Validated;
                await UpdateOtp(otpResponse, cancellationToken);
            }

            var otpStatuses = Enum.GetValues(typeof(OtpStatusEnum)).Cast<OtpStatusEnum>()
                   .Select(e => new NameAndId<byte>((byte)e, e.ToDescription())).ToList();
            return new OtpDetailDto
            {
                Code = otpResponse.Code,
                NumberOfRetries = otpResponse.NumberOfRetries,
                DateCreated = otpResponse.DateCreated,
                DateExpiry = otpResponse.DateExpiry,
                EmailAddress = otpResponse.EmailAddress,
                Id = otpResponse.Id,
                PhoneNumber = otpResponse.PhoneNumber,
                RegistrationId = otpResponse.RegistrationId,
                UserId = otpResponse.UserId, 
                Reference = otpResponse.Reference,
                OtpStatus = otpStatuses.FirstOrDefault(x => x.Id == otpResponse.OtpStatusId), 
            };
        }
        #endregion
    }
}
