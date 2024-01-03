namespace Account.Application.ViewModels.Responses
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
