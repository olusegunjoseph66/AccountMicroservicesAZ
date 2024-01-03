using Shared.ExternalServices.ViewModels.Response;

namespace Account.Application.DTOs.Features.Account
{
    public class AccountCacheDto
    {
        public AccountCacheDto(SapCustomerAccountDto sapAccount, int userId)
        {
            SapAccount = sapAccount;
            UserId = userId;
        }

        public SapCustomerAccountDto SapAccount { get; set; }
        public int UserId { get; set; }
    }
}
