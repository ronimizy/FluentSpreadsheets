namespace FluentSpreadsheets;

public readonly record struct HeaderRowData<THeaderData, TRowData>(THeaderData HeaderData, TRowData RowData);