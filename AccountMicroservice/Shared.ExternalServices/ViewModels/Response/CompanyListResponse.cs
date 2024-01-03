
using Shared.ExternalServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.ViewModels.Response
{
    public class CompanyListResponse
    {
        public List<NameAndCodeDto> Companies { get; set; }
    }
}
