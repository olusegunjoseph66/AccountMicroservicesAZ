using System.ComponentModel;

namespace Account.Application.Enums
{
    public enum RoleStatusEnum
    {
        
        [Description("Distributor")]
        Distributor = 1,

        [Description("Super Administrator")]
        SuperAdministrator = 2,

        [Description("Administrator")]
        Administrator = 3
    }
}
