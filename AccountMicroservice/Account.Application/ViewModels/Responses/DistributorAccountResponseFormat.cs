namespace Account.Application.ViewModels.Responses
{
    public record DistributorAccountResponseFormat
    {
        public int SapAccountId { get; set; }
        public string CountryCode { get; set; }
        public string CompanyCode { get; set; }
        public string DistributorSapNumber { get; set; }
        public string FriendlyName { get; set; }
        public DateTime DateCreated { get; set; }
        public AccountTypeResponse AccountType { get; set; }
        
    }
}
