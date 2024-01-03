namespace Account.Application.Configurations
{
    public class OtpSettings
    {
        public byte MaximumRequiredRetries { get; set; }
        public byte RetryIntervalInMinutes { get; set; }
        public byte OtpCodeLength { get; set; }
        public byte OtpExpiryInMinutes { get; set; }
    }
}
