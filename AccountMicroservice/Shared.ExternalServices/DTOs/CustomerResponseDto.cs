using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.DTOs
{
    public class CustomerResponseDto
    {
        public string Id { get; set; }
        public string DistributorName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountType { get; set; }
        public NameAndCodeResponse Status { get; set; }
    }
}
