using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.ViewModels.Requests
{
    public class TwoFactorCompletionRequest : ValidateOtpRequest
    {
        public string? ChannelCode { get; set; }
        public string? DeviceId { get; set; }
        public string? IpAddress { get; set; }
    }
}
