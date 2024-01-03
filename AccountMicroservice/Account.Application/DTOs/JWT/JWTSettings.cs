namespace Account.Application.DTOs.JWT
{
    public class JWTSettings 
    {
        public string SecretKey { get; set; }
        public double DurationInMinutes { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
