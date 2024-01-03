namespace Account.Application.DTOs.Features.Otp
{
    public record OtpDto(long Id, string Code, DateTime ExpiryTime, DateTime DateCreated, string PhoneNumber, string EmailAddress, string Reference, int countDownInSeconds);
}
