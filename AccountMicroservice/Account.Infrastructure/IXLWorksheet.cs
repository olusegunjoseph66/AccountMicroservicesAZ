#region Assembly ClosedXML, Version=0.95.4.0, Culture=neutral, PublicKeyToken=null
// C:\Users\user\.nuget\packages\closedxml\0.95.4\lib\netstandard2.0\ClosedXML.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.Drawing;
using System.IO;
using ClosedXML.Excel.Drawings;

namespace ClosedXML.Excel
{
    public interface IXLWorksheet : IXLRangeBase, IXLAddressable, IXLProtectable<IXLSheetProtection, XLSheetProtectionElements>, IXLProtectable
    {
        //
        // Summary:
        //     Gets the workbook that contains this worksheet
        XLWorkbook Workbook { get; }

        //
        // Summary:
        //     Gets or sets the default column width for this worksheet.
        double ColumnWidth { get; set; }

        //
        // Summary:
        //     Gets or sets the default row height for this worksheet.
        double RowHeight { get; set; }

        //
        // Summary:
        //     Gets or sets the name (caption) of this worksheet.
        string Name { get; set; }

        //
        // Summary:
        //     Gets or sets the position of the sheet.
        //     When setting the Position all other sheets' positions are shifted accordingly.
        int Position { get; set; }

        //
        // Summary:
        //     Gets an object to manipulate the sheet's print options.
        IXLPageSetup PageSetup { get; }

        //
        // Summary:
        //     Gets an object to manipulate the Outline levels.
        IXLOutline Outline { get; }

        //
        // Summary:
        //     Gets an object to manage this worksheet's named ranges.
        IXLNamedRanges NamedRanges { get; }

        //
        // Summary:
        //     Gets an object to manage how the worksheet is going to displayed by Excel.
        IXLSheetView SheetView { get; }

        //
        // Summary:
        //     Gets an object to manage this worksheet's Excel tables
        IXLTables Tables { get; }

        IXLDataValidations DataValidations { get; }

        XLWorksheetVisibility Visibility { get; set; }

        IXLSortElements SortRows { get; }

        IXLSortElements SortColumns { get; }

        bool ShowFormulas { get; set; }

        bool ShowGridLines { get; set; }

        bool ShowOutlineSymbols { get; set; }

        bool ShowRowColHeaders { get; set; }

        bool ShowRuler { get; set; }

        bool ShowWhiteSpace { get; set; }

        bool ShowZeros { get; set; }

        XLColor TabColor { get; set; }

        bool TabSelected { get; set; }

        bool TabActive { get; set; }

        IXLPivotTables PivotTables { get; }

        bool RightToLeft { get; set; }

        IXLAutoFilter AutoFilter { get; }

        IXLRanges MergedRanges { get; }

        IXLConditionalFormats ConditionalFormats { get; }

        IXLSparklineGroups SparklineGroups { get; }

        IXLRanges SelectedRanges { get; }

        IXLCell ActiveCell { get; set; }

        string Author { get; set; }

        IXLPictures Pictures { get; }

        //
        // Summary:
        //     Gets the first row of the worksheet.
        IXLRow FirstRow();

        //
        // Summary:
        //     Gets the first row of the worksheet that contains a cell with a value.
        //     Formatted empty cells do not count.
        IXLRow FirstRowUsed();

        //
        // Summary:
        //     Gets the first row of the worksheet that contains a cell with a value.
        //
        // Parameters:
        //   includeFormats:
        //     If set to true formatted empty cells will count as used.
        [Obsolete("Use the overload with XLCellsUsedOptions")]
        IXLRow FirstRowUsed(bool includeFormats);

        IXLRow FirstRowUsed(XLCellsUsedOptions options);

        //
        // Summary:
        //     Gets the last row of the worksheet.
        IXLRow LastRow();

        //
        // Summary:
        //     Gets the last row of the worksheet that contains a cell with a value.
        IXLRow LastRowUsed();

