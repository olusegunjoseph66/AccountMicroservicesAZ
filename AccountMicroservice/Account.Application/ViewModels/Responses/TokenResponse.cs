namespace Account.Application.ViewModels.Responses
{
    public class TokenResponse
    {
        
        public string UserId { get; set; }
        
        public string RoleName { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
       
        public string AuthToken { get; set; }
        
        public int ExpiresIn { get; set; }
       
        public string RefreshToken { get; set; }
    }
}
