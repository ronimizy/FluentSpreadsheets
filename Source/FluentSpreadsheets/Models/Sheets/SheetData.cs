namespace FluentSpreadsheets;

public readonly record struct SheetData<THeaderData, TRowData, TFooterData>(
    THeaderData HeaderData,
    IReadOnlyCollection<TRowData> RowData,
    TFooterData FooterData);