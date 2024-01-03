using Account.Application.Enums;

namespace Account.Application.ViewModels.QueryFilters
{
    public class ExportUsersDataQueryFilter
    {
        public string? RoleName { get; set; }
        public string? SearchKeyword { get; set; }
        public string? UserStatusCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public UserSortingEnum Sort { get; set; }
        public ExportFileTypeEnum? Format { get; set; }
    }
}
