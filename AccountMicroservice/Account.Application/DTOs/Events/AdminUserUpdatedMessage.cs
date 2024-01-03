using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class AdminUserUpdatedMessage : IntegrationBaseMessage
    {
        public int UserId { get; set; }
        public string OldFirstName { get; set; }
        public string OldLastName { get; set; }
        public string OldEmailAddress { get; set; }
        public string OldUsername { get; set; }
        public int ModifiedByUserId { get; set; }
        public string OldAccountStatus { get; set; }
        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string NewEmailAddress { get; set; }
        public string NewUsername { get; set; }
        public string NewAccountStatus { get; set; }
        public string ChannelCode { get; set; }
        public DateTime DateModified { get; set; }




    }
}
