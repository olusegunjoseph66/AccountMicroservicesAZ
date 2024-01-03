using Account.Application.Configurations;
using Account.Application.Constants;
using Account.Application.DTOs.APIDataFormatters;
using Account.Application.DTOs.Events;
using Account.Application.DTOs.Features.Login;
using Account.Application.DTOs.Features.Registration;
using Account.Application.Enums;
using Account.Application.Exceptions;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Account.Application.ViewModels.Responses.ResponseDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Shared.Data.Models;
using Shared.Data.Repository;
using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.Interfaces;
using Shared.ExternalServices.ViewModels.Response;
using Shared.Utilities.Handlers;
using Shared.Utilities.Helpers;
using System;
using System.Text.RegularExpressions;

namespace Account.Infrastructure.Services
{
    public class RegistrationService : BaseUnauthenticatedService, IRegistrationService
    {
        private readonly IAsyncRepository<DistributorSapAccount> _distributorAccountRepository;
        private readonly IAsyncRepository<Registration> _registrationRepository;
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<UserStatus> _userStatusRepository;
        private readonly IAsyncRepository<Role> _roleRepository;
        private readonly IAsyncRepository<AccountType> _accountTypeRepository;

        private readonly PasswordSettings _passwordSetting;
        private readonly RoleSettings _roleSetting;

        private readonly ICachingService _cachingService;
        private readonly ISapService _sapService;
        private readonly IOtpService _otpService;
        public readonly IMessagingService _messageBus;
        public readonly IFileService _fileService;

        public RegistrationService(IAuthenticatedUserService authenticatedUserService, IAsyncRepository<DistributorSapAccount> distributorAccountRepository, ISapService sapService, IOtpService otpService, IMessagingService messageBus, ICachingService cachingService, IFileService fileService, IAsyncRepository<Registration> registrationRepository, IAsyncRepository<User> userRepository, IOptions<PasswordSettings> passwordSetting, IOptions<RoleSettings> roleSetting, IAsyncRepository<Role> roleRepository, IAsyncRepository<UserStatus> userStatusRepository, IAsyncRepository<AccountType> accountTypeRepository)
        {
            _distributorAccountRepository = distributorAccountRepository;
            _registrationRepository = registrationRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userStatusRepository = userStatusRepository;
            _accountTypeRepository = accountTypeRepository;

            _cachingService = cachingService;
            _sapService = sapService;
            _otpService = otpService;
            _messageBus = messageBus;
            _fileService = fileService;

            _passwordSetting = passwordSetting.Value;
            _roleSetting = roleSetting.Value;
        }

        public async Task<ApiResponse> InitiateRegistration(InitiateRegistrationRequest request, CancellationToken cancellationToken)
        {
            if(!request.IsPrivacyPolicyAccepted)
                throw new UnauthorizedUserException(ErrorMessages.PRIVACY_POLICY_NOT_ACCEPTED, ErrorCodes.PRIVACY_POLICY_NOT_ACCEPTED);

            if (await _distributorAccountRepository.Table.AnyAsync(x => x.DistributorSapNumber == request.DistributorNumber && x.CompanyCode == request.CompanyCode && x.CountryCode == request.CountryCode, cancellationToken))
                throw new ConflictException(ErrorMessages.DISTRIBUTOR_ACCOUNT_ALREADY_EXIST, ErrorCodes.DISTRIBUTOR_ACCOUNT_ALREADY_EXIST_CODE);

            var sapCustomer = await _sapService.FindCustomer(request.CompanyCode, request.CountryCode, request.DistributorNumber) ?? throw new NotFoundException(ErrorMessages.SAP_ACCOUNT_NOTFOUND, ErrorCodes.SAP_ACCOUNT_NOTFOUND_CODE);
            if (sapCustomer.SapAccount.Status.Name == Shared.ExternalServices.Enums.SapAccountStatusEnum.Inactive.ToDescription())
                throw new ConflictException(ErrorMessages.SAP_ACCOUNT_INACTIVE, ErrorCodes.SAP_ACCOUNT_INACTIVE_CODE);

            if (string.IsNullOrWhiteSpace(sapCustomer.SapAccount.PhoneNumber) || string.IsNullOrWhiteSpace(sapCustomer.SapAccount.EmailAddress) || string.IsNullOrWhiteSpace(sapCustomer.SapAccount.AccountType))
                throw new ConflictException(ErrorMessages.SAP_ACCOUNT_INFORMATION_INCOMPLETE, ErrorCodes.SAP_ACCOUNT_INFORMATION_INCOMPLETE_CODE);

            var registration = await _registrationRepository.AddAsync(new Registration
            {
                ChannelCode = request.ChannelCode,
                CompanyCode = request.CompanyCode,
                CountryCode = request.CountryCode,
                DateCreated = DateTime.UtcNow,
                DeviceId = request.DeviceId,
                DistributorNumber = request.DistributorNumber,
                RegistrationStatusId = (byte)RegistrationStatusEnum.New
            }, cancellationToken);
            await _registrationRepository.CommitAsync(cancellationToken);

            var key = $"{CacheKeys.REGISTRATION_INITIATION}{registration.Id}";
            var cacheData = new RegistrationCacheDto(sapCustomer, registration.Id, request.IsPrivacyPolicyAccepted);
            await _cachingService.SetAsync(key, cacheData, TimeSpan.FromMinutes(30), cancellationToken: cancellationToken);

            var otpResponse = await _otpService.GenerateOtp(sapCustomer.SapAccount.EmailAddress, cancellationToken, phoneNumber: sapCustomer.SapAccount.PhoneNumber, registrationId: registration.Id);

            var messageObject = new RegistrationPublishMessage
            {
                DateCreated = DateTime.UtcNow,
                DateExpiry = otpResponse.ExpiryTime,
                EmailAddress = sapCustomer.SapAccount.EmailAddress,
                OtpCode = otpResponse.Code,
                OtpId = otpResponse.Id,
                PhoneNumber = sapCustomer.SapAccount.PhoneNumber
            };
            await _messageBus.PublishTopicMessage(messageObject, EventMessages.ACCOUNT_OTP_GENERATED);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_ACCOUNT_REGISTRATION.Replace("{EmailAddress}", sapCustomer.SapAccount.EmailAddress.MaskString(40)).Replace("{PhoneNumber}", sapCustomer.SapAccount.PhoneNumber.MaskString(40)), new OtpResponse(new OtpResponseDto(otpResponse.Reference, otpResponse.countDownInSeconds)));
        }

