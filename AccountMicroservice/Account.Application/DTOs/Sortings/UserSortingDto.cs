
namespace Account.Application.DTOs.Sortings
{
    public class UserSortingDto
    {
        public bool IsNameAscending { get; set; } = false;
        public bool IsNameDescending { get; set; } = false;
        public bool IsDateAscending { get; set; } = false;
        public bool IsDateDescending { get; set; } = false;
    }
}
