namespace Account.Application.ViewModels.Requests
{
    public class CompleteRegistrationRequest
    {
        public int RegistrationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? ProfilePhoto { get; set; }
    }
}