        //
        // Summary:
        //     Gets the last row of the worksheet that contains a cell with a value.
        //
        // Parameters:
        //   includeFormats:
        //     If set to true formatted empty cells will count as used.
        [Obsolete("Use the overload with XLCellsUsedOptions")]
        IXLRow LastRowUsed(bool includeFormats);

        IXLRow LastRowUsed(XLCellsUsedOptions options);

        //
        // Summary:
        //     Gets the first column of the worksheet.
        IXLColumn FirstColumn();

        //
        // Summary:
        //     Gets the first column of the worksheet that contains a cell with a value.
        IXLColumn FirstColumnUsed();

        //
        // Summary:
        //     Gets the first column of the worksheet that contains a cell with a value.
        //
        // Parameters:
        //   includeFormats:
        //     If set to true formatted empty cells will count as used.
        [Obsolete("Use the overload with XLCellsUsedOptions")]
        IXLColumn FirstColumnUsed(bool includeFormats);

        IXLColumn FirstColumnUsed(XLCellsUsedOptions options);

        //
        // Summary:
        //     Gets the last column of the worksheet.
        IXLColumn LastColumn();

        //
        // Summary:
        //     Gets the last column of the worksheet that contains a cell with a value.
        IXLColumn LastColumnUsed();

        //
        // Summary:
        //     Gets the last column of the worksheet that contains a cell with a value.
        //
        // Parameters:
        //   includeFormats:
        //     If set to true formatted empty cells will count as used.
        [Obsolete("Use the overload with XLCellsUsedOptions")]
        IXLColumn LastColumnUsed(bool includeFormats);

        IXLColumn LastColumnUsed(XLCellsUsedOptions options);

        //
        // Summary:
        //     Gets a collection of all columns in this worksheet.
        IXLColumns Columns();

        //
        // Summary:
        //     Gets a collection of the specified columns in this worksheet, separated by commas.
        //     e.g. Columns("G:H"), Columns("10:11,13:14"), Columns("P:Q,S:T"), Columns("V")
        //
        // Parameters:
        //   columns:
        //     The columns to return.
        IXLColumns Columns(string columns);

        //
        // Summary:
        //     Gets a collection of the specified columns in this worksheet.
        //
        // Parameters:
        //   firstColumn:
        //     The first column to return.
        //
        //   lastColumn:
        //     The last column to return.
        IXLColumns Columns(string firstColumn, string lastColumn);

        //
        // Summary:
        //     Gets a collection of the specified columns in this worksheet.
        //
        // Parameters:
        //   firstColumn:
        //     The first column to return.
        //
        //   lastColumn:
        //     The last column to return.
        IXLColumns Columns(int firstColumn, int lastColumn);

        //
        // Summary:
        //     Gets a collection of all rows in this worksheet.
        IXLRows Rows();

        //
        // Summary:
        //     Gets a collection of the specified rows in this worksheet, separated by commas.
        //     e.g. Rows("4:5"), Rows("7:8,10:11"), Rows("13")
        //
        // Parameters:
        //   rows:
        //     The rows to return.
        IXLRows Rows(string rows);

        //
        // Summary:
        //     Gets a collection of the specified rows in this worksheet.
        //
        // Parameters:
        //   firstRow:
        //     The first row to return.
        //
        //   lastRow:
        //     The last row to return.
        IXLRows Rows(int firstRow, int lastRow);

        //
        // Summary:
        //     Gets the specified row of the worksheet.
        //
        // Parameters:
        //   row:
        //     The worksheet's row.
        IXLRow Row(int row);

        //
        // Summary:
        //     Gets the specified column of the worksheet.
        //
        // Parameters:
        //   column:
        //     The worksheet's column.
        IXLColumn Column(int column);

        //
        // Summary:
        //     Gets the specified column of the worksheet.
        //
        // Parameters:
        //   column:
        //     The worksheet's column.
        IXLColumn Column(string column);

        //
        // Summary:
        //     Gets the cell at the specified row and column.
        //
        // Parameters:
        //   row:
        //     The cell's row.
        //
        //   column:
        //     The cell's column.
        IXLCell Cell(int row, int column);

