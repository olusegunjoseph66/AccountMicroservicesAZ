using Account.Application.Enums;

namespace Account.Application.ViewModels.QueryFilters
{
    public class DistributorUserQueryFilter
    {
        public string? CountryCode { get; set; }
        public string? CompanyCode { get; set; }
        public string? SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public UserDateSortingEnum Sort { get; set; }
    }
}
