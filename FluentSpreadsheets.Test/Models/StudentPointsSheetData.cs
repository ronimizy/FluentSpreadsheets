namespace FluentSpreadsheets.Test.Models;

public readonly record struct StudentPointsSheetData(
    HeaderData HeaderData,
    IReadOnlyCollection<StudentPoints> StudentPoints);