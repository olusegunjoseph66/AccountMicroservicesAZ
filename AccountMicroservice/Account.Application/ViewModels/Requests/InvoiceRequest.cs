using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.ViewModels.Requests
{
    public class InvoiceRequest
    {
        public int DistributorSapAccountId { get; set; }
        public DateTime? BillingDate { get; set; }
        public string AtcNumber { get; set; }
    }
}
