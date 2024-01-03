namespace Account.Application.DTOs.Features.Otp
{
    public class OtpDetailDto
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public string Reference { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public NameAndId<byte> OtpStatus { get; set; }
        public int? RegistrationId { get; set; }
        public int? UserId { get; set; }
        public short? NumberOfRetries { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpiry { get; set; }
    }
}
