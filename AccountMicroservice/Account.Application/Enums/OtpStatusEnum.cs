using System.ComponentModel;

namespace Account.Application.Enums
{
    public enum OtpStatusEnum
    {
        [Description("New")]
        New = 1,

        [Description("Validated")]
        Validated = 2,

        [Description("Invalidated")]
        Invalidated = 3
    }
}
