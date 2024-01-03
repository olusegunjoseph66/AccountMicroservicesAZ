namespace Account.Application.ViewModels.Requests
{
    public class RenameSapAccountRequest
    {
        public int SapAccountId { get; set; }       
        public string FriendlyName { get; set; }
    }
}
