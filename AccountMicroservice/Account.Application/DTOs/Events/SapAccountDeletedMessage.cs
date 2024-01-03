using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class SapAccountDeletedMessage : IntegrationBaseMessage
    {
        public string DistributorSapNumber { get; set; }
        public int SapAccountId { get; set; }
        public int UserId { get; set; }
        public DateTime DateDeleted { get; set; }
        
    }
}
