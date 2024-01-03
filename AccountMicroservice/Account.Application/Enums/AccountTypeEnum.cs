using System.ComponentModel;

namespace Account.Application.Enums
{
    public enum AccountTypeEnum
    {
        [Description("Bank Guarantee")]
        BankGuarantee = 1,

        [Description("Clean Credit Customer")]
        CleanCreditCustomer = 2,

        [Description("Cash Customer")]
        CashCustomer = 3,

        [Description("BG Customer")]
        BankGuaranteeCustomer = 4
    }
}
