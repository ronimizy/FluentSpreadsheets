using FluentAssertions;
using FluentSpreadsheets.GoogleSheets.Extensions;
using Xunit;

namespace FluentSpreadsheets.Test;

public class GoogleIndexParserTest
{
    [Theory]
    [InlineData("A1", 1, 1)]
    [InlineData("a1", 1, 1)]
    [InlineData("aa1", 1, 27)]
    public void FromSpan_Should_ParseValidIndex(string value, int row, int column)
    {
        var index = IndexExtensions.FromSpan(value);

        index.Row.Should().Be(row);
        index.Column.Should().Be(column);
    }

    [Theory]
    [InlineData("1a")]
    public void FromSpan_Should_FailOnInvalidIndex(string value)
    {
        void Parse()
            => IndexExtensions.FromSpan(value);

        Assert.ThrowsAny<Exception>(Parse);
    }
}