using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class User
    {
        public User()
        {
            AdminUsers = new HashSet<AdminUser>();
            DeletionRequests = new HashSet<DeletionRequest>();
            DistributorSapAccounts = new HashSet<DistributorSapAccount>();
            Otps = new HashSet<Otp>();
            UserChangeLogs = new HashSet<UserChangeLog>();
            UserLogins = new HashSet<UserLogin>();
            UserPasswordHistories = new HashSet<UserPasswordHistory>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? EmailAddress { get; set; }
        public string UserName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? DeviceId { get; set; }
        public byte UserStatusId { get; set; }
        public string? ProfilePhotoPublicUrl { get; set; }
        public string? ProfilePhotoCloudPath { get; set; }
        public bool PasswordResetRequired { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiryDate { get; set; }
        public DateTime PasswordExpiryDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? DeletedByUserId { get; set; }
        public DateTime? LockedOutDate { get; set; }
        public bool? IsPrivacyPolicyAccepted { get; set; }

        public virtual UserStatus UserStatus { get; set; } = null!;
        public virtual ICollection<AdminUser> AdminUsers { get; set; }
        public virtual ICollection<DeletionRequest> DeletionRequests { get; set; }
        public virtual ICollection<DistributorSapAccount> DistributorSapAccounts { get; set; }
        public virtual ICollection<Otp> Otps { get; set; }
        public virtual ICollection<UserChangeLog> UserChangeLogs { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserPasswordHistory> UserPasswordHistories { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
