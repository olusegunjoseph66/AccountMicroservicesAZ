using Account.Application.DTOs.Filters;
using Shared.Data.Extensions;

namespace Account.Infrastructure.QueryObjects
{
    public class DistributorUserQueryObject : QueryObject<Shared.Data.Models.DistributorSapAccount>
    {
        public DistributorUserQueryObject(DistributorUserFilterDto filter)
        {
            if (filter == null) return;

            if (!string.IsNullOrWhiteSpace(filter.CountryCode))
                And(u => u.CountryCode == filter.CountryCode);

            if (!string.IsNullOrWhiteSpace(filter.CompanyCode))
                And(u => u.CompanyCode == filter.CompanyCode);

            if (!string.IsNullOrWhiteSpace(filter.SearchKeyword))
            {
                And(u => u.DistributorSapNumber.Contains(filter.SearchKeyword));
            }
        }
    }
}
