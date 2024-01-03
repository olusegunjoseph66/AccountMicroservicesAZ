using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class OtpPublishMessage : IntegrationBaseMessage
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateExpiry { get; set; }
        public long OtpId { get; set; }
        public string OtpCode { get; set; }
    }
}
