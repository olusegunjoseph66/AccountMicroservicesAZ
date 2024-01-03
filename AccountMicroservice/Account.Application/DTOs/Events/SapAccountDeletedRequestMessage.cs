using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class SapAccountDeletedRequestMessage : IntegrationBaseMessage
    {
        public string Reason { get; set; }
        public int DeletionRequestId { get; set; }
        public int UserId { get; set; }
    }

}
