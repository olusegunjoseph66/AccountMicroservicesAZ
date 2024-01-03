using Shared.ExternalServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.ViewModels.Response
{
    public class CountryListResponse
    {
        public List<NameAndCodeDto> Countries { get; set; }
    }
}
