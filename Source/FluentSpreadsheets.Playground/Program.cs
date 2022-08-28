using System.Globalization;
using ClosedXML.Excel;
using FluentSpreadsheets;
using FluentSpreadsheets.ClosedXML.Rendering;
using FluentSpreadsheets.GoogleSheets.Factories;
using FluentSpreadsheets.GoogleSheets.Rendering;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Tables;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using static FluentSpreadsheets.ComponentFactory;
using Color = System.Drawing.Color;

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

var sheet = table.Render(sheetData);

// await RenderGoogleSheets();
await RenderXlsx();

async Task RenderGoogleSheets()
{
    const string jsonCredentials = "";
    var credential = GoogleCredential.FromJson(jsonCredentials);

    var initializer = new BaseClientService.Initializer
    {
        HttpClientInitializer = credential,
    };

    var service = new SheetsService(initializer);
    const string spreadsheetId = "";

    var renderer = new GoogleSheetComponentRenderer(service);

    const string title = "";

    var renderCommandFactory = new RenderCommandFactory(service);

    var renderCommand = await renderCommandFactory.CreateAsync(spreadsheetId, title, sheet);

    await renderer.RenderAsync(renderCommand);
}

async Task RenderXlsx()
{
    var workbook = new XLWorkbook();
    var worksheet = workbook.AddWorksheet("Student Progress");

    var renderer = new ClosedXmlComponentRenderer();
    var renderCommand = new ClosedXmlRenderCommand(worksheet, sheet);

    await renderer.RenderAsync(renderCommand);

    workbook.SaveAs("student-progress.xlsx");
}

public readonly record struct Student(string Name);

public readonly record struct Lab(int Id, string Name, double MinPoints, double MaxPoints);

public readonly record struct LabPoints(int LabId, double Points);

public readonly record struct StudentPoints(Student Student, IReadOnlyCollection<LabPoints> LabPoints);

public readonly record struct HeaderData(IReadOnlyCollection<Lab> Labs);

public readonly record struct StudentPointsSheetData(
    HeaderData HeaderData,
    IReadOnlyCollection<StudentPoints> StudentPoints);

public class StudentPointsRowTable : RowTable<StudentPointsSheetData>, ITableCustomizer
{
    protected override IEnumerable<IRowComponent> RenderRows(StudentPointsSheetData model)
    {
        yield return Row
        (
            Label("#"),
            Label("Student Name").WithColumnWidth(30),
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
                Label((i + 1).ToString()),
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

    public IComponent Customize(IComponent component)
    {
        return component
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }
}