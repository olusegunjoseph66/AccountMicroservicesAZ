using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.DTO.Pagination;

namespace Account.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("statement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> RequestStatement([FromBody] StatementRequest request, CancellationToken cancellationToken) => Response(await _reportService.RequestStatement(request, cancellationToken));

        [HttpPost("invoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> RequestInvoiceStatement([FromBody] InvoiceRequest request, CancellationToken cancellationToken) => Response(await _reportService.RequestInvoiceStatement(request, cancellationToken));

        [HttpPost("invoice/list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<EmptyResponse>))]
        public async Task<IActionResult> RequestInvoiceList([FromBody] InvoiceRequest request, CancellationToken cancellationToken) => Response(await _reportService.RequestInvoiceList(request, cancellationToken));
    }
}