        //
        // Summary:
        //     Gets the cell at the specified address.
        //
        // Parameters:
        //   cellAddressInRange:
        //     The cell address in the worksheet.
        IXLCell Cell(string cellAddressInRange);

        //
        // Summary:
        //     Gets the cell at the specified row and column.
        //
        // Parameters:
        //   row:
        //     The cell's row.
        //
        //   column:
        //     The cell's column.
        IXLCell Cell(int row, string column);

        //
        // Summary:
        //     Gets the cell at the specified address.
        //
        // Parameters:
        //   cellAddressInRange:
        //     The cell address in the worksheet.
        IXLCell Cell(IXLAddress cellAddressInRange);

        //
        // Summary:
        //     Returns the specified range.
        //
        // Parameters:
        //   rangeAddress:
        //     The range boundaries.
        IXLRange Range(IXLRangeAddress rangeAddress);

        //
        // Summary:
        //     Returns the specified range.
        //
        // Parameters:
        //   rangeAddress:
        //     The range boundaries.
        IXLRange Range(string rangeAddress);

        //
        // Summary:
        //     Returns the specified range.
        //
        // Parameters:
        //   firstCell:
        //     The first cell in the range.
        //
        //   lastCell:
        //     The last cell in the range.
        IXLRange Range(IXLCell firstCell, IXLCell lastCell);

        //
        // Summary:
        //     Returns the specified range.
        //
        // Parameters:
        //   firstCellAddress:
        //     The first cell address in the worksheet.
        //
        //   lastCellAddress:
        //     The last cell address in the worksheet.
        IXLRange Range(string firstCellAddress, string lastCellAddress);

        //
        // Summary:
        //     Returns the specified range.
        //
        // Parameters:
        //   firstCellAddress:
        //     The first cell address in the worksheet.
        //
        //   lastCellAddress:
        //     The last cell address in the worksheet.
        IXLRange Range(IXLAddress firstCellAddress, IXLAddress lastCellAddress);

        //
        // Summary:
        //     Returns a collection of ranges, separated by commas.
        //
        // Parameters:
        //   ranges:
        //     The ranges to return.
        IXLRanges Ranges(string ranges);

        //
        // Summary:
        //     Returns the specified range.
        //
        // Parameters:
        //   firstCellRow:
        //     The first cell's row of the range to return.
        //
        //   firstCellColumn:
        //     The first cell's column of the range to return.
        //
        //   lastCellRow:
        //     The last cell's row of the range to return.
        //
        //   lastCellColumn:
        //     The last cell's column of the range to return.
        //
        // Returns:
        //     .
        IXLRange Range(int firstCellRow, int firstCellColumn, int lastCellRow, int lastCellColumn);

        //
        // Summary:
        //     Gets the number of rows in this worksheet.
        int RowCount();

        //
        // Summary:
        //     Gets the number of columns in this worksheet.
        int ColumnCount();

        //
        // Summary:
        //     Collapses all outlined rows.
        IXLWorksheet CollapseRows();

        //
        // Summary:
        //     Collapses all outlined columns.
        IXLWorksheet CollapseColumns();

        //
        // Summary:
        //     Expands all outlined rows.
        IXLWorksheet ExpandRows();

        //
        // Summary:
        //     Expands all outlined columns.
        IXLWorksheet ExpandColumns();

        //
        // Summary:
        //     Collapses the outlined rows of the specified level.
        //
        // Parameters:
        //   outlineLevel:
        //     The outline level.
        IXLWorksheet CollapseRows(int outlineLevel);

        //
        // Summary:
        //     Collapses the outlined columns of the specified level.
        //
        // Parameters:
        //   outlineLevel:
        //     The outline level.
        IXLWorksheet CollapseColumns(int outlineLevel);

        //
        // Summary:
        //     Expands the outlined rows of the specified level.
        //
        // Parameters:
        //   outlineLevel:
        //     The outline level.
        IXLWorksheet ExpandRows(int outlineLevel);

