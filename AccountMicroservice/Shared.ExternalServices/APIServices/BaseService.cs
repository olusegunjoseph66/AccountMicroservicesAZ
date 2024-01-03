using Azure.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.Helpers;
using Shared.ExternalServices.ViewModels.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.APIServices
{
    public class BaseService
    {
        public ResponseDto response { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public readonly IConfiguration _config;

        public BaseService(IHttpClientFactory httpClient, IConfiguration _config)
        {
            this.httpClient = httpClient;
            this.response = new ResponseDto();
            this._config = _config;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                HttpRequestMessage request = new();

                if (apiRequest.ApiType == Enums.ApiTypeEnum.GET)
                    request = new HttpRequestMessage(HttpMethod.Get, apiRequest.Url);
                else if (apiRequest.ApiType == Enums.ApiTypeEnum.POST)
                {
                    request = new HttpRequestMessage(HttpMethod.Post, apiRequest.Url);
                    var jsonObject = JsonConvert.SerializeObject(apiRequest.Data);
                    var stringContent = new StringContent(jsonObject, UnicodeEncoding.UTF8, "application/json");
                    request.Content = stringContent;
                }

                var handler = new HttpRedirectHandler()
                {
                    InnerHandler = new HttpClientHandler()
                    {
                        AllowAutoRedirect = false
                    }
                };
                HttpClient client = new(handler);
                //request.Headers.Authorization = new BasicAuthenticationHeaderValue("DMS_D", "z95W!j5V39gQ");
                request.Headers.Authorization = new BasicAuthenticationHeaderValue(_config["SapSetting:UserName"], _config["SapSetting:Password"]);
                client.DefaultRequestHeaders.Clear();
               
                var response = await client.SendAsync(request);
                var apiContent = await response.Content.ReadAsStringAsync();

                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    Message = "Error",
                    Data = new List<string> { Convert.ToString(e.Message) },
                    Status = "Failed"
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
