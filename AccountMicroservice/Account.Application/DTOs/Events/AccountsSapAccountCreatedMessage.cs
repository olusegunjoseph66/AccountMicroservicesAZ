using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class AccountsSapAccountCreatedMessage : IntegrationBaseMessage
    {
        public int DistributorSapAccountId { get; set; }
        public int UserId { get; set; }
        public string? FriendlyName { get; set; }
        public string DistributorName { get; set; }
        public string DistributorSapNumber { get; set; }
        public string CompanyCode { get; set; }
        public string CountryCode { get; set; }
        public NameAndCode AccountType { get; set; }
        public bool? IsMessageRequired { get; set; }
    }
}
