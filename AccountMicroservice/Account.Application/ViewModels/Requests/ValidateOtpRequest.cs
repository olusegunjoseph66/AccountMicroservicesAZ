namespace Account.Application.ViewModels.Requests
{
    public class ValidateOtpRequest
    {
        public string OtpDisplayId { get; set; }
        public string OtpCode { get; set; }
    }
}
