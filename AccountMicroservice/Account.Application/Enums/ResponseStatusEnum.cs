using System.ComponentModel;

namespace Account.Application.Enums
{
    public enum ResponseStatusEnum
    {
        [Description("Failed")]
        Failed = 1,

        [Description("Successful")]
        Successful = 2,
    }
}
