using ClosedXML.Excel;
using FluentAssertions;
using FluentSpreadsheets.ClosedXML.Rendering;
using Xunit;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.Test;

public class LabelTests
{
    [Fact]
    public async Task WithIndexLabel_ShouldSetCorrectIndex()
    {
        // Arrange
        var component = Label("aboba").WithIndexLabel(out var label);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("test");

        var renderer = new ClosedXmlComponentRenderer();

        // Act
        await renderer.RenderAsync(component, worksheet, default);

        // Assert
        label.Index.Should().BeEquivalentTo(new Index(1, 1));
    }

    [Fact]
    public async Task WithIndexLabel_ShouldSetTopLeftIndex_WhenComponentIsScaled()
    {
        // Arrange
        var component = Label("aboba")
            .ScaledBy(2, Axis.Horizontal)
            .WithIndexLabel(out var label);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("test");

        var renderer = new ClosedXmlComponentRenderer();

        // Act
        await renderer.RenderAsync(component, worksheet, default);

        // Assert
        label.Index.Should().BeEquivalentTo(new Index(1, 1));
    }

    [Fact]
    public async Task WithIndexRangeLabel_ShouldSetCorrectIndexRange()
    {
        // Arrange
        var component = Label("aboba")
            .ScaledBy(2, Axis.Horizontal)
            .WithIndexRangeLabel(out var label);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("test");

        var renderer = new ClosedXmlComponentRenderer();

        // Act
        await renderer.RenderAsync(component, worksheet, default);

        // Assert
        label.Range.Should().BeEquivalentTo(new IndexRange(new Index(1, 1), new Size(2, 1)));
    }
    
    [Fact]
    public async Task WithIndexRangeLabel_ShouldSetCorrectIndex_WhenComponentIsNotScaled()
    {
        // Arrange
        var component = Label("aboba")
            .WithIndexRangeLabel(out var label);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("test");

        var renderer = new ClosedXmlComponentRenderer();

        // Act
        await renderer.RenderAsync(component, worksheet, default);

        // Assert
        label.Range.Should().BeEquivalentTo(new IndexRange(new Index(1, 1), new Size(1, 1)));
    }
}