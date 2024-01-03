using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class DistributorSapAccount
    {
        public int Id { get; set; }
        public string DistributorName { get; set; } = null!;
        public byte AccountTypeId { get; set; }
        public string CountryCode { get; set; } = null!;
        public string DistributorSapNumber { get; set; } = null!;
        public string CompanyCode { get; set; } = null!;
        public int UserId { get; set; }
        public string? FriendlyName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual AccountType AccountType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
