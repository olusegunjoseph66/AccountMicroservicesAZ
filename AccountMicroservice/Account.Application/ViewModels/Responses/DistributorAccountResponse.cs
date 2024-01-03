using Account.Application.DTOs;

namespace Account.Application.ViewModels.Responses
{
    public class DistributorAccountResponse
    {
        public int SapAccountId { get; set; }
        public string CountryCode { get; set; }
        public string CompanyCode { get; set; }
        public string DistributorSapNumber { get; set; }
        public  string DistributorName { get; set; }
        public string CompanyDisplayName { get; set; }
        public string FriendlyName { get; set; }
        public DateTime DateCreated { get; set; }
        public NameAndCode AccountType { get; set; }

        public DistributorAccountResponse(int id, string companyCode, string countryCode, string friendlyName, string distributorNumber, string distributorName, DateTime dateCreated, NameAndCode accountType, string companyName)
        {
            CountryCode = countryCode;
            CompanyCode = companyCode;
            FriendlyName = friendlyName;
            DistributorSapNumber = distributorNumber;
            DistributorName = distributorName;
            SapAccountId = id;
            DateCreated = dateCreated;
            AccountType = accountType;
            CompanyDisplayName = companyName;
        }
    }
}
