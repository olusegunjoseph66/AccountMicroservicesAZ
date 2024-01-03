namespace Account.Application.Configurations
{
    public class PasswordSettings
    {
        public string RegexPattern { get; set; }
        public short PasswordExpiryDays { get; set; }
        public short PasswordAttemptedTries { get; set; }
        public short RecycleLimit { get; set; }
    }
}
