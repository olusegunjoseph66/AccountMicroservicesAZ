using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.ExternalServices.Configurations;
using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.Enums;
using Shared.ExternalServices.Interfaces;
using Shared.ExternalServices.ViewModels.Request;
using Shared.ExternalServices.ViewModels.Response;

namespace Shared.ExternalServices.APIServices
{
    public class SapService : BaseService, ISapService
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly SapServiceUrls  _sapServiceUrls;
        private readonly ILogger<SapService> _sapServiceLogger;

        public SapService(IHttpClientFactory httpClientFactory, IOptions<SapServiceUrls> sapServiceUrls, ILogger<SapService> sapServiceLogger, IConfiguration _config) : base(httpClientFactory, _config)
        {
            _httpClientFactory = httpClientFactory;
            _sapServiceUrls = sapServiceUrls.Value;
            _sapServiceLogger = sapServiceLogger;
        }

        public async Task<SAPCustomerResponse> FindCustomer(string companyCode, string countryCode, string distributorNumber)
        {
            var endpoint = _config["SapServiceUrls:FindCustomerEndpoint"].Replace("{countryCode}", countryCode).Replace("{companyCode}", companyCode).Replace("{distributorNumber}", distributorNumber);

            var result = await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{_config["SapServiceUrls:BaseUrl"]}/{endpoint}"
            });
            if (result != null && (result?.Status.ToLower() == "success") || (result?.Status.ToLower() == "successfull"))
                return JsonConvert.DeserializeObject<SAPCustomerResponse>(Convert.ToString(result.Data));
            else if (result is null)
                _sapServiceLogger.LogError("SAP Response: Exception- API Response is null");
            else
            {
                var serializedResponse = JsonConvert.SerializeObject(result);
                _sapServiceLogger.LogError($"SAP Response: Exception- {serializedResponse}");
            }

            return null;
        }

        public async Task<(bool,bool)> RequestStatement(SAPStatementRequest request)
        {
            try
            {
                var endpoint = _config["SapServiceUrls:RequestStatementEndpoint"];
                _sapServiceLogger.LogError($"The request to SAP on Customer Statement. Endpoint is {endpoint}");
                var rep = request.FromDate.ToShortDateString();
                var result = await this.SendAsync<ResponseDto>(new ApiRequest()
                {
                    ApiType = ApiTypeEnum.POST,
                    Url = $"{_config["SapServiceUrls:BaseUrl"]}/{endpoint}",
                    Data = new SAPStatementDto
                    {
                        CompanyCode = request.CompanyCode,
                        CountryCode = request.CountryCode,
                        DistributorNumber = request.DistributorNumber,
                        FromDate = request.FromDate.ToString("yyyy-MM-dd"),
                        ToDate = request.ToDate.ToString("yyyy-MM-dd")
                    }
                });

                if (result is not null)
                {
                    var responseString = JsonConvert.SerializeObject(result);
                    _sapServiceLogger.LogInformation($"Info: The request to SAP on Customer Statement. result  is {responseString}");

                    switch (result.StatusCode)
                    {
                        case "00":
                            return (true, true);
                        case "01X":
                            return (true, false);
                        case "02X":
                            _sapServiceLogger.LogCritical($"ALERT: SAP is currently unreachable.");
                            return (false, false);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is TimeoutException)
                    _sapServiceLogger.LogError(ex, "SAP Customer Statement:Task was aborted due to inability to reach the destination service.");
                else
                    _sapServiceLogger.LogError($"{ex.InnerException.Message}");
            }
            return (false,false);
        }

        public async Task<(bool, bool, string)> RequestInvoiceStatement(SAPInvoiceStatementRequest request)
        {
            try
            {
                var endpoint = _config["SapServiceUrls:RequestInvoiceStatement"];
                _sapServiceLogger.LogError($"The request to SAP on Invoice Statement. Endpoint is {endpoint}");

                var result = await this.SendAsync<ResponseDto>(new ApiRequest()
                {
                    ApiType = ApiTypeEnum.POST,
                    Url = $"{_config["SapServiceUrls:BaseUrl"]}/{endpoint}",
                    Data = new SAPInvoiceStatementDto
                    {
                        CompanyCode = request.CompanyCode,
                        CountryCode = request.CountryCode,
                        DistributorNumber = request.DistributorNumber,
                        AtcNumber = request.AtcNumber,
                        BillingDate = request.BillingDate.ToString("yyyy-MM-dd")
                    }

                });

                if (result is not null)
                {
                    var responseString = JsonConvert.SerializeObject(result);
                    _sapServiceLogger.LogInformation($"Info: The request to SAP on Invoice Statement. result  is {responseString}");

                    switch (result.StatusCode)
                    {
                        case "00":
                            return (true, true, result.Message);
                        case "01X":
                            return (true, false, result.Message);
                        case "02X":
                            _sapServiceLogger.LogCritical($"ALERT: SAP is currently unreachable.");
                            return (false, false, result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is TimeoutException)
                    _sapServiceLogger.LogError(ex, "SAP Invoice Statement:Task was aborted due to inability to reach the destination service.");
                else
                    _sapServiceLogger.LogError($"{ex.InnerException.Message}");
            }
            return (false, false, "Sorry, we are unable to complete your request at the moment. Please try again later.");
        }

        public async Task<(bool, bool, string)> RequestInvoiceList(SAPInvoiceListRequest request)
        {
            try
            {
                var endpoint = _config["SapServiceUrls:InvoiceListStatement"];
                _sapServiceLogger.LogError($"The request to SAP on Invoice Statement. Endpoint is {endpoint}");

                var result = await this.SendAsync<ResponseDto>(new ApiRequest()
                {
                    ApiType = ApiTypeEnum.POST,
                    Url = $"{_config["SapServiceUrls:BaseUrl"]}/{endpoint}",
                    Data = new SAPInvoiceListDto
                    {
                        AtcNumber = request.AtcNumber,
                        BillingDocumentType = request.BillingDocumentType,
                        CompanyCode = request.CompanyCode,
                        CountryCode = request.CountryCode,
                        DistributorNumber = request.DistributorNumber,
                        BillingDate = request.BillingDate.ToString("yyyy-MM-dd")
                    }
                });

                if(result is not null)
                {
                    var responseString = JsonConvert.SerializeObject(result);
                    _sapServiceLogger.LogInformation($"Info: The request to SAP on Invoice List. result  is {responseString}");

                    switch (result.StatusCode)
                    {
                        case "00":
                            return (true, true, result.Message);
                        case "01X":
                            return (true, false, result.Message);
                        case "02X":
                            _sapServiceLogger.LogCritical($"ALERT: SAP is currently unreachable.");
                            return (false, false, result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is TimeoutException)
                    _sapServiceLogger.LogError(ex, "SAP Invoice Statement:Task was aborted due to inability to reach the destination service.");
                else
                    _sapServiceLogger.LogError($"{ex.InnerException.Message}");
            }
            return (false, false, "Sorry, we are unable to complete your request at the moment. Please try again later.");
        }
    }
}
