using Account.Application.DTOs.APIDataFormatters;
using Account.Application.ViewModels.Requests;

namespace Account.Application.Interfaces.Services
{
    public interface IReportService
    {
        Task<ApiResponse> RequestStatement(StatementRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> RequestInvoiceStatement(InvoiceRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> RequestInvoiceList(InvoiceRequest request, CancellationToken cancellationToken);
    }
}