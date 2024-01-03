using System;
using System.Collections.Generic;

namespace Shared.Data.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            DistributorSapAccounts = new HashSet<DistributorSapAccount>();
        }

        public byte Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<DistributorSapAccount> DistributorSapAccounts { get; set; }
    }
}
