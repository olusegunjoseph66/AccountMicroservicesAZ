using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.ExternalServices.Configurations;
using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.Enums;
using Shared.ExternalServices.Interfaces;
using Shared.ExternalServices.ViewModels.Request;
using Shared.ExternalServices.ViewModels.Response;
using Shared.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.APIServices
{
#pragma warning disable CS8604 // Possible null reference argument.
    public class DmsService : BaseService, IDmsService
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly AppMicroservices _microServiceSetting;

        public DmsService(IHttpClientFactory httpClientFactory, IOptions<AppMicroservices> microServiceSetting, IConfiguration _config) : base(httpClientFactory, _config)
        {
            _httpClientFactory = httpClientFactory;
            _microServiceSetting = microServiceSetting.Value;
        }
        
        public async Task<List<NameAndCodeDto>> GetCompanies()
        {
            var result = await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{_config["AppMicroservices:RData:BaseUrl"]}/{_config["AppMicroservices:RData:GetCompaniesEndpoint"]}"
                //Url = $"{_microServiceSetting.RData.BaseUrl}/{_microServiceSetting.RData.GetCompaniesEndpoint}"
            });

            if (result != null && (result?.StatusCode == "00"))
            {

                var response = JsonConvert.DeserializeObject<CompanyListResponse>(result.Data.ToString());

                return response.Companies;
            }
            return new List<NameAndCodeDto>();
        }

        public async Task<List<NameAndCodeDto>> GetCountries()
        {
            var result = await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{_config["AppMicroservices:RData:BaseUrl"]}/{_config["AppMicroservices:RData:GetCountriesEndpoint"]}"
                //Url = $"{_microServiceSetting.RData.BaseUrl}/{_microServiceSetting.RData.GetCountriesEndpoint}"
            });

            if(string.IsNullOrWhiteSpace(result.Data.ToString()))
                return new List<NameAndCodeDto>();

            var resultData = JsonConvert.DeserializeObject<ResponseDto>(result.Data.ToString());
            if (resultData != null && (resultData?.StatusCode == "00"))
            {
                var response = JsonConvert.DeserializeObject<CountryListResponse>(resultData.Data.ToString());
                return response.Countries;
            }
            return new List<NameAndCodeDto>();
        }
    }
#pragma warning restore CS8604 // Possible null reference argument.
}
