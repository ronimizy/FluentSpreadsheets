namespace FluentSpreadsheets;

public readonly record struct HeaderFooterData<THeaderData, TFooterData>(
    THeaderData HeaderData,
    TFooterData FooterData);