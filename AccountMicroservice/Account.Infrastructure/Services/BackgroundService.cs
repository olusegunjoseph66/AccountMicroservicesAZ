using Account.Application.Constants;
using Account.Application.DTOs;
using Account.Application.DTOs.APIDataFormatters;
using Account.Application.DTOs.Events;
using Account.Application.DTOs.Features.Account;
using Account.Application.DTOs.Filters;
using Account.Application.DTOs.Sortings;
using Account.Application.Enums;
using Account.Application.Exceptions;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Account.Application.ViewModels.Responses.ResponseDto;
using Account.Infrastructure.QueryObjects;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Shared.Data.Extensions;
using Shared.Data.Models;
using Shared.Data.Repository;
using Shared.ExternalServices.Interfaces;

namespace Account.Infrastructure.Services
{
    public class BackgroundService : IBackgroundService
    {
        private readonly ILogger<BackgroundService> _logger;
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<DistributorSapAccount> _distributorSapRepository;

        private readonly ISapService _sapService;
        public readonly IMessagingService _messageBus;

        public BackgroundService(ILogger<BackgroundService> logger, IAsyncRepository<User> userRepository, IAsyncRepository<DistributorSapAccount> distributorSapRepository, ISapService sapService, IMessagingService messageBus)
        {
            _logger = logger;
            _userRepository = userRepository;
            _distributorSapRepository = distributorSapRepository;
            _sapService = sapService;
            _messageBus = messageBus;
        }

        public async Task<ApiResponse> AutoExpireAccount(CancellationToken cancellationToken)
        {
            var usersQuery = _userRepository.Table.Where(x => !x.IsDeleted && x.PasswordExpiryDate < DateTime.UtcNow && x.UserStatusId != (byte)UserStatusEnum.Expired);

            var totalCount = await usersQuery.CountAsync(cancellationToken);
            var users = await usersQuery.ToListAsync(cancellationToken);

            users.ForEach(x =>
            {
                x.UserStatusId = (byte)UserStatusEnum.Expired;
            });
            _userRepository.UpdateRange(users);
            await _userRepository.CommitAsync(cancellationToken);

            _logger.LogInformation($"The AutoExpireAccount Service completed with {totalCount} users updated at {DateTime.UtcNow}");

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_AUTO_EXPIRE_ACCOUNT, new SingleCountDto { TotalCount = totalCount });
        }