        //
        // Summary:
        //     Expands the outlined columns of the specified level.
        //
        // Parameters:
        //   outlineLevel:
        //     The outline level.
        IXLWorksheet ExpandColumns(int outlineLevel);

        //
        // Summary:
        //     Deletes this worksheet.
        void Delete();

        //
        // Summary:
        //     Gets the specified named range.
        //
        // Parameters:
        //   rangeName:
        //     Name of the range.
        IXLNamedRange NamedRange(string rangeName);

        //
        // Summary:
        //     Gets the Excel table of the given index
        //
        // Parameters:
        //   index:
        //     Index of the table to return
        IXLTable Table(int index);

        //
        // Summary:
        //     Gets the Excel table of the given name
        //
        // Parameters:
        //   name:
        //     Name of the table to return
        IXLTable Table(string name);

        //
        // Summary:
        //     Copies the
        //
        // Parameters:
        //   newSheetName:
        IXLWorksheet CopyTo(string newSheetName);

        IXLWorksheet CopyTo(string newSheetName, int position);

        IXLWorksheet CopyTo(XLWorkbook workbook, string newSheetName);

        IXLWorksheet CopyTo(XLWorkbook workbook, string newSheetName, int position);

        IXLRange RangeUsed();

        [Obsolete("Use the overload with XLCellsUsedOptions")]
        IXLRange RangeUsed(bool includeFormats);

        IXLRange RangeUsed(XLCellsUsedOptions options);

        IXLWorksheet Hide();

        IXLWorksheet Unhide();

        IXLRange Sort();

        IXLRange Sort(string columnsToSortBy, XLSortOrder sortOrder = XLSortOrder.Ascending, bool matchCase = false, bool ignoreBlanks = true);

        IXLRange Sort(int columnToSortBy, XLSortOrder sortOrder = XLSortOrder.Ascending, bool matchCase = false, bool ignoreBlanks = true);

        IXLRange SortLeftToRight(XLSortOrder sortOrder = XLSortOrder.Ascending, bool matchCase = false, bool ignoreBlanks = true);

        IXLWorksheet SetShowFormulas();

        IXLWorksheet SetShowFormulas(bool value);

        IXLWorksheet SetShowGridLines();

        IXLWorksheet SetShowGridLines(bool value);

        IXLWorksheet SetShowOutlineSymbols();

        IXLWorksheet SetShowOutlineSymbols(bool value);

        IXLWorksheet SetShowRowColHeaders();

        IXLWorksheet SetShowRowColHeaders(bool value);

        IXLWorksheet SetShowRuler();

        IXLWorksheet SetShowRuler(bool value);

        IXLWorksheet SetShowWhiteSpace();

        IXLWorksheet SetShowWhiteSpace(bool value);

        IXLWorksheet SetShowZeros();

        IXLWorksheet SetShowZeros(bool value);

        IXLWorksheet SetTabColor(XLColor color);

        IXLWorksheet SetTabSelected();

        IXLWorksheet SetTabSelected(bool value);

        IXLWorksheet SetTabActive();

        IXLWorksheet SetTabActive(bool value);

        IXLPivotTable PivotTable(string name);

        IXLWorksheet SetRightToLeft();

        IXLWorksheet SetRightToLeft(bool value);

        [Obsolete("Use the overload with XLCellsUsedOptions")]
        IXLRows RowsUsed(bool includeFormats, Func<IXLRow, bool> predicate = null);

        IXLRows RowsUsed(XLCellsUsedOptions options = XLCellsUsedOptions.AllContents, Func<IXLRow, bool> predicate = null);

        IXLRows RowsUsed(Func<IXLRow, bool> predicate);

        [Obsolete("Use the overload with XLCellsUsedOptions")]
        IXLColumns ColumnsUsed(bool includeFormats, Func<IXLColumn, bool> predicate = null);

        IXLColumns ColumnsUsed(XLCellsUsedOptions options = XLCellsUsedOptions.AllContents, Func<IXLColumn, bool> predicate = null);

        IXLColumns ColumnsUsed(Func<IXLColumn, bool> predicate);

        object Evaluate(string expression);

