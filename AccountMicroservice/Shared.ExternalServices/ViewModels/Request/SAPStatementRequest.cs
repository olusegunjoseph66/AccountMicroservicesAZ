using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.ViewModels.Request
{
    public class SAPStatementRequest
    {
        public string CompanyCode { get; set; }
        public string CountryCode { get; set; }
        public string DistributorNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
