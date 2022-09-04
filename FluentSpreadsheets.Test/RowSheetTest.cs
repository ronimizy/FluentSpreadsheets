using System.Globalization;
using FluentAssertions;
using FluentSpreadsheets.Tables;

namespace FluentSpreadsheets.Test;

using static ComponentFactory;

public class RowSheetTest
{
    [Fact]
    public void Test1()
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
        var headerComponent = stack.Components.First();
        var headerStack = headerComponent.Should().BeAssignableTo<IHStackComponent>().Subject;

        headerStack.Components.Should().HaveCount(4);
    }

    public readonly record struct Student(string Name);

    public readonly record struct Lab(int Id, string Name, double MinPoints, double MaxPoints);

    public readonly record struct LabPoints(int LabId, double Points);

    public readonly record struct StudentPoints(Student Student, IReadOnlyCollection<LabPoints> LabPoints);

    public readonly record struct HeaderData(IReadOnlyCollection<Lab> Labs);

    public readonly record struct StudentPointsSheetData(
        HeaderData HeaderData,
        IReadOnlyCollection<StudentPoints> StudentPoints);

    public class StudentPointsRowTable : RowTable<StudentPointsSheetData>
    {
        protected override IEnumerable<IRowComponent> RenderRows(StudentPointsSheetData model)
        {
            yield return Row
            (
                Label("#"),
                Label("Student Name"),
                ForEach(model.HeaderData.Labs, headerData => VStack
                (
                    Label(headerData.Name),
                    HStack
                    (
                        Label("Min"),
                        Label("Max")
                    ),
                    HStack
                    (
                        Label(headerData.MinPoints, CultureInfo.InvariantCulture),
                        Label(headerData.MaxPoints, CultureInfo.InvariantCulture)
                    )
                )).CustomizedWith(x => VStack(Label("Labs"), x)),
                Label("Total")
            );
        }
    }
}