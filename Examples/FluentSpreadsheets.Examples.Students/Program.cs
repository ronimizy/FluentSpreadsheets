using System.Drawing;
using System.Globalization;
using ClosedXML.Excel;
using FluentSpreadsheets;
using FluentSpreadsheets.ClosedXML.Extensions;
using FluentSpreadsheets.ClosedXML.Rendering;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Tables;
using Microsoft.Extensions.DependencyInjection;
using static FluentSpreadsheets.ComponentFactory;

var studentA = new Student("Student 1");
var studentB = new Student("Student 2");
var studentC = new Student("Student 3");

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

var sheet = table.Render(sheetData);

var workbook = new XLWorkbook();
var worksheet = workbook.AddWorksheet("Student Progress");

var collection = new ServiceCollection();

collection
    .AddFluentSpreadsheets()
    .AddClosedXml();

var provider = collection.BuildServiceProvider();
using var scope = provider.CreateScope();

var renderer = scope.ServiceProvider.GetRequiredService<IClosedXmlComponentRenderer>();

await renderer.RenderAsync(sheet, worksheet, default);

workbook.SaveAs("student-progress.xlsx");

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
            Label("Student Name").WithColumnWidth(1.7),
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
            )).CustomizedWith(x => VStack(Label("Labs"), x))
        );

        foreach (var (data, i) in model.StudentPoints.Select((p, i) => (p, i)))
        {
            yield return Row
            (
                Label(i + 1),
                Label(data.Student.Name),
                ForEach(model.HeaderData.Labs, lab => BuildLabPointsCell(lab, data.LabPoints))
            );
        }
    }

    private static IComponent BuildLabPointsCell(Lab lab, IEnumerable<LabPoints> labPoints)
    {
        var labId = lab.Id;
        LabPoints? points = labPoints.SingleOrDefault(x => x.LabId.Equals(labId));

        if (points.Equals(default))
            points = null;

        var component = points is null
            ? Empty()
            : Label(points.Value.Points, CultureInfo.InvariantCulture);

        return component
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }

    protected override IComponent Customize(IComponent component)
    {
        return component
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }
}