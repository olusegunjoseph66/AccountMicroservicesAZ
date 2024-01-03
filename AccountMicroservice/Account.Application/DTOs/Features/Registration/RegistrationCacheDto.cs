using Shared.ExternalServices.ViewModels.Response;

namespace Account.Application.DTOs.Features.Registration
{
    public class RegistrationCacheDto
    {
        public RegistrationCacheDto(SAPCustomerResponse sapAccount, int registrationId, bool isPrivacyPolicyAccepted)
        {
            SapAccount = sapAccount;
            RegistrationId = registrationId;
            IsPrivacyPolicyAccepted = isPrivacyPolicyAccepted;
        }

        public SAPCustomerResponse SapAccount { get; set; }
        public int RegistrationId { get; set; }
        public bool IsPrivacyPolicyAccepted { get; set; }
    }
}
