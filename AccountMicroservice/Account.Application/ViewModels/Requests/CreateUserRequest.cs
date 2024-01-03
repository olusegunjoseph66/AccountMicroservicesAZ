namespace Account.Application.ViewModels.Requests
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string CompanyCode { get; set; }
        public List<string> RoleNames { get; set; } = new List<string>();
    }
}
