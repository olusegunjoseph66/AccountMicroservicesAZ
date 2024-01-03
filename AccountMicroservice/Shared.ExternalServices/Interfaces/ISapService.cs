using Shared.ExternalServices.ViewModels.Request;
using Shared.ExternalServices.ViewModels.Response;

namespace Shared.ExternalServices.Interfaces
{
    public interface ISapService
    {
        Task<SAPCustomerResponse> FindCustomer(string companyCode, string countryCode, string distributorNumber);

        Task<(bool, bool)> RequestStatement(SAPStatementRequest request);

        Task<(bool, bool, string)> RequestInvoiceStatement(SAPInvoiceStatementRequest request);

        Task<(bool, bool, string)> RequestInvoiceList(SAPInvoiceListRequest request);
    }
}