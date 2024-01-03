using Account.Application.Enums;


public class UserQueryFilter
{
    public string? RoleName { get; set; }
    public string? SearchKeyword { get; set; }
    public string? UserStatusCode { get; set; }
    public string? UserName { get; set; }
    public string? CompanyCode { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public UserSortingEnum Sort { get; set; }
}
