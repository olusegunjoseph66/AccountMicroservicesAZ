namespace Account.Application.ViewModels.Requests
{
    public class AuthenticationRequest
    {
        public string ChannelCode { get; set; }        
        public string DeviceId { get; set; }        
        public string UserName { get; set; }        
        public string IpAddress { get; set; }
        public string Password { get; set; }
    }
}
