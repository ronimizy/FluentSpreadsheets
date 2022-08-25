using System.Drawing;
using System.Globalization;
using ClosedXML.Excel;
using FluentSpreadsheets;
using FluentSpreadsheets.ClosedXML.Rendering;
using FluentSpreadsheets.SheetBuilders;
using FluentSpreadsheets.Sheets;
using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Styles;
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

ISheetSegment[] segments =
{
    new NumberSegmentBuilder(),
    new StudentNameSegmentBuilder(),
    new LabPointsSegmentBuilder(),
};

var sheetBuilder = new SheetBuilder();
var sheet = sheetBuilder.Build(segments, headerData, studentPoints);

var workbook = new XLWorkbook();
var worksheet = workbook.AddWorksheet("Student Progress");

var renderer = new ClosedXmlComponentRenderer();
var renderCommand = new ClosedXmlRenderCommand(worksheet, sheet);

await renderer.RenderAsync(renderCommand);

workbook.SaveAs("student-progress.xlsx");

public readonly record struct Student(string Name);

public readonly record struct Lab(int Id, string Name, double MinPoints, double MaxPoints);

public readonly record struct LabPoints(int LabId, double Points);

public readonly record struct StudentPoints(Student Student, IReadOnlyCollection<LabPoints> LabPoints);

public readonly record struct HeaderData(IReadOnlyCollection<Lab> Labs);

public class NumberSegmentBuilder : HeaderRowSegmentBase<HeaderData, StudentPoints>
{
    public override IComponent BuildHeader(HeaderData data)
    {
        return Label("#")
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }

    public override IComponent BuildRow(StudentPoints data, int rowIndex)
    {
        return Label((rowIndex + 1).ToString())
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }
}

public class StudentNameSegmentBuilder : HeaderRowSegmentBase<HeaderData, StudentPoints>
{
    public override IComponent BuildHeader(HeaderData data)
    {
        return Label("Student Name")
            .WithColumnWidth(30)
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }

    public override IComponent BuildRow(StudentPoints data, int rowIndex)
    {
        return Label(data.Student.Name)
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }
}

public class LabPointsSegmentBuilder : PrototypeHeaderRowSegmentBase<HeaderData, Lab, HeaderRowData<Lab, StudentPoints>>,
    IPrototypeSegmentHeaderCustomizer<HeaderData>
{
    public override IComponent BuildHeader(Lab data)
    {
        return VStack
        (
            Label(data.Name)
                .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
                .WithBottomBorder(BorderType.Thin, Color.Black),
            HStack
            (
                Label("Min").WithContentAlignment(HorizontalAlignment.Center)
                    .WithBottomBorder(BorderType.Thin, Color.Black),
                Label("Max").WithContentAlignment(HorizontalAlignment.Center)
                    .WithBottomBorder(BorderType.Thin, Color.Black)
            ),
            HStack
            (
                Label(data.MinPoints.ToString(CultureInfo.InvariantCulture))
                    .WithContentAlignment(HorizontalAlignment.Center)
                    .WithBottomBorder(BorderType.Thin, Color.Black),
                Label(data.MaxPoints.ToString(CultureInfo.InvariantCulture))
                    .WithContentAlignment(HorizontalAlignment.Center)
                    .WithBottomBorder(BorderType.Thin, Color.Black)
            )
        ).WithTrailingBorder(BorderType.Thin, Color.Black);
    }

    public override IComponent BuildRow(HeaderRowData<Lab, StudentPoints> data, int rowIndex)
    {
        var labId = data.HeaderData.Id;
        LabPoints? points = data.RowData.LabPoints.SingleOrDefault(x => x.LabId.Equals(labId));

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

    public override IEnumerable<Lab> SelectHeaderData(HeaderData data)
        => data.Labs;

    public IComponent CustomizeHeader(IComponent component, HeaderData data)
    {
        return VStack
        (
            Label("Labs")
                .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
                .WithBottomBorder(BorderType.Thin, Color.Black)
                .WithTrailingBorder(BorderType.Thin, Color.Black),
            component
        );
    }
}