        //
        // Summary:
        //     Force recalculation of all cell formulas.
        void RecalculateAllFormulas();

        IXLPicture Picture(string pictureName);

        IXLPicture AddPicture(Stream stream);

        IXLPicture AddPicture(Stream stream, string name);

        IXLPicture AddPicture(Stream stream, XLPictureFormat format);

        IXLPicture AddPicture(Stream stream, XLPictureFormat format, string name);

        IXLPicture AddPicture(Bitmap bitmap);

        IXLPicture AddPicture(Bitmap bitmap, string name);

        IXLPicture AddPicture(string imageFile);

        IXLPicture AddPicture(string imageFile, string name);
    }
}
#if false // Decompilation log
'340' items in cache
------------------
Resolve: 'netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
WARN: Version mismatch. Expected: '2.0.0.0', Got: '2.1.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\netstandard.dll'
------------------
Resolve: 'System.Drawing.Common, Version=4.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Drawing.Common, Version=4.0.2.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
WARN: Version mismatch. Expected: '4.0.0.0', Got: '4.0.2.0'
Load from: 'C:\Users\user\.nuget\packages\system.drawing.common\4.7.0\ref\netcoreapp3.0\System.Drawing.Common.dll'
------------------
Resolve: 'DocumentFormat.OpenXml, Version=2.7.2.0, Culture=neutral, PublicKeyToken=8fb06cb64d019a17'
Found single assembly: 'DocumentFormat.OpenXml, Version=2.7.2.0, Culture=neutral, PublicKeyToken=8fb06cb64d019a17'
Load from: 'C:\Users\user\.nuget\packages\documentformat.openxml\2.7.2\lib\netstandard1.3\DocumentFormat.OpenXml.dll'
------------------
Resolve: 'ExcelNumberFormat, Version=1.0.10.0, Culture=neutral, PublicKeyToken=23c6f5d73be07eca'
Found single assembly: 'ExcelNumberFormat, Version=1.0.10.0, Culture=neutral, PublicKeyToken=23c6f5d73be07eca'
Load from: 'C:\Users\user\.nuget\packages\excelnumberformat\1.0.10\lib\netstandard2.0\ExcelNumberFormat.dll'
------------------
Resolve: 'System.IO.Packaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.Packaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\user\.nuget\packages\system.io.packaging\4.0.0\ref\netstandard1.3\System.IO.Packaging.dll'
------------------
Resolve: 'System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.dll'
------------------
Resolve: 'System.IO.MemoryMappedFiles, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.MemoryMappedFiles, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.MemoryMappedFiles.dll'
------------------
Resolve: 'System.IO.Pipes, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.Pipes, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.Pipes.dll'
------------------
Resolve: 'System.Diagnostics.Process, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Process, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Diagnostics.Process.dll'
------------------
Resolve: 'System.Security.Cryptography.X509Certificates, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.X509Certificates, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Security.Cryptography.X509Certificates.dll'
------------------
Resolve: 'System.Memory, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Memory, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Memory.dll'
------------------
Resolve: 'System.Collections, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Collections.dll'
------------------
Resolve: 'System.Collections.NonGeneric, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.NonGeneric, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Collections.NonGeneric.dll'
------------------
Resolve: 'System.Collections.Concurrent, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.Concurrent, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Collections.Concurrent.dll'
------------------
Resolve: 'System.ObjectModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ObjectModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.ObjectModel.dll'
------------------
Resolve: 'System.Collections.Specialized, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.Specialized, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Collections.Specialized.dll'
------------------
Resolve: 'System.ComponentModel.TypeConverter, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel.TypeConverter, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.ComponentModel.TypeConverter.dll'
------------------
Resolve: 'System.ComponentModel.EventBasedAsync, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel.EventBasedAsync, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.ComponentModel.EventBasedAsync.dll'
------------------
Resolve: 'System.ComponentModel.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.ComponentModel.Primitives.dll'
------------------
Resolve: 'System.ComponentModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.ComponentModel.dll'
------------------
Resolve: 'Microsoft.Win32.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'Microsoft.Win32.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\Microsoft.Win32.Primitives.dll'
------------------
Resolve: 'System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Console.dll'
------------------
Resolve: 'System.Data.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Data.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Data.Common.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Diagnostics.TraceSource, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.TraceSource, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Diagnostics.TraceSource.dll'
------------------
Resolve: 'System.Diagnostics.Contracts, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Contracts, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Diagnostics.Contracts.dll'
------------------
Resolve: 'System.Diagnostics.TextWriterTraceListener, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.TextWriterTraceListener, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Diagnostics.TextWriterTraceListener.dll'
------------------
Resolve: 'System.Diagnostics.FileVersionInfo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.FileVersionInfo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Diagnostics.FileVersionInfo.dll'
------------------
Resolve: 'System.Diagnostics.StackTrace, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.StackTrace, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Diagnostics.StackTrace.dll'
------------------
Resolve: 'System.Diagnostics.Tracing, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Tracing, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Diagnostics.Tracing.dll'
------------------
Resolve: 'System.Drawing.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Drawing.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Drawing.Primitives.dll'
------------------
Resolve: 'System.Linq.Expressions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq.Expressions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Linq.Expressions.dll'
------------------
Resolve: 'System.IO.Compression.Brotli, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.IO.Compression.Brotli, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.Compression.Brotli.dll'
------------------
Resolve: 'System.IO.Compression, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.IO.Compression, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.Compression.dll'
------------------
Resolve: 'System.IO.Compression.ZipFile, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.IO.Compression.ZipFile, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.Compression.ZipFile.dll'
------------------
Resolve: 'System.IO.FileSystem, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.FileSystem.dll'
------------------
Resolve: 'System.IO.FileSystem.DriveInfo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.DriveInfo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.FileSystem.DriveInfo.dll'
------------------
Resolve: 'System.IO.FileSystem.Watcher, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.FileSystem.Watcher, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.FileSystem.Watcher.dll'
------------------
Resolve: 'System.IO.IsolatedStorage, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.IO.IsolatedStorage, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.IO.IsolatedStorage.dll'
------------------
Resolve: 'System.Linq, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Linq.dll'
------------------
Resolve: 'System.Linq.Queryable, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq.Queryable, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Linq.Queryable.dll'
------------------
Resolve: 'System.Linq.Parallel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq.Parallel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Linq.Parallel.dll'
------------------
Resolve: 'System.Threading.Thread, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Thread, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Threading.Thread.dll'
------------------
Resolve: 'System.Net.Requests, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Requests, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.Requests.dll'
------------------
Resolve: 'System.Net.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.Primitives.dll'
------------------
Resolve: 'System.Net.HttpListener, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.HttpListener, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.HttpListener.dll'
------------------
Resolve: 'System.Net.ServicePoint, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.ServicePoint, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.ServicePoint.dll'
------------------
Resolve: 'System.Net.NameResolution, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.NameResolution, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.NameResolution.dll'
------------------
Resolve: 'System.Net.WebClient, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.WebClient, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.WebClient.dll'
------------------
Resolve: 'System.Net.Http, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Http, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.Http.dll'
------------------
Resolve: 'System.Net.WebHeaderCollection, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.WebHeaderCollection, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.WebHeaderCollection.dll'
------------------
Resolve: 'System.Net.WebProxy, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.WebProxy, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.WebProxy.dll'
------------------
Resolve: 'System.Net.Mail, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Net.Mail, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.Mail.dll'
------------------
Resolve: 'System.Net.NetworkInformation, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.NetworkInformation, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.NetworkInformation.dll'
------------------
Resolve: 'System.Net.Ping, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Ping, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.Ping.dll'
------------------
Resolve: 'System.Net.Security, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Security, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.Security.dll'
------------------
Resolve: 'System.Net.Sockets, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Sockets, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.Sockets.dll'
------------------
Resolve: 'System.Net.WebSockets.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.WebSockets.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.WebSockets.Client.dll'
------------------
Resolve: 'System.Net.WebSockets, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.WebSockets, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Net.WebSockets.dll'
------------------
Resolve: 'System.Runtime.Numerics, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Numerics, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.Numerics.dll'
------------------
Resolve: 'System.Numerics.Vectors, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Numerics.Vectors, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Numerics.Vectors.dll'
------------------
Resolve: 'System.Reflection.DispatchProxy, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.DispatchProxy, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Reflection.DispatchProxy.dll'
------------------
Resolve: 'System.Reflection.Emit, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Reflection.Emit.dll'
------------------
Resolve: 'System.Reflection.Emit.ILGeneration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.ILGeneration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Reflection.Emit.ILGeneration.dll'
------------------
Resolve: 'System.Reflection.Emit.Lightweight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Emit.Lightweight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Reflection.Emit.Lightweight.dll'
------------------
Resolve: 'System.Reflection.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Reflection.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Reflection.Primitives.dll'
------------------
Resolve: 'System.Resources.Writer, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Resources.Writer, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Resources.Writer.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.VisualC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.CompilerServices.VisualC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.CompilerServices.VisualC.dll'
------------------
Resolve: 'System.Runtime.InteropServices.RuntimeInformation, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices.RuntimeInformation, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.InteropServices.RuntimeInformation.dll'
------------------
Resolve: 'System.Runtime.Serialization.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.Serialization.Primitives.dll'
------------------
Resolve: 'System.Runtime.Serialization.Xml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Xml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.Serialization.Xml.dll'
------------------
Resolve: 'System.Runtime.Serialization.Json, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Json, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.Serialization.Json.dll'
------------------
Resolve: 'System.Runtime.Serialization.Formatters, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Formatters, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.Serialization.Formatters.dll'
------------------
Resolve: 'System.Security.Claims, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Claims, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Security.Claims.dll'
------------------
Resolve: 'System.Security.Cryptography.Algorithms, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Algorithms, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Security.Cryptography.Algorithms.dll'
------------------
Resolve: 'System.Security.Cryptography.Csp, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Csp, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Security.Cryptography.Csp.dll'
------------------
Resolve: 'System.Security.Cryptography.Encoding, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Encoding, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Security.Cryptography.Encoding.dll'
------------------
Resolve: 'System.Security.Cryptography.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Security.Cryptography.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Security.Cryptography.Primitives.dll'
------------------
Resolve: 'System.Text.Encoding.Extensions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.Encoding.Extensions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Text.Encoding.Extensions.dll'
------------------
Resolve: 'System.Text.RegularExpressions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.RegularExpressions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Text.RegularExpressions.dll'
------------------
Resolve: 'System.Threading, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Threading.dll'
------------------
Resolve: 'System.Threading.Overlapped, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Overlapped, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Threading.Overlapped.dll'
------------------
Resolve: 'System.Threading.ThreadPool, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.ThreadPool, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Threading.ThreadPool.dll'
------------------
Resolve: 'System.Threading.Tasks.Parallel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Tasks.Parallel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Threading.Tasks.Parallel.dll'
------------------
Resolve: 'System.Transactions.Local, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Transactions.Local, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Transactions.Local.dll'
------------------
Resolve: 'System.Web.HttpUtility, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Web.HttpUtility, Version=5.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Web.HttpUtility.dll'
------------------
Resolve: 'System.Xml.ReaderWriter, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.ReaderWriter, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Xml.ReaderWriter.dll'
------------------
Resolve: 'System.Xml.XDocument, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XDocument, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Xml.XDocument.dll'
------------------
Resolve: 'System.Xml.XmlSerializer, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XmlSerializer, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Xml.XmlSerializer.dll'
------------------
Resolve: 'System.Xml.XPath.XDocument, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XPath.XDocument, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Xml.XPath.XDocument.dll'
------------------
Resolve: 'System.Xml.XPath, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XPath, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Xml.XPath.dll'
------------------
Resolve: 'System.Drawing.Primitives, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Drawing.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '4.2.0.0', Got: '5.0.0.0'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Drawing.Primitives.dll'
#endif
