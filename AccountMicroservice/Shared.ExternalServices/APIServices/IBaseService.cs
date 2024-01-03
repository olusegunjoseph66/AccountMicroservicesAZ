using Shared.ExternalServices.DTOs;
using Shared.ExternalServices.ViewModels.Request;

namespace Shared.ExternalServices.APIServices
{
    public interface IBaseService
    {
        IHttpClientFactory httpClient { get; set; }
        ResponseDto response { get; set; }

        void Dispose();
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}