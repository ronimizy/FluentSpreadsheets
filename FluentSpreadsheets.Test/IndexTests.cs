using FluentAssertions;
using Xunit;

namespace FluentSpreadsheets.Test;

public sealed class IndexTests
{
    [Theory]
    [InlineData("A1", 1, 1)]
    [InlineData("a1", 1, 1)]
    [InlineData("aa1", 1, 27)]
    [InlineData("AB1", 1, 28)]
    public void FromSpan_Should_ParseValidIndex(string value, int row, int column)
    {
        var index = Index.FromSpan(value);

        index.Row.Should().Be(row);
        index.Column.Should().Be(column);
    }

    [Theory]
    [InlineData("1a")]
    public void FromSpan_Should_FailOnInvalidIndex(string value)
    {
        void Parse() => Index.FromSpan(value);

        Assert.ThrowsAny<Exception>(Parse);
    }
    
    [Theory]
    [InlineData("A1", 1, 1)]
    [InlineData("AA1", 1, 27)]
    [InlineData("AB1", 1, 28)]
    public void ToString_Should_ProduceValidIndex(string value, int row, int column)
    {
        var index = new Index(row, column);

        index.ToString().Should().Be(value);
    }
}
