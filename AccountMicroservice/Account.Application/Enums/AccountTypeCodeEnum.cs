using System.ComponentModel;

namespace Account.Application.Enums
{
    public enum AccountTypeCodeEnum
    {
        [Description("BG")]
        BankGuarantee = 1,

        [Description("CC")]
        CleanCreditCustomer = 2,

        [Description("CS")]
        CashCustomer = 3,

        [Description("BG")]
        BankGuaranteeCustomer = 4,
    }
}
