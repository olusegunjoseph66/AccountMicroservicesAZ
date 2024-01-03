using Shared.ExternalServices.DTOs;

namespace Shared.ExternalServices.Interfaces
{
    public interface IDmsService
    {
        Task<List<NameAndCodeDto>> GetCompanies();
        Task<List<NameAndCodeDto>> GetCountries();
    }
}