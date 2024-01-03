using Shared.Data.Extensions;


namespace Account.Infrastructure.QueryObjects
{
    public class UserQueryObject : QueryObject<Shared.Data.Models.User>
    {
        public UserQueryObject(UserFilterDto filter)
        {
            if (filter == null)
                And(u => !u.IsDeleted);

            And(u => !u.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter.UserStatusCode))
                And(u => u.UserStatus.Code == filter.UserStatusCode);

            if (filter.RoleIds.Any())
                And(u => filter.RoleIds.Contains(u.UserRoles.FirstOrDefault().RoleId));

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                And(u => u.FirstName.ToLower().Contains(filter.SearchText.ToLower())
                  || u.LastName.ToLower().Contains(filter.SearchText.ToLower()) || u.UserName.ToLower().Contains(filter.SearchText.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.UserName))
                And(u => u.UserName.ToLower() == filter.UserName.ToLower());

            if (filter.FromDate.HasValue)
                And(u => u.DateCreated.Date >= filter.FromDate.Value.Date);

            if (filter.ToDate.HasValue)
                And(u => u.DateCreated.Date <= filter.ToDate.Value.Date);
        }
    }
}
