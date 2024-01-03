using Shared.ExternalServices.DTOs;

namespace Account.Application.DTOs.Events
{
    public class AdminUserDeletedMessage : IntegrationBaseMessage
    {

        public int UserId { get; set; }
        public DateTime DateDeleted { get; set; }
        public int DeletedByUserId { get; set; }

    }
}
