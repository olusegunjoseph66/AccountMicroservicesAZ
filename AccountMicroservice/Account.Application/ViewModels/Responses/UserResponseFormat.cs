namespace Account.Application.ViewModels.Responses
{
    //public record UserResponseFormat(int Id, string FirstName, string LastName, string EmailAddress, UserStatusResponse UserStatus, int NumOfSapAccounts, string UserRole, DateTime UserLoginDate);
    public record UserResponseFormat
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public UserStatusResponse UserStatus { get; set; }
        public int NumOfSapAccounts { get; set; }
        public string Roles { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
