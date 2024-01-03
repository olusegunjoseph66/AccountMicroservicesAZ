using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class UserChangeLog
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string ChangeType { get; set; } = null!;
        public string? OldFirstName { get; set; }
        public string? OldLastName { get; set; }
        public string? OldEmailAddress { get; set; }
        public string? OldUserName { get; set; }
        public string? OldDeviceId { get; set; }
        public string? OldAccountStatusCode { get; set; }
        public DateTime? OldPasswordExpiryDate { get; set; }
        public DateTime? OldDateModified { get; set; }
        public int? OldModifiedByUserId { get; set; }
        public bool? OldIsDeleted { get; set; }
        public DateTime? OldDateDeleted { get; set; }
        public int? OldDeletedByUserId { get; set; }
        public DateTime? OldLockedOutDate { get; set; }
        public string? NewFirstName { get; set; }
        public string? NewLastName { get; set; }
        public string? NewEmailAddress { get; set; }
        public string? NewUserName { get; set; }
        public string? NewDeviceId { get; set; }
        public string? NewAccountStatusCode { get; set; }
        public DateTime? NewPasswordExpiryDate { get; set; }
        public DateTime? NewDateModified { get; set; }
        public int? NewModifiedByUserId { get; set; }
        public bool? NewIsDeleted { get; set; }
        public DateTime? NewDateDeleted { get; set; }
        public int? NewDeletedByUserId { get; set; }
        public DateTime? NewLockedOutDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
