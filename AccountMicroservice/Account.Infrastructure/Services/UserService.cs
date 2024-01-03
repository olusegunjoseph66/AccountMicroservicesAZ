using Account.Application.Configurations;
using Account.Application.Constants;
using Account.Application.DTOs.Sortings;
using Account.Application.Enums;
using Account.Application.Exceptions;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Account.Infrastructure.QueryObjects;
using ClosedXML.Excel;
using Aspose.Pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Data.Extensions;
using Shared.Data.Models;
using Shared.Data.Repository;
using Shared.ExternalServices.Interfaces;
using Shared.Utilities.DTO.Pagination;
using Shared.Utilities.Helpers;
using System.Data;
using Account.Application.DTOs.APIDataFormatters;
using Microsoft.AspNetCore.Mvc;
using Account.Application.DTOs.Events;
using LinqKit;
using Fare;
using System.Text.RegularExpressions;
using Shared.ExternalServices.DTOs;
using Account.Application.DTOs;

namespace Account.Infrastructure.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<Company> _companyRepository;
        private readonly IAsyncRepository<Role> _roleRepository;
        private readonly IAsyncRepository<UserRole> _userRoleRepository;
        private readonly IAsyncRepository<UserStatus> _userStatusRepository;
        private readonly IAsyncRepository<AdminUser> _adminUserRepository;
        public readonly IMessagingService _messageBus;

        private readonly PasswordSettings _passwordSetting;
        private readonly IFileService _fileService;


        public UserService(IAsyncRepository<User> userRepository,
            IAsyncRepository<Role> roleRepository, IAsyncRepository<UserStatus> userStatusRepository, IAsyncRepository<UserRole> userRoleRepository, IAsyncRepository<AdminUser> adminUserRepository, IOptions<PasswordSettings> passwordSetting, IFileService fileService, IMessagingService messageBus, 
            IAuthenticatedUserService authenticatedUserService, IAsyncRepository<Company> companyRepository) : base(authenticatedUserService)
        {
            _messageBus = messageBus;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userStatusRepository = userStatusRepository;
            _userRoleRepository = userRoleRepository;
            _adminUserRepository = adminUserRepository;

            _passwordSetting = passwordSetting.Value;
            _fileService = fileService;
            _companyRepository = companyRepository;
        }

        public async Task<ApiResponse> GetUsers(UserQueryFilter filter, CancellationToken cancellationToken)
        {
            GetUserId();

            BasePageFilter pageFilter = new(filter.PageSize, filter.PageIndex);
            UserSortingDto sorting = new();
            if (filter.Sort == UserSortingEnum.NameDescending)
                sorting.IsNameDescending = true;
            else if (filter.Sort == UserSortingEnum.NameAscending)
                sorting.IsNameAscending = true;
            else if (filter.Sort == UserSortingEnum.DateAscending)
                sorting.IsDateAscending = true;
            else if (filter.Sort == UserSortingEnum.DateDescending)
                sorting.IsDateDescending = true;

            List<short> selectedRoleIds = new();
            if (!string.IsNullOrWhiteSpace(filter.RoleName))
            {
                var roles = Enum.GetValues(typeof(RoleStatusEnum)).Cast<RoleStatusEnum>()
                   .Select(e => new NameAndId<short>((short)e, e.ToDescription())).ToList();

                selectedRoleIds = roles.Where(x => x.Name.ToLower().Contains(filter.RoleName.ToLower())).Select(x => x.Id).ToList();
            }

            UserFilterDto userFilter = new()
            {
                RoleIds = selectedRoleIds,
                UserStatusCode = filter.UserStatusCode,
                SearchText = filter.SearchKeyword, 
                FromDate = filter.FromDate, 
                ToDate = filter.ToDate, 
                UserName = filter.UserName
            };

            string? companyName = null;
            if (!string.IsNullOrWhiteSpace(filter.CompanyCode))
            {
                var company = await _companyRepository.Table.FirstOrDefaultAsync(x => x.Code == filter.CompanyCode, cancellationToken: cancellationToken);
                if (company is not null)
                {
                    userFilter.CompanyId = company.Id;
                    companyName = company.Name;
                }
            }

            var expression = new UserQueryObject(userFilter).Expression;
            var orderExpression = ProcessOrderFunc(sorting);

            IQueryable<User> query;

            if (userFilter.CompanyId.HasValue)
            {
                query = _userRepository.Table.AsNoTrackingWithIdentityResolution().Where(x => x.AdminUsers.Any(a => a.CompanyId == userFilter.CompanyId.Value))
                    .Select(ux => new User
                    {
                        Id = ux.Id,
                        FirstName = ux.FirstName,
                        LastName = ux.LastName,
                        EmailAddress = ux.EmailAddress, 
                        UserName = ux.UserName, 
                        DistributorSapAccounts = ux.DistributorSapAccounts.Count == 0 ? new List<DistributorSapAccount>() : ux.DistributorSapAccounts.Select(x => new DistributorSapAccount
                        {
                            Id = x.Id
                        }).ToList(),
                        UserRoles = ux.UserRoles.Select(x => new UserRole
                        {
                            RoleId = x.RoleId,
                            Role = new Role
                            {
                                Id = x.Role.Id,
                                Name = x.Role.Name
                            }
                        }).ToList(),
                        AdminUsers = ux.AdminUsers.Any() ? ux.AdminUsers.Select(x => new AdminUser
                        {
                            Company = new Company
                            {
                                Id = x.Company.Id,
                                Name = x.Company.Name,
                                Code = x.Company.Code,
                                DisplayName = x.Company.DisplayName
                            },
                            CompanyId = x.CompanyId
                        }).ToList() : new List<AdminUser>(),
                        UserStatus = new UserStatus
                        {
                            Code = ux.UserStatus.Code,
                            Name = ux.UserStatus.Name
                        },
                        DateCreated = ux.DateCreated,
                        IsDeleted = ux.IsDeleted,
                        PhoneNumber = ux.PhoneNumber,
                        UserLogins = ux.UserLogins.Count > 0 ? ux.UserLogins.Select(x => ux.UserLogins.FirstOrDefault()).ToList() : new List<UserLogin>()
                    }).OrderByWhere(expression, orderExpression);
            }
            else
            {
                query = _userRepository.Table.AsNoTrackingWithIdentityResolution()
                    .Select(ux => new User
                    {
                        Id = ux.Id,
                        FirstName = ux.FirstName,
                        LastName = ux.LastName,
                        EmailAddress = ux.EmailAddress,
                        UserName = ux.UserName,
                        DistributorSapAccounts = ux.DistributorSapAccounts.Count == 0 ? new List<DistributorSapAccount>() : ux.DistributorSapAccounts.Select(x => new DistributorSapAccount
                        {
                            Id = x.Id
                        }).ToList(),
                        UserRoles = ux.UserRoles.Select(x => new UserRole
                        {
                            RoleId = x.RoleId,
                            Role = new Role
                            {
                                Id = x.Role.Id,
                                Name = x.Role.Name
                            }
                        }).ToList(),
                        AdminUsers = ux.AdminUsers.Any() ? ux.AdminUsers.Select(x => new AdminUser
                        {
                            Company = new Company
                            {
                                Id = x.Company.Id,
                                Name = x.Company.Name,
                                Code = x.Company.Code,
                                DisplayName = x.Company.DisplayName
                            },
                            CompanyId = x.CompanyId
                        }).ToList() : new List<AdminUser>(),
                        UserStatus = new UserStatus
                        {
                            Code = ux.UserStatus.Code,
                            Name = ux.UserStatus.Name
                        },
                        DateCreated = ux.DateCreated,
                        IsDeleted = ux.IsDeleted,
                        PhoneNumber = ux.PhoneNumber,
                        UserLogins = ux.UserLogins.Count > 0 ? ux.UserLogins.Select(x => ux.UserLogins.FirstOrDefault()).ToList() : new List<UserLogin>()
                    }).OrderByWhere(expression, orderExpression);
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var queryResult = query.Paginate(pageFilter.PageNumber, pageFilter.PageSize);
            var users = await queryResult.ToListAsync(cancellationToken);
            var totalPages = NumberManipulator.PageCountConverter(totalCount, pageFilter.PageSize);
            var response = new PaginatedList<UserResponse>(ProcessQuery(users, filter.CompanyCode, companyName), new PaginationMetaData(filter.PageIndex, filter.PageSize, totalPages, totalCount));

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_RETRIEVAL_OF_USER_LIST, response);
        }

        public async Task<ApiResponse> AddUsers(CreateUserRequest request, CancellationToken cancellationToken)
        {
            GetSuperAdminCredentials();

            var company = await _companyRepository.Table.FirstOrDefaultAsync(p => p.Code == request.CompanyCode, cancellationToken) ?? throw new ConflictException(ErrorMessages.COMPANY_NOT_FOUND, ErrorCodes.COMPANY_NOT_FOUND);
            if (await _userRepository.Table.AnyAsync(p => p.UserName.ToLower() == request.Username.ToLower(), cancellationToken))
                throw new ConflictException(ErrorMessages.USERNAME_ALEADY_EXIST, ErrorCodes.USERNAME_ALEADY_EXIST);

            if (await _userRepository.Table.AnyAsync(p => p.EmailAddress.ToLower() == request.EmailAddress.ToLower(), cancellationToken))
                throw new ConflictException(ErrorMessages.PROVIDED_EMAIL_CONFLICT_WITH_ANOTHER, ErrorCodes.PROVIDED_EMAIL_CONFLICT_WITH_ANOTHER);

            //We Need Config from configuration api
            var passwordExpiryDays = DateTime.UtcNow.AddDays(_passwordSetting.PasswordExpiryDays);

            //var userPassword = RandomValueGenerator.GenerateRegexString(_passwordSetting.RegexPattern);
            var userPassword = "Passw@rd123";
            var hash = EncryptionHelper.Hash(userPassword);

            User user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                Password = hash,
                PasswordResetRequired = true,
                PasswordExpiryDate = passwordExpiryDays,
                DateCreated = DateTime.UtcNow,
                EmailAddress = request.EmailAddress,
                CreatedByUserId = LoggedInUserId,
                IsDeleted = false,
                UserStatusId = (int)UserStatusEnum.Active,
            };

            var roles = request.RoleNames.ConvertAll(x => x.ToLower());
            var selectRoles = await _roleRepository.Table.Where(r => roles.Contains(r.Name.ToLower())).ToListAsync(cancellationToken);

            List<UserRole> userRoles = new();
            roles.ForEach(x =>
            {
                var role = selectRoles.FirstOrDefault(r => r.Name.ToLower() == x.ToLower()) ?? throw new ValidationException(ErrorMessages.ROLE_NOTFOUND, ErrorCodes.DEFAULT_VALIDATION_CODE);
                UserRole userRole = new()
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };
                userRoles.Add(userRole);
            });

            List<AdminUser> adminUsers = new()
            {
                new AdminUser
                {
                    CompanyId = company.Id,
                    UserId = user.Id
                }
            };

            user.UserRoles = userRoles;
            user.AdminUsers = adminUsers;
            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.CommitAsync(cancellationToken);

            var userStatus = await _userStatusRepository.Table.FirstOrDefaultAsync(x => x.Id == user.UserStatusId, cancellationToken);
            
            //Azure ServiceBus
            UserCreatedMessage adminUserCreatedMessage = new()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateCreated = DateTime.UtcNow,
                EmailAddress = user.EmailAddress,
                UserName = user.UserName,
                AccountStatus = new Application.DTOs.NameAndCode(userStatus.Name, userStatus.Code), 
                Roles = request.RoleNames, 
                PhoneNumber = user.PhoneNumber, 
                Password = userPassword
            };
            await _messageBus.PublishTopicMessage(adminUserCreatedMessage, EventMessages.ACCOUNT_USER_CREATED);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_CREATING_OF_USER);
        }

        public async Task<ApiResponse> UpdateUsers(UpdateUserRequest request, int userId, CancellationToken cancellationToken)
        {
            GetSuperAdminCredentials();

            var userDetail = await _userRepository.Table.Where(p => p.Id == userId && !p.IsDeleted)
                .Select(x => new User
                {
                    Id = x.Id,
                    CreatedByUserId = x.CreatedByUserId,
                    IsDeleted = x.IsDeleted,
                    DateCreated = x.DateCreated,
                    DateDeleted = x.DateDeleted,
                    DateModified = x.DateModified,
                    DeletedByUserId = x.DeletedByUserId,
                    DeviceId = x.DeviceId,
                    EmailAddress = x.EmailAddress,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LockedOutDate = x.LockedOutDate,
                    ModifiedByUserId = x.ModifiedByUserId,
                    Password = x.Password,
                    PasswordExpiryDate = x.PasswordExpiryDate,
                    PasswordResetRequired = x.PasswordResetRequired,
                    PhoneNumber = x.PhoneNumber,
                    ResetToken = x.ResetToken,
                    ResetTokenExpiryDate = x.ResetTokenExpiryDate,
                    UserName = x.UserName,
                    UserStatusId = x.UserStatusId,
                    ProfilePhotoCloudPath = x.ProfilePhotoCloudPath,
                    ProfilePhotoPublicUrl = x.ProfilePhotoPublicUrl,
                    UserStatus = new UserStatus
                    {
                        Id = x.UserStatus.Id,
                        Name = x.UserStatus.Name,
                        Code = x.UserStatus.Code
                    },
                    UserRoles = x.UserRoles.Select(r => new UserRole
                    {
                        Id = r.Id,
                        RoleId = r.RoleId,
                        UserId = r.UserId
                    }).ToList(), 
                    AdminUsers = x.AdminUsers.Select(a => new AdminUser
                    {
                        Id = a.Id, Company = new Company
                        {
                            Id = a.Company.Id,
                            Code = a.Company.Code, 
                            Name = a.Company.Name
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(ErrorMessages.USER_RECORD_NOT_FOUND, ErrorCodes.USER_RECORD_NOT_FOUND);
            var roles = await _roleRepository.Table.Select(x => new Role
            {
                Id = x.Id, 
                Name = x.Name
            }).ToListAsync(cancellationToken);

            var selectedRoles = new List<Role>();
            request.RoleNames.ForEach(role =>
            {
                var selectedRole = roles.FirstOrDefault(x => x.Name.ToLower() == role.ToLower()) ?? throw new Exception("The Role selected does not exist.");
                selectedRoles.Add(selectedRole);
            });

            List<AdminUser> newAdminUsers = new();
            if (!string.IsNullOrWhiteSpace(request.CompanyCode)) 
            {
                var company = await _companyRepository.Table.FirstOrDefaultAsync(x => x.Code == request.CompanyCode, cancellationToken: cancellationToken) ?? throw new Exception("The Company with the Company Code selected does not exist.");
                if (userDetail.AdminUsers.Any())
                {
                    if(userDetail.AdminUsers.FirstOrDefault().Company.Code != request.CompanyCode)
                    {
                        userDetail.AdminUsers.ElementAt(0).CompanyId = company.Id;
                    }
                }
                else
                {
                    newAdminUsers = new()
                    {
                        new AdminUser
                        {
                            CompanyId = company.Id,
                            UserId = userDetail.Id
                        }
                    };
                }
            }
                    

            var oldUser = new User
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                EmailAddress = userDetail.EmailAddress,
                Id = userDetail.Id,
                UserName = userDetail.UserName,
                UserStatus = userDetail.UserStatus
            };
           
            
            List<UserRole> userRolesToRemove = new();
            userDetail.UserRoles.ForEach(x =>
            {
                if (!selectedRoles.Any(a => a.Id == x.RoleId))
                    userRolesToRemove.Add(x);
            });

            List<UserRole> userRolesToAdd = new();
            selectedRoles.ForEach(x =>
            {
                if (!userDetail.UserRoles.Any(n => n.RoleId == x.Id))
                    userRolesToAdd.Add(new UserRole
                    {
                        UserId = userDetail.Id,
                        RoleId = x.Id
                    });
            });


            if(userRolesToRemove.Any())
                _userRoleRepository.DeleteRange(userRolesToRemove);

            if (userRolesToAdd.Any())
                await _userRoleRepository.AddRangeAsync(userRolesToAdd, cancellationToken);

            if (newAdminUsers.Any())
                await _adminUserRepository.AddRangeAsync(newAdminUsers, cancellationToken);

            User userToUpdate = new()
            {
                CreatedByUserId = userDetail.CreatedByUserId,
                DateCreated = userDetail.DateCreated,
                DateDeleted = userDetail.DateDeleted,
                DateModified = DateTime.UtcNow,
                DeletedByUserId = userDetail.DeletedByUserId,
                DeviceId = userDetail.DeviceId,
                EmailAddress = request.EmailAddress,
                FirstName = request.FirstName,
                Id = userDetail.Id,
                LastName = request.LastName,
                Password = userDetail.Password,
                UserStatusId = userDetail.UserStatusId,
                UserName = userDetail.UserName,
                ResetTokenExpiryDate = userDetail.ResetTokenExpiryDate,
                ResetToken = userDetail.ResetToken,
                PhoneNumber = userDetail.PhoneNumber,
                PasswordResetRequired = userDetail.PasswordResetRequired,
                ProfilePhotoPublicUrl = userDetail.ProfilePhotoPublicUrl,
                ProfilePhotoCloudPath = userDetail.ProfilePhotoCloudPath,
                PasswordExpiryDate = userDetail.PasswordExpiryDate,
                ModifiedByUserId = LoggedInUserId,
                LockedOutDate = userDetail.LockedOutDate,
                IsDeleted = userDetail.IsDeleted
            };
            _userRepository.Update(userToUpdate);
            await _userRepository.CommitAsync(cancellationToken);

            //Azure ServiceBus
            AdminUserUpdatedMessage adminUserUpdatedMessage = new()
            {
                UserId = oldUser.Id,
                OldFirstName = oldUser.FirstName,
                OldLastName = oldUser.LastName,
                DateModified = DateTime.UtcNow,
                OldEmailAddress = oldUser.EmailAddress,
                OldUsername = oldUser.UserName,
                OldAccountStatus = oldUser.UserStatus.Name,
                NewFirstName = request.FirstName,
                NewLastName = request.LastName,
                NewEmailAddress = request.EmailAddress,
                NewUsername = request.EmailAddress,
                NewAccountStatus = oldUser.UserStatus.Name, 
                DateCreated = oldUser.DateCreated
            };

            await _messageBus.PublishTopicMessage(adminUserUpdatedMessage, EventMessages.ACCOUNT_USER_UPDATED);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_UPDATED_USER);
        }

        public async Task<ApiResponse> UpdateProfile(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            GetUserId();

            var userDetail = await _userRepository.Table.Where(p => p.Id == LoggedInUserId && !p.IsDeleted)
                .Select(x => new User
                {
                    Id = x.Id,
                    CreatedByUserId = x.CreatedByUserId,
                    IsDeleted = x.IsDeleted,
                    DateCreated = x.DateCreated,
                    DateDeleted = x.DateDeleted,
                    DateModified = x.DateModified,
                    DeletedByUserId = x.DeletedByUserId,
                    DeviceId = x.DeviceId,
                    EmailAddress = x.EmailAddress,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LockedOutDate = x.LockedOutDate,
                    ModifiedByUserId = x.ModifiedByUserId,
                    Password = x.Password,
                    PasswordExpiryDate = x.PasswordExpiryDate,
                    PasswordResetRequired = x.PasswordResetRequired,
                    PhoneNumber = x.PhoneNumber,
                    ResetToken = x.ResetToken,
                    ResetTokenExpiryDate = x.ResetTokenExpiryDate,
                    UserName = x.UserName,
                    UserStatusId = x.UserStatusId,
                    ProfilePhotoCloudPath = x.ProfilePhotoCloudPath,
                    ProfilePhotoPublicUrl = x.ProfilePhotoPublicUrl,
                    UserStatus = new UserStatus
                    {
                        Id = x.UserStatus.Id,
                        Name = x.UserStatus.Name,
                        Code = x.UserStatus.Code
                    },
                    UserRoles = x.UserRoles.Select(r => new UserRole
                    {
                        Id = r.Id,
                        RoleId = r.RoleId,
                        UserId = r.UserId
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(ErrorMessages.USER_RECORD_NOT_FOUND, ErrorCodes.USER_RECORD_NOT_FOUND);
            var oldUser = new User
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                EmailAddress = userDetail.EmailAddress,
                Id = userDetail.Id,
                UserName = userDetail.UserName,
                UserStatus = userDetail.UserStatus,
                DeviceId = userDetail.DeviceId,
                PhoneNumber = userDetail.PhoneNumber,
                DateCreated = userDetail.DateCreated
            };

            if (!string.IsNullOrWhiteSpace(userDetail.ProfilePhotoCloudPath) && string.IsNullOrWhiteSpace(request.ProfilePhoto))
            {
                await _fileService.DeleteFile(userDetail.ProfilePhotoPublicUrl).ConfigureAwait(false);

                userDetail.ProfilePhotoPublicUrl = null;
                userDetail.ProfilePhotoCloudPath = null;
            }
            else if(!string.IsNullOrWhiteSpace(userDetail.ProfilePhotoCloudPath) && !string.IsNullOrWhiteSpace(request.ProfilePhoto))
            {
                var fileName = $"{userDetail.Id}_{DateTime.UtcNow.Ticks}";
                (UploadResponse? fileResponse, bool isValid) = await _fileService.FileUpload(base64String: request.ProfilePhoto, fileName, cancellationToken: cancellationToken);

                if (isValid)
                {
                    await _fileService.DeleteFile(userDetail.ProfilePhotoPublicUrl).ConfigureAwait(false);

                    userDetail.ProfilePhotoPublicUrl = fileResponse.PublicUrl;
                    userDetail.ProfilePhotoCloudPath = fileResponse.CloudUrl;
                }
            }
            else if (string.IsNullOrWhiteSpace(userDetail.ProfilePhotoCloudPath) && !string.IsNullOrWhiteSpace(request.ProfilePhoto))
            {
                var fileName = $"{userDetail.Id}_{DateTime.UtcNow.Ticks}";
                (UploadResponse? fileResponse, bool isValid) = await _fileService.FileUpload(base64String: request.ProfilePhoto, fileName, cancellationToken: cancellationToken);

                if (isValid)
                {
                    userDetail.ProfilePhotoPublicUrl = fileResponse.PublicUrl;
                    userDetail.ProfilePhotoCloudPath = fileResponse.CloudUrl;
                }
            }
            else
                throw new ValidationException(ErrorMessages.UNABLE_TO_UPDATE_USER_PROFILE, ErrorCodes.INCOMPLETE_INPUT);

            userDetail.DateModified = DateTime.UtcNow;
            userDetail.ModifiedByUserId = LoggedInUserId;
            _userRepository.Update(userDetail);
            await _userRepository.CommitAsync(cancellationToken);

            //Azure ServiceBus
            AccountsUserUpdatedMessage adminUserUpdatedMessage = new()
            {
                UserId = oldUser.Id,
                OldFirstName = oldUser.FirstName,
                OldLastName = oldUser.LastName,
                DateModified = DateTime.UtcNow,
                OldEmailAddress = oldUser.EmailAddress,
                OldDeviceId = oldUser.DeviceId,
                OldUserName = oldUser.UserName,
                OldAccountStatus = new Application.DTOs.NameAndCode(oldUser.UserStatus.Name, oldUser.UserStatus.Code),
                ModifiedByUserId = LoggedInUserId,
                NewFirstName = userDetail.FirstName,
                NewLastName = userDetail.LastName,
                NewEmailAddress = userDetail.EmailAddress,
                NewDeviceId = userDetail.DeviceId,
                NewPhoneNumber = userDetail.PhoneNumber,
                OldPhoneNumber = oldUser.PhoneNumber,
                NewUserName = userDetail.UserName,
                NewAccountStatus = new Application.DTOs.NameAndCode(userDetail.UserStatus.Name, userDetail.UserStatus.Code),
                DateCreated = oldUser.DateCreated
            };

            await _messageBus.PublishTopicMessage(adminUserUpdatedMessage, EventMessages.ACCOUNT_USER_UPDATED);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_UPDATED_USER);
        }

        public async Task<ApiResponse> ActivateDeactivateUsers(ActivateDeactivateUserRequest request, CancellationToken cancellationToken)
        {
            GetSuperAdminCredentials();

            var userDetail = await _userRepository.Table.FirstOrDefaultAsync(p => p.Id == request.UserId && !p.IsDeleted, cancellationToken) ?? throw new NotFoundException(ErrorMessages.USER_RECORD_NOT_FOUND, ErrorCodes.USER_RECORD_NOT_FOUND);
            if (request.Activate)
                userDetail.UserStatusId = (int)UserStatusEnum.Active;
            else
                userDetail.UserStatusId = (int)UserStatusEnum.Inactive;

            userDetail.DateModified = DateTime.UtcNow;
            userDetail.ModifiedByUserId = LoggedInUserId;

            _userRepository.Update(userDetail);
            await _userRepository.CommitAsync(cancellationToken);

            //Azure ServiceBus
            AdminUserUpdatedMessage adminUserUpdatedMessage = new()
            {
                UserId = userDetail.Id,
                OldFirstName = userDetail.FirstName,
                OldLastName = userDetail.LastName,
                DateModified = DateTime.UtcNow,
                OldEmailAddress = userDetail.EmailAddress,
                OldUsername = userDetail.UserName,
                OldAccountStatus = _userStatusRepository.Table.Where(u => u.Id == userDetail.UserStatusId).FirstOrDefault().Name

            };

            await _messageBus.PublishTopicMessage(adminUserUpdatedMessage, EventMessages.ACCOUNT_USER_UPDATED);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_UPDATED_USER);
        }

        public async Task<ApiResponse> DeleteUsers(int userId, CancellationToken cancellationToken)
        {
            GetSuperAdminCredentials();

            var userDetail = await _userRepository.Table.FirstOrDefaultAsync(p => p.Id == userId && !p.IsDeleted, cancellationToken) ?? throw new NotFoundException(ErrorMessages.USER_RECORD_NOT_FOUND, ErrorCodes.USER_RECORD_NOT_FOUND);
            userDetail.IsDeleted = true;
            userDetail.DateDeleted = DateTime.UtcNow;
            userDetail.DeletedByUserId = LoggedInUserId;

            _userRepository.Update(userDetail);
            await _userRepository.CommitAsync(cancellationToken);

            //Azure ServiceBus
            AdminUserDeletedMessage adminUserDeletedMessage = new()
            {
                UserId = userDetail.Id,
                DateDeleted = DateTime.UtcNow,
                DeletedByUserId = LoggedInUserId, 
                DateCreated = userDetail.DateCreated
            };

            await _messageBus.PublishTopicMessage(adminUserDeletedMessage, EventMessages.ACCOUNT_USER_DELETED);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_DELETED_USER);
        }

        public async Task<ApiResponse> ExportUsers(ExportUsersDataQueryFilter filter, CancellationToken cancellationToken)
        {
            GetUserId();

            if (!filter.Format.HasValue)
                throw new ValidationException(ErrorMessages.INVALID_FILE_DOWNLOAD_FORMAT, ErrorCodes.DEFAULT_VALIDATION_CODE);

            var response = new FileResponse();

            UserSortingDto sorting = new();
            if (filter.Sort == UserSortingEnum.NameDescending)
                sorting.IsNameDescending = true;
            else if (filter.Sort == UserSortingEnum.NameAscending)
                sorting.IsNameAscending = true;
            else if (filter.Sort == UserSortingEnum.DateAscending)
                sorting.IsDateAscending = true;
            else if (filter.Sort == UserSortingEnum.DateDescending)
                sorting.IsDateDescending = true;

            List<short> selectedRoleIds = new();
            if (!string.IsNullOrWhiteSpace(filter.RoleName))
            {
                var roles = Enum.GetValues(typeof(RoleStatusEnum)).Cast<RoleStatusEnum>()
                   .Select(e => new NameAndId<short>((short)e, e.ToDescription())).ToList();

                selectedRoleIds = roles.Where(x => x.Name.ToLower().Contains(filter.RoleName.ToLower())).Select(x => x.Id).ToList();
            }

            UserFilterDto userFilter = new()
            {
                RoleIds = selectedRoleIds,
                UserStatusCode = filter.UserStatusCode,
                SearchText = filter.SearchKeyword,
                FromDate = filter.FromDate,
                ToDate = filter.ToDate
            };
            var expression = new UserQueryObject(userFilter).Expression;
            var orderExpression = ProcessOrderFunc(sorting);
            var query = _userRepository.Table.Where(u => !u.IsDeleted).AsNoTrackingWithIdentityResolution()
                    .OrderByWhere(expression, orderExpression);
            var totalCount = await query.CountAsync(cancellationToken);

            query = query.Select(ux => new User
            {
                Id = ux.Id,
                FirstName = ux.FirstName,
                LastName = ux.LastName,
                EmailAddress = ux.EmailAddress,
                DistributorSapAccounts = ux.DistributorSapAccounts.Count == 0 ? new List<DistributorSapAccount>() : ux.DistributorSapAccounts.Select(x => new DistributorSapAccount
                {
                    Id = x.Id
                }).ToList(),
                UserRoles = ux.UserRoles.Select(x => new UserRole
                {
                    Role = new Role
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name
                    }
                }).ToList(),
                UserStatus = new UserStatus
                {
                    Code = ux.UserStatus.Code,
                    Name = ux.UserStatus.Name
                }, 
                DateCreated = ux.DateCreated, 
                IsDeleted = ux.IsDeleted, 
                UserName = ux.UserName, 
                PhoneNumber = ux.PhoneNumber,
                UserLogins = ux.UserLogins.Count != 0 ? ux.UserLogins.Select(x => ux.UserLogins.FirstOrDefault()).ToList() : new List<UserLogin>()
            });

            var users = await query.ToListAsync(cancellationToken);

            var results = users;
            if (filter.Format == ExportFileTypeEnum.Xls)
            {
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "Users_Data_Export.xlsx";

                using XLWorkbook? workbook = new();
                IXLWorksheet worksheet =
                workbook.Worksheets.Add("UserLists");
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "FirstName";
                worksheet.Cell(1, 3).Value = "LastName";
                worksheet.Cell(1, 4).Value = "EmailAddress";
                worksheet.Cell(1, 5).Value = "Status";
                worksheet.Cell(1, 6).Value = "PhoneNumber";
                worksheet.Cell(1, 7).Value = "DateCreated";
                //worksheet.Cell(1, 8).Value = "Role";
                for (int index = 1; index <= results.Count; index++)
                {
                    worksheet.Cell(index + 1, 1).Value = results[index - 1].Id;
                    worksheet.Cell(index + 1, 2).Value = results[index - 1].FirstName;
                    worksheet.Cell(index + 1, 3).Value = results[index - 1].LastName;
                    worksheet.Cell(index + 1, 4).Value = results[index - 1].EmailAddress;
                    worksheet.Cell(index + 1, 5).Value = results[index - 1].UserStatus?.Name;
                    worksheet.Cell(index + 1, 6).Value = results[index - 1].PhoneNumber;
                    worksheet.Cell(index + 1, 7).Value = results[index - 1].DateCreated;
                    //worksheet.Cell(index + 1, 8).Value = results[index - 1].UserRoles.FirstOrDefault(ur => ur.UserId == results[index - 1].Id).Role?.Name;
                }
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                response = new FileResponse
                {
                    Content = content,
                    ContentType = contentType,
                    FileName = fileName
                };
            }
            if (filter.Format == ExportFileTypeEnum.Csv)
            {
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "Users_Data_Export.csv";

                using XLWorkbook? workbook = new();
                IXLWorksheet worksheet =
                workbook.Worksheets.Add("UserLists");
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "FirstName";
                worksheet.Cell(1, 3).Value = "LastName";
                worksheet.Cell(1, 4).Value = "EmailAddress";
                worksheet.Cell(1, 5).Value = "Status";
                worksheet.Cell(1, 6).Value = "PhoneNumber";
                worksheet.Cell(1, 7).Value = "DateCreated";
                //worksheet.Cell(1, 8).Value = "Role";
                for (int index = 1; index <= results.Count; index++)
                {
                    worksheet.Cell(index + 1, 1).Value = results[index - 1].Id;
                    worksheet.Cell(index + 1, 2).Value = results[index - 1].FirstName;
                    worksheet.Cell(index + 1, 3).Value = results[index - 1].LastName;
                    worksheet.Cell(index + 1, 4).Value = results[index - 1].EmailAddress;
                    worksheet.Cell(index + 1, 5).Value = results[index - 1].UserStatus?.Name;
                    worksheet.Cell(index + 1, 6).Value = results[index - 1].PhoneNumber;
                    worksheet.Cell(index + 1, 7).Value = results[index - 1].DateCreated;
                    //worksheet.Cell(index + 1, 8).Value = results[index - 1].UserRoles.FirstOrDefault(ur => ur.UserId == results[index - 1].Id).Role?.Name;
                }
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                response = new FileResponse
                {
                    Content = content,
                    ContentType = contentType,
                    FileName = fileName
                };
            }
            if (filter.Format == ExportFileTypeEnum.PdfFile)
            {
                string contentType = "application/pdf";
                string fileName = "Users_Data_Export.pdf";
                
                DataTable dataTable = new("UserDetails");
                dataTable.Columns.AddRange(new DataColumn[7] { new DataColumn("Id"),
                                            new DataColumn("FirstName"),
                                            new DataColumn("LastName"),
                                             new DataColumn("EmailAddress"),
                                            new DataColumn("Status"),
                                             new DataColumn("PhoneNumber"),
                                            new DataColumn("DateCreated") });
                foreach (var item in results)
                {
                    dataTable.Rows.Add(item.Id, item.FirstName,
                        item.LastName, item.EmailAddress,
                        item.UserStatus?.Name, item.PhoneNumber, item.DateCreated);
                }

                var document = new Document
                {
                    PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 30) }
                };
                var pdfPage = document.Pages.Add();
                Table table = new()
                {
                    ColumnWidths = "7% 16% 16% 16% 10% 16% 15%",
                    DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                    Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Color.Black),
                };

                table.ImportDataTable(dataTable, true, 0, 0);
                document.Pages[1].Paragraphs.Add(table);

                using var memoryStream = new MemoryStream();
                document.Save(memoryStream);
                var content = memoryStream.ToArray();
                response = new FileResponse
                {
                    Content = content,
                    ContentType = contentType,
                    FileName = fileName
                };
            }

            var documentResponse = new FileContentResult(response.Content, response.ContentType)
            {
                FileDownloadName = response.FileName
            };
            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_RETRIEVAL_OF_USER_LIST, documentResponse);
        }

        public async Task<ApiResponse> GetUserProfile(CancellationToken cancellationToken)
        {
            GetUserId();

            var userDetail = await _userRepository.Table.Where(p => p.Id == LoggedInUserId).Select(ux => new User
            {
                Id = ux.Id,
                FirstName = ux.FirstName,
                LastName = ux.LastName,
                EmailAddress = ux.EmailAddress, 
                UserName = ux.UserName,  
                PhoneNumber = ux.PhoneNumber, 
                ProfilePhotoPublicUrl = ux.ProfilePhotoPublicUrl,
                DateCreated = ux.DateCreated,
                DistributorSapAccounts = ux.DistributorSapAccounts.Count == 0 ? new List<DistributorSapAccount>() : ux.DistributorSapAccounts.Select(x => new DistributorSapAccount
                {
                    Id = x.Id
                }).ToList(),
                UserRoles = ux.UserRoles.Select(x => new UserRole
                {
                    Role = new Role
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name
                    }
                }).ToList(),
                UserStatus = new UserStatus
                {
                    Code = ux.UserStatus.Code,
                    Name = ux.UserStatus.Name
                },
                UserLogins = ux.UserLogins.Count != 0 ? ux.UserLogins.Select(x => new UserLogin
                {
                      LoginDate = x.LoginDate
                }).ToList() : new List<UserLogin>()

            }).FirstOrDefaultAsync(cancellationToken);

            var userResponse = ProcessQuery(userDetail);

            return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_RETRIEVAL_OF_USER, userResponse);
        }

        #region Private Methods
        private static Func<IQueryable<User>, IOrderedQueryable<User>> ProcessOrderFunc(UserSortingDto? orderExpression = null)
        {
            IOrderedQueryable<User> orderFunction(IQueryable<User> queryable)
            {
                if (orderExpression == null)
                    return queryable.OrderByDescending(p => p.DateCreated);

                var orderQueryable = orderExpression.IsNameAscending
                   ? queryable.OrderBy(p => p.LastName).ThenByDescending(p => p.DateCreated)
                   : orderExpression.IsNameDescending
                       ? queryable.OrderByDescending(p => p.LastName).ThenByDescending(p => p.DateCreated)
                       : orderExpression.IsDateAscending 
                           ? queryable.OrderBy(p => p.DateCreated)
                           : orderExpression.IsDateDescending 
                               ? queryable.OrderByDescending(p => p.DateCreated)
                               : queryable.OrderByDescending(p => p.DateCreated);
                return orderQueryable;
            }
            return orderFunction;
        }

        private static IReadOnlyList<UserResponse> ProcessQuery(IReadOnlyList<User> users, string companyCode, string companyName)
        {
            return users.Select(p =>
            {
                DateTime? lastLoginDate = p.UserLogins.Count == 0 ? null : p.UserLogins.FirstOrDefault().LoginDate;
                var numberOfSapAccounts = p.DistributorSapAccounts.Count;
                var userStatus = new UserStatusResponse
                { 
                    Code = p.UserStatus.Code, 
                    Name = p.UserStatus.Name 
                };

                var company = p.AdminUsers.Any() ? p.AdminUsers.FirstOrDefault().Company : null;

                var item = new UserResponse(p.Id, p.FirstName, p.LastName, p.EmailAddress, p.UserName, userStatus, numberOfSapAccounts, p.UserRoles.Select(x => x.Role.Name).ToList(), p.DateCreated, lastLoginDate, company is null ? null : company.Code, company is null ? null : company.Name);
                return item;
            }).ToList();
        }

        private static UserDetailResponse ProcessQuery(User user)
        {
            if (user == null)
                return null;

            var userResponse = new UserDetailResponse(new Application.ViewModels.Responses.ResponseDto.UserDetailResponseDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress, 
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName, 
                ProfilePhotoPublicUrl = user.ProfilePhotoPublicUrl,
                DateCreated = user.DateCreated,
                NumOfSapAccounts = user.DistributorSapAccounts.Count,
                LastLoginDate = user.UserLogins.Count == 0 ? null : user.UserLogins.OrderByDescending(x => x.LoginDate).FirstOrDefault().LoginDate
            }); 
            return userResponse;
        }
        #endregion
    }
}
