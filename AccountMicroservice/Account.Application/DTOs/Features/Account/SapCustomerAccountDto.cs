using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.DTOs.Features.Account
{
    public class SapCustomerAccountDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyCode { get; set; }
        public string CountryCode { get; set; }
        public string DistributorName { get; set; }
        public string DistributorNumber { get; set; }
        public NameAndCode Status { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountType { get; set; }
        public string FriendlyName { get; set; }
    }
}
