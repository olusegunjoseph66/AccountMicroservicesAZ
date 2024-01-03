namespace Account.Application.ViewModels.Requests
{
    public class LinkDistributorAccountRequest
    {
        public string CountryCode { get; set; }
        public string CompanyCode { get; set; }
        public string DistributorNumber { get; set; }
        public string FriendlyName { get; set; }
    }
}
