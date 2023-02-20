using FluentAssertions;
using FluentSpreadsheets.GoogleSheets.Extensions;
using Xunit;

namespace FluentSpreadsheets.Test;

public class GoogleIndexRangeParserTest
{
    [Theory]
    [InlineData("A1:B2", 1, 1, 2, 2)]
    [InlineData("Sheet!A1:B2", 1, 1, 2, 2)]
    public void FromSpan_Should_ParseValidRange(string value, int startRow, int startColumn, int endRow, int endColumn)
    {
        var range = IndexRangeExtensions.FromSpan(value);

        range.Start.Row.Should().Be(startRow);
        range.Start.Column.Should().Be(startColumn);
        range.End.Row.Should().Be(endRow);
        range.End.Column.Should().Be(endColumn);
    }
}