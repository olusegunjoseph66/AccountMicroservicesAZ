using System.ComponentModel;

namespace Account.Application.Enums
{
    public enum UserSortingEnum
    {

        [Description("name_asc")]
        NameAscending = 1,

        [Description("name_desc")]
        NameDescending = 2,

        [Description("date_asc")]
        DateAscending = 3,

        [Description("date_desc")]
        DateDescending = 4,
    }
}
