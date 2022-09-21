using FluentAssertions;
using FluentSpreadsheets.Test.Models;
using FluentSpreadsheets.Test.Tables;
using Xunit;

namespace FluentSpreadsheets.Test;


public class RowSheetTest
{
    [Fact]
    public void Render_Should_ProduceCorrectAmountOfComponents()
    {
        // Arrange
        var studentA = new Student("Aboba 1");
        var studentB = new Student("Aboba 2");
        var studentC = new Student("Aboba 3");

        var lab1 = new Lab(1, "Lab 1", 0, 10);
        var lab2 = new Lab(2, "Lab 2", 2, 15);

        var studentPoints1 = new StudentPoints(studentA, new[]
        {
            new LabPoints(1, 10),
        });

        var studentPoints2 = new StudentPoints(studentB, new[]
        {
            new LabPoints(1, 10),
            new LabPoints(2, 5),
        });

        var studentPoints3 = new StudentPoints(studentC, Array.Empty<LabPoints>());

        StudentPoints[] studentPoints =
        {
            studentPoints1,
            studentPoints2,
            studentPoints3,
        };

        Lab[] labs = { lab1, lab2 };

        var headerData = new HeaderData(labs);

        var table = new StudentPointsRowTable();
        var sheetData = new StudentPointsSheetData(headerData, studentPoints);

        // Act
        var sheet = table.Render(sheetData);

        // Assert
        var stack = sheet.Should().BeAssignableTo<IVStackComponent>().Subject;
        var headerComponent = stack.First();
        var headerStack = headerComponent.Should().BeAssignableTo<IHStackComponent>().Subject;

        headerStack.Should().HaveCount(4);
    }
}