        public async Task<ApiResponse> CompleteRegistration(CompleteRegistrationRequest request, CancellationToken cancellationToken)
        {
            if(PasswordContainsUserName(request.UserName, request.Password))
                throw new NotFoundException(ErrorMessages.PASSWORD_COMBINATION_INVALID, ErrorCodes.PASSWORD_COMBINATION_INVALID);

            var registration = await _registrationRepository.Table.FirstOrDefaultAsync(x => x.Id == request.RegistrationId, cancellationToken) ?? throw new NotFoundException(ErrorMessages.REGISTRATION_NOTFOUND, ErrorCodes.REGISTRATION_NOTFOUND_CODE);

            if (registration.RegistrationStatusId == (byte)RegistrationStatusEnum.Completed)
                throw new ConflictException(ErrorMessages.REGISTRATION_PREVIOUSLY_COMPLETED, ErrorCodes.REGISTRATION_PREVIOUSLY_COMPLETED_CODE);

            var isUserNameExisting = await _userRepository.Table.AnyAsync(x => x.UserName == request.UserName, cancellationToken);

            if (isUserNameExisting)
                throw new ConflictException(ErrorMessages.USERNAME_ALREADY_EXIST, ErrorCodes.USERNAME_ALREADY_EXIST_CODE);

            var isPasswordValid = Regex.IsMatch(request.Password, _passwordSetting.RegexPattern);
            if (!isPasswordValid)
                throw new ConflictException(ErrorMessages.PASSWORD_INVALID, ErrorCodes.PASSWORD_INVALID_CODE);

            var key = $"{CacheKeys.REGISTRATION_INITIATION}{registration.Id}";
            var cacheResponse = await _cachingService.GetAsync(key, cancellationToken: cancellationToken) ?? throw new ConflictException(ErrorMessages.INVALIDATED_RECORD, ErrorCodes.CONFLICT_ERROR_CODE);
            var jObjectResponse = (JObject)cacheResponse;
            RegistrationCacheDto cacheData = (RegistrationCacheDto)jObjectResponse.ToObject(typeof(RegistrationCacheDto));

            var role = await _roleRepository.Table.Where(x => x.Name.ToLower() == _roleSetting.DefaultRoleName.ToLower()).Select(x => new Role
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(ErrorMessages.DEFAULT_ROLE_NOTFOUND, ErrorCodes.SERVER_CONFIGURATION_ERROR_CODE);
            UploadResponse? upload = new();
            if (!string.IsNullOrWhiteSpace(request.ProfilePhoto))
            {
                (upload, bool isValid) = await _fileService.FileUpload(base64String: request.ProfilePhoto, cancellationToken: cancellationToken);

                if (!isValid)
                    throw new ValidationException(ErrorMessages.INVALID_FILE_UPLOAD_FORMAT, ErrorCodes.DEFAULT_VALIDATION_CODE);
            }

            var accountTypes = await _accountTypeRepository.Table.ToListAsync(cancellationToken: cancellationToken);
            if (!accountTypes.Any())
                throw new Exception();

            byte statusId = 0;
            if (cacheData.SapAccount.SapAccount.Status.Code == UserStatusEnum.Active.ToDescription())
                statusId = (byte)UserStatusEnum.Active;
            else if (cacheData.SapAccount.SapAccount.Status.Code == UserStatusEnum.Inactive.ToDescription())
                statusId = (byte)UserStatusEnum.Inactive;
            else if (cacheData.SapAccount.SapAccount.Status.Code == UserStatusEnum.Locked.ToDescription())
                statusId = (byte)UserStatusEnum.Locked;
            else if (cacheData.SapAccount.SapAccount.Status.Code == UserStatusEnum.Expired.ToDescription())
                statusId = (byte)UserStatusEnum.Expired;

            var hashedPassword = EncryptionHelper.Hash(request.Password);
            var user = new User
            {
                DateCreated = DateTime.UtcNow,
                EmailAddress = cacheData.SapAccount.SapAccount.EmailAddress,
                FirstName = request.FirstName,
                IsDeleted = false,
                LastName = request.LastName,
                Password = hashedPassword,
                PasswordResetRequired = false,
                PhoneNumber = cacheData.SapAccount.SapAccount.PhoneNumber,
                UserName = request.UserName,
                UserStatusId = statusId,
                IsPrivacyPolicyAccepted = cacheData.IsPrivacyPolicyAccepted,
                PasswordExpiryDate = DateTime.UtcNow.AddDays(_passwordSetting.PasswordExpiryDays)
            };

            if(upload != null)
            {
                user.ProfilePhotoCloudPath = upload.CloudUrl;
                user.ProfilePhotoPublicUrl = upload.PublicUrl;
            }

            var userRoles = new List<UserRole>()
            {
                new UserRole
                {
                    RoleId = role.Id,
                    UserId = user.Id
                }
            };

            var passwordHistories = new List<UserPasswordHistory>()
            {
                new UserPasswordHistory
                {
                     DateCreated = DateTime.UtcNow, UserId = user.Id, Password = hashedPassword
                }
            };

            byte accountTypeId = 0;
            if (cacheData.SapAccount.SapAccount.AccountType == AccountTypeEnum.BankGuarantee.ToDescription() || cacheData.SapAccount.SapAccount.AccountType == AccountTypeCodeEnum.BankGuarantee.ToDescription())
                accountTypeId = (byte)AccountTypeEnum.BankGuarantee;
            else if (cacheData.SapAccount.SapAccount.AccountType == AccountTypeEnum.CashCustomer.ToDescription() || cacheData.SapAccount.SapAccount.AccountType == AccountTypeCodeEnum.CashCustomer.ToDescription())
                accountTypeId = (byte)AccountTypeEnum.CashCustomer;
            else if (cacheData.SapAccount.SapAccount.AccountType == AccountTypeEnum.CleanCreditCustomer.ToDescription() || cacheData.SapAccount.SapAccount.AccountType == AccountTypeCodeEnum.CleanCreditCustomer.ToDescription())
                accountTypeId = (byte)AccountTypeEnum.CleanCreditCustomer;
            else if (cacheData.SapAccount.SapAccount.AccountType == AccountTypeEnum.BankGuaranteeCustomer.ToDescription() || cacheData.SapAccount.SapAccount.AccountType == AccountTypeCodeEnum.BankGuaranteeCustomer.ToDescription())
                accountTypeId = (byte)AccountTypeEnum.BankGuarantee;

            var accountType = accountTypes.FirstOrDefault(x => x.Id == accountTypeId);    

            var distributorAccounts = new List<DistributorSapAccount>() 
            {
                new DistributorSapAccount
                {
                    CompanyCode = registration.CompanyCode,
                    CountryCode = registration.CountryCode,
                    DateCreated = DateTime.UtcNow,
                    DistributorSapNumber = registration.DistributorNumber,
                    UserId = user.Id,
                    DistributorName = cacheData.SapAccount.SapAccount.DistributorName,
                    AccountTypeId = accountTypeId
                }
            };
            
            user.UserRoles = userRoles;
            user.UserPasswordHistories = passwordHistories;
            user.DistributorSapAccounts = distributorAccounts;
            await _userRepository.AddAsync(user, cancellationToken);

            registration.RegistrationStatusId = (byte)RegistrationStatusEnum.Completed;
            _registrationRepository.Update(registration);
            await _userRepository.CommitAsync(cancellationToken);

            var sapMessageObject = new AccountsSapAccountCreatedMessage
            {
                CompanyCode = registration.CompanyCode,
                CountryCode = registration.CountryCode,
                DateCreated = DateTime.UtcNow,
                DistributorSapNumber = registration.DistributorNumber,
                DistributorName = cacheData.SapAccount.SapAccount.DistributorName,
                DistributorSapAccountId = distributorAccounts[0].Id,
                UserId = user.Id,
                AccountType = new Application.DTOs.NameAndCode(accountType.Name, accountType.Code), 
                IsMessageRequired = false
            };

            var status = await _userStatusRepository.Table.FirstOrDefaultAsync(x => x.Id == user.UserStatusId, cancellationToken: cancellationToken);
            var userRole = await _roleRepository.Table.FirstOrDefaultAsync(x => x.Id == user.UserRoles.FirstOrDefault().RoleId, cancellationToken: cancellationToken);
            var messageObject = new UserCreatedMessage
            {
                DateCreated = DateTime.UtcNow,
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName, 
                UserId = user.Id, 
                AccountStatus = new Application.DTOs.NameAndCode(status.Name, status.Code), 
                Roles = new List<string> { userRole.Name }
            };

            await _messageBus.PublishTopicMessage(sapMessageObject, EventMessages.ACCOUNT_SAP_CREATED);
            await _messageBus.PublishTopicMessage(messageObject, EventMessages.ACCOUNT_USER_CREATED);
            await _cachingService.RemoveAsync(key, cancellationToken);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_REGISTRATION_COMPLETION);
        }
    }
}
