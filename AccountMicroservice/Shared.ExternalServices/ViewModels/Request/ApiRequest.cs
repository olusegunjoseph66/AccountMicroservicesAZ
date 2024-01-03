using Shared.ExternalServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Shared.ExternalServices.Configurations.SapServiceUrls;

namespace Shared.ExternalServices.ViewModels.Request
{
    public class ApiRequest
    {
        public ApiTypeEnum ApiType { get; set; } = ApiTypeEnum.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
