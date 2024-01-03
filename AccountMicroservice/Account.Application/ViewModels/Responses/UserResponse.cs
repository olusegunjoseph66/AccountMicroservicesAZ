using Account.Application.ViewModels.Responses;
using Shared.ExternalServices.DTOs;

public record UserResponse
    {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; }
    public UserStatusResponse UserStatus { get; set; }
    public NameAndCodeResponse Company { get; set; }
    public int NumOfSapAccounts { get; set; }
    public List<string> Roles { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public DateTime DateCreated { get; set; }

    public UserResponse(int id, string firstName, string lastName, string emailAddress, string userName, UserStatusResponse status, int numberOfSapAccounts, List<string> roles, DateTime dateCreated, DateTime? userLoginDate, string? companyCode, string? companyName)
    {
        UserId = id;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        UserName = userName;
        UserStatus = status;
        NumOfSapAccounts = numberOfSapAccounts;
        Roles = roles;
        LastLoginDate = userLoginDate;
        DateCreated = dateCreated;
        Company = (string.IsNullOrWhiteSpace(companyName) && string.IsNullOrWhiteSpace(companyCode)) ? null : new NameAndCodeResponse(companyName, companyCode);
    }
}