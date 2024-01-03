/// <summary>
/// Summary description for Class1
/// </summary>
public class UserFilterDto
{
    public string SearchText { get; set; }
    public List<short> RoleIds { get; set; }
    public string UserStatusCode { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set;}
    public byte? CompanyId { get; set; }
    public string UserName { get; set; }    
}
