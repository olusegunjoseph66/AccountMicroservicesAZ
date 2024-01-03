namespace Account.Application.ViewModels.Requests
{
    public class CompleteResetPasswordRequest
    {
        public string ResetToken { get; set; }
        public string Password { get; set; }
    }
}
