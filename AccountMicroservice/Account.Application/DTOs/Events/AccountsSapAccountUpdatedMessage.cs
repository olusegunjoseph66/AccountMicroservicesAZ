using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class AccountsSapAccountUpdatedMessage : IntegrationBaseMessage
    {
        public int DistributorSapAccountId { get; set; }
        public string DistributorName { get; set; } = null!;
        public int UserId { get; set; }
        public string OldFriendlyName { get; set; }
        public string NewFriendlyName { get; set; }
        public string DistributorSapNumber { get; set; }
        public DateTime DateModified { get; set; }


    }
}
