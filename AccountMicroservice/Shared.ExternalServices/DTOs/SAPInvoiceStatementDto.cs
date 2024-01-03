using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.DTOs
{
    public class SAPInvoiceStatementDto
    {
        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("distributorNumber")]
        public string DistributorNumber { get; set; }
        [JsonProperty("billingDate")]
        public string BillingDate { get; set; }
        [JsonProperty("atcNumber")]
        public string AtcNumber { get; set; }
    }
}
