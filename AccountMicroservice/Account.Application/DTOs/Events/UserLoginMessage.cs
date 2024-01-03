using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class UserLoginMessage : IntegrationBaseMessage
    {

        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string ChannelCode { get; set; }
        public string DeviceId { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
