using System.ComponentModel;

namespace Account.Application.Enums
{
    public enum RegistrationStatusEnum
    {
        [Description("New")]
        New = 1,

        [Description("Completed")]
        Completed = 2
    }
}