        public async Task<ApiResponse> SynchronizeSapData(CancellationToken cancellationToken)
        {
            var distributors = await _distributorSapRepository.Table.Include(x => x.User).ThenInclude(x => x.UserStatus).ToListAsync(cancellationToken);

            List<DistributorSapAccount> distributorsToUpdate = new();
            List<AccountsSapAccountUpdatedMessage> sapAccountUpdatedMessages = new();
            List<AccountsUserUpdatedMessage> accountUserUpdatedMessages = new();
            
            for(int index = 0; index < distributors.Count; index++)
            {
                var sapCustomer = await _sapService.FindCustomer(distributors[index].CompanyCode, distributors[index].CountryCode, distributors[index].DistributorSapNumber);

                if (sapCustomer is null)
                {
                    _logger.LogError($"Sorry, the Sap Account with the Number {distributors[index].DistributorSapNumber}, Company Code {distributors[index].CompanyCode} and Country Code {distributors[index].CountryCode} does not exist or has not been previously onboarded.");

                    continue; 
                }

                if (sapCustomer.SapAccount is not null)
                {
                    bool isDistributorModified = false;
                    bool isUserModified = false;

                    DistributorSapAccount distributorToUpdate = new();
                    var selectedDistributor = distributors[index];
                    distributorToUpdate = distributors[index];

                    if (sapCustomer.SapAccount.PhoneNumber != selectedDistributor.User.PhoneNumber)
                    {
                        distributorToUpdate.User.PhoneNumber = sapCustomer.SapAccount.PhoneNumber;
                        isUserModified = true;
                    }

                    if (sapCustomer.SapAccount.EmailAddress.ToLower() != selectedDistributor.User.EmailAddress.ToLower())
                    {
                        distributorToUpdate.User.EmailAddress = sapCustomer.SapAccount.EmailAddress;
                        isUserModified = true;
                    }

                    if (sapCustomer.SapAccount.DistributorName.ToLower() != selectedDistributor.DistributorName)
                    {
                        distributorToUpdate.DistributorName = sapCustomer.SapAccount.DistributorName;
                        isDistributorModified = true;
                    }

                    if (isDistributorModified)
                    {
                        distributorToUpdate.DateModified = DateTime.UtcNow;

                        AccountsSapAccountUpdatedMessage sapUserUpdatedMessage = new()
                        {
                            DistributorSapAccountId = selectedDistributor.Id,
                            DistributorName = selectedDistributor.DistributorName,
                            NewFriendlyName = selectedDistributor.FriendlyName,
                            OldFriendlyName = selectedDistributor.FriendlyName,
                            DateModified = DateTime.UtcNow,
                            DistributorSapNumber = selectedDistributor.DistributorSapNumber,
                            DateCreated = selectedDistributor.DateCreated,
                            UserId = selectedDistributor.UserId,
                        };
                        sapAccountUpdatedMessages.Add(sapUserUpdatedMessage);
                    }

                    if (isUserModified)
                    {
                        distributorToUpdate.User.DateModified = DateTime.UtcNow;

                        AccountsUserUpdatedMessage userUpdatedMessage = new()
                        {
                            UserId = selectedDistributor.User.Id,
                            OldFirstName = selectedDistributor.User.FirstName,
                            OldLastName = selectedDistributor.User.LastName,
                            DateModified = DateTime.UtcNow,
                            OldEmailAddress = selectedDistributor.User.EmailAddress,
                            OldDeviceId = selectedDistributor.User.DeviceId,
                            OldUserName = selectedDistributor.User.UserName,
                            OldAccountStatus = new Application.DTOs.NameAndCode(selectedDistributor.User.UserStatus.Name, selectedDistributor.User.UserStatus.Code),
                            ModifiedByUserId = selectedDistributor.UserId,
                            NewFirstName = selectedDistributor.User.FirstName,
                            NewLastName = selectedDistributor.User.LastName,
                            NewEmailAddress = sapCustomer.SapAccount.EmailAddress,
                            NewDeviceId = selectedDistributor.User.DeviceId,
                            NewPhoneNumber = sapCustomer.SapAccount.PhoneNumber,
                            OldPhoneNumber = selectedDistributor.User.PhoneNumber,
                            NewUserName = selectedDistributor.User.UserName,
                            NewAccountStatus = new Application.DTOs.NameAndCode(selectedDistributor.User.UserStatus.Name, selectedDistributor.User.UserStatus.Code),
                            DateCreated = selectedDistributor.User.DateCreated
                        };
                        accountUserUpdatedMessages.Add(userUpdatedMessage);
                    }

                    distributorsToUpdate.Add(distributorToUpdate);
                }
            }

            if(distributorsToUpdate.Any())
            {
                _distributorSapRepository.UpdateRange(distributorsToUpdate);
                await _distributorSapRepository.CommitAsync(cancellationToken);
            }

            if (sapAccountUpdatedMessages.Any())
            {
                foreach(var item in sapAccountUpdatedMessages)
                {
                    await _messageBus.PublishTopicMessage(item, EventMessages.ACCOUNT_SAP_USER_UPDATED).ConfigureAwait(false);
                }
            }

            if (accountUserUpdatedMessages.Any())
            {
                foreach(var item in accountUserUpdatedMessages)
                {
                    await _messageBus.PublishTopicMessage(item, EventMessages.ACCOUNT_USER_UPDATED).ConfigureAwait(false);
                }
            }

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_AUTO_REFRESH_ACCOUNT);
        }
    }
}
