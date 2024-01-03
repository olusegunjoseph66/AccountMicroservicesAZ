using System.ComponentModel;

namespace Account.Application.Enums
{

    public enum ExportFileTypeEnum
    {
        [Description("pdf")]
        PdfFile = 1,

        [Description("xls")]
        Xls = 2,

        [Description("csv")]
        Csv = 3,

    }
}
