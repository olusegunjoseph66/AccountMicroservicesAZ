namespace Account.Application.ViewModels.Requests
{
    public class InitiateRegistrationRequest
    {
        public string DistributorNumber { get; set; }
        public string CountryCode { get; set; }
        public string CompanyCode { get; set; }
        public string ChannelCode { get; set; }
        public bool IsPrivacyPolicyAccepted { get; set; }
        public string? DeviceId { get; set; }
    }
}
