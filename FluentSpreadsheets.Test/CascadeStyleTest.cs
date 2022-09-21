using System.Drawing;
using FluentAssertions;
using FluentSpreadsheets.Tables;
using Xunit;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.Test;

public class CascadeStyleTest
{
    [Fact]
    public void Component_Should_HaveCorrectCascadeStyle()
    {
        var component = Label()
            .FilledWith(Color.Black)
            .FilledWith(Color.Blue);

        component.Style.Fill.Should().BeEquivalentTo(Color.Blue);
    }

    [Fact]
    public void Container_Should_HaveCorrectCascadeStyle()
    {
        var container = HStack
        (
            Label(),
            Label().FilledWith(Color.Black)
        ).FilledWith(Color.Blue).FilledWith(Color.Violet);

        IComponent[] children = container.Should().BeAssignableTo<IHStackComponent>().Subject.ToArray();

        container.Style.Fill.Should().BeEquivalentTo(Color.Violet);
        children[0].Style.Fill.Should().BeEquivalentTo(Color.Violet);
        children[1].Style.Fill.Should().BeEquivalentTo(Color.Black);
    }

    [Fact]
    public void Table_Should_RenderProperStyles()
    {
        var finalColor = Color.Green;

        IEnumerable<IRowComponent> Generate(Unit unit)
        {
            yield return Row
            (
                Label()
            );

            yield return Row
            (
                Label()
            ).FilledWith(Color.AliceBlue).FilledWith(finalColor);
        }

        ITable<Unit> table = Table<Unit>(Generate);

        var component = table.Render(Unit.Value);

        var stack = component.Should().BeAssignableTo<IVStackComponent>().Subject;
        var lastRow = stack.Last();
        var lastRowStack = lastRow.Should().BeAssignableTo<IHStackComponent>().Subject;

        lastRowStack.Single().Style.Fill.Should().BeEquivalentTo(finalColor);
    }

    [Fact]
    public void RowContainer_Should_RenderProperStyles()
    {
        var color = Color.Aquamarine;
        
        var row = Row
        (
            HStack
            (
                Label()
            ).FilledWith(color)
        ).FilledWith(Color.Azure);

        var stack = row.First().Should().BeAssignableTo<IHStackComponent>().Subject;
        var label = stack.Single();

        label.Style.Fill.Should().BeEquivalentTo(color);
    }
}