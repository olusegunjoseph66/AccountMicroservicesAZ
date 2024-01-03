namespace Account.Application.DTOs.APIDataFormatters
{
    public class SwaggerResponse<T> where T : class
    {
        public T Data { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
