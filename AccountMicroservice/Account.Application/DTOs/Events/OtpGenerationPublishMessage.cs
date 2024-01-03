using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class OtpGenerationPublishMessage : IntegrationBaseMessage
    {
        public long OtpId { get; set; }
        public string OtpCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateExpiry { get; set; }
    }

}
