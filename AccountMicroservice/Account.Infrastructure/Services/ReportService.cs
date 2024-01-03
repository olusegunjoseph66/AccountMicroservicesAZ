using Account.Application.Configurations;
using Account.Application.Constants;
using Account.Application.DTOs.Sortings;
using Account.Application.Enums;
using Account.Application.Exceptions;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Account.Infrastructure.QueryObjects;
using ClosedXML.Excel;
using Aspose.Pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Data.Extensions;
using Shared.Data.Models;
using Shared.Data.Repository;
using Shared.ExternalServices.Interfaces;
using Shared.Utilities.DTO.Pagination;
using Shared.Utilities.Helpers;
using System.Data;
using Account.Application.DTOs.APIDataFormatters;
using Microsoft.AspNetCore.Mvc;
using Account.Application.DTOs.Events;
using LinqKit;
using Fare;
using System.Text.RegularExpressions;
using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.Configurations;
using Microsoft.Extensions.Configuration;

namespace Account.Infrastructure.Services
{
    public class ReportService : BaseService, IReportService
    {
        private readonly IAsyncRepository<DistributorSapAccount> _distributorAccountRepository;
        private readonly ISapService _sapService;
        private readonly BillingDocumentType _billingDocumentType;
        private readonly IConfiguration _config;

        public ReportService(IAsyncRepository<DistributorSapAccount> distributorAccountRepository, ISapService sapService, IOptions<BillingDocumentType> billingDocumentType,
            IAuthenticatedUserService authenticatedUserService, IConfiguration _config) : base(authenticatedUserService)
        {
            _distributorAccountRepository = distributorAccountRepository;
            _sapService = sapService;
            _billingDocumentType = billingDocumentType.Value;
            this._config = _config;
        }

        public async Task<ApiResponse> RequestStatement(StatementRequest request, CancellationToken cancellationToken)
        {
            GetUserId();

            var distributorAccount = await _distributorAccountRepository.Table.FirstOrDefaultAsync(p => p.UserId == LoggedInUserId && p.Id == request.DistributorSapAccountId, cancellationToken) ?? throw new NotFoundException(ErrorMessages.SAP_ACCOUNT_NOTFOUND, ErrorCodes.SAP_ACCOUNT_NOTFOUND_CODE);
            (bool result, bool isStatementFound) = await _sapService.RequestStatement(new Shared.ExternalServices.ViewModels.Request.SAPStatementRequest 
            { 
                CompanyCode = distributorAccount.CompanyCode,
                CountryCode = distributorAccount.CountryCode,
                DistributorNumber = distributorAccount.DistributorSapNumber, 
                FromDate = request.FromDate, 
                ToDate = request.ToDate
            });

            if (result)
            {
                if(isStatementFound)
                    return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_STATEMENT_REQUEST);
                else
                    return ResponseHandler.SuccessResponse(SuccessMessages.COMPANY_STATEMENT_REQUEST_NOTFOUND, SuccessCodes.STATEMENT_NOT_AVAILABLE);
            }
            else
                return ResponseHandler.FailureResponse(ErrorCodes.FAILED_SAP_REQUEST_CODE, ErrorMessages.COMPANY_STATEMENT_REQUEST_FAILURE);
        }

        public async Task<ApiResponse> RequestInvoiceStatement(InvoiceRequest request, CancellationToken cancellationToken)
        {
            GetUserId();

            var distributorAccount = await _distributorAccountRepository.Table.FirstOrDefaultAsync(p => p.UserId == LoggedInUserId && p.Id == request.DistributorSapAccountId, cancellationToken) ?? throw new NotFoundException(ErrorMessages.SAP_ACCOUNT_NOTFOUND, ErrorCodes.SAP_ACCOUNT_NOTFOUND_CODE);
            (bool result, bool isStatementFound, string message) = await _sapService.RequestInvoiceStatement( new Shared.ExternalServices.ViewModels.Request.SAPInvoiceStatementRequest
            {
                CompanyCode = distributorAccount.CompanyCode,
                CountryCode = distributorAccount.CountryCode,
                DistributorNumber = distributorAccount.DistributorSapNumber,
                BillingDate = request.BillingDate.HasValue ? request.BillingDate.Value : DateTime.UtcNow,
                AtcNumber = request.AtcNumber
            });

            if (result)
            {
                if (isStatementFound)
                    return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_INVOICE_REQUEST);
                else
                    return ResponseHandler.SuccessResponse(SuccessMessages.INVOICE_REQUEST_NOTFOUND, SuccessCodes.INVOICE_NOT_AVAILABLE);
            }
            else
                return ResponseHandler.FailureResponse(ErrorCodes.FAILED_SAP_REQUEST_CODE, ErrorMessages.INVOICE_REQUEST_FAILURE);
        }

        public async Task<ApiResponse> RequestInvoiceList(InvoiceRequest request, CancellationToken cancellationToken)
        {
            GetUserId();

            var distributorAccount = await _distributorAccountRepository.Table.FirstOrDefaultAsync(p => p.UserId == LoggedInUserId && p.Id == request.DistributorSapAccountId, cancellationToken) ?? throw new NotFoundException(ErrorMessages.SAP_ACCOUNT_NOTFOUND, ErrorCodes.SAP_ACCOUNT_NOTFOUND_CODE);
            (bool result, bool isStatementFound, string message) = await _sapService.RequestInvoiceList(new Shared.ExternalServices.ViewModels.Request.SAPInvoiceListRequest
            {
                CompanyCode = distributorAccount.CompanyCode,
                CountryCode = distributorAccount.CountryCode,
                DistributorNumber = distributorAccount.DistributorSapNumber,
                BillingDate = request.BillingDate.HasValue ? request.BillingDate.Value : DateTime.Now,
                AtcNumber = request.AtcNumber,
                BillingDocumentType = _config["BillingDocumentType:InvoiceList"]//_billingDocumentType.InvoiceList
            });

            if (result)
            {
                if (isStatementFound)
                    return ResponseHandler.SuccessResponse(SuccessMessages.SUCCESSFUL_INVOICE_LIST_REQUEST);
                else
                    return ResponseHandler.SuccessResponse(SuccessMessages.INVOICE_LIST_REQUEST_NOTFOUND, SuccessCodes.INVOICE_LIST_NOT_AVAILABLE);
            }
            else
                return ResponseHandler.FailureResponse(ErrorCodes.FAILED_SAP_REQUEST_CODE, ErrorMessages.INVOICE_LIST_REQUEST_FAILURE);
        }

        #region Private Methods
        #endregion
    }
}
