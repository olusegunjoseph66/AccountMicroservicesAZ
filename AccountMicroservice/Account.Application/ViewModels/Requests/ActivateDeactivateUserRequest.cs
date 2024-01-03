namespace Account.Application.ViewModels.Requests
{
    public class ActivateDeactivateUserRequest
    {
        public int UserId { get; set; }
        public bool Activate { get; set; }

    }
}
