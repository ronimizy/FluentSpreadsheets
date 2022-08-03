﻿using System.Globalization;
using ClosedXML.Excel;
using FluentSpreadsheets;
using FluentSpreadsheets.ClosedXML.Rendering;
using FluentSpreadsheets.GoogleSheets.Rendering;
using FluentSpreadsheets.SheetBuilders;
using FluentSpreadsheets.SheetSegments;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
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

ISheetSegment<HeaderData, StudentPoints, HeaderData>[] segments =
{
    new NumberSegment(),
    new StudentNameSegment(),
    new LabPointsSegment(),
};

var sheetData = new SheetData<HeaderData, StudentPoints, HeaderData>(headerData, studentPoints, headerData);
var sheetBuilder = new SheetBuilder();

IComponent sheet = sheetBuilder.Build(segments, sheetData);

await RenderGoogleSheets();
//await RenderXlm();

async Task RenderGoogleSheets()
{
    const string jsonCredentials = "";
    var credential = GoogleCredential.FromJson(jsonCredentials);

    var initializer = new BaseClientService.Initializer
    {
        HttpClientInitializer = credential
    };

    var service = new SheetsService(initializer);
    const string spreadsheetId = "";

    var renderer = new GoogleSheetComponentRenderer(service, spreadsheetId);
    
    const string title = "";
    int id = await GetSheetId(service, spreadsheetId, title);

    var renderCommand = new GoogleSheetRenderCommand(id, title, sheet);

    await renderer.RenderAsync(renderCommand);
}

async Task<int> GetSheetId(SheetsService service, string spreadsheetId, string title)
{
    Spreadsheet spreadSheet = await service.Spreadsheets
        .Get(spreadsheetId)
        .ExecuteAsync();

    Sheet googleSheet = spreadSheet.Sheets.FirstOrDefault(s => s.Properties.Title == title)
                         ?? throw new Exception("Sheet does not exist");
    
    return googleSheet.Properties.SheetId!.Value;
}

async Task RenderXlm()
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

public class NumberSegment : SheetSegmentBase<HeaderData, StudentPoints, HeaderData>
{
    protected override IComponent BuildHeader(HeaderData data)
    {
        return Label("#")
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }

    protected override IComponent BuildRow(HeaderRowData<HeaderData, StudentPoints> data, int rowIndex)
    {
        return Label((rowIndex + 1).ToString())
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }
}

public class StudentNameSegment : SheetSegmentBase<HeaderData, StudentPoints, HeaderData>
{
    protected override IComponent BuildHeader(HeaderData data)
    {
        return Label("Student Name")
            .WithColumnWidth(30)
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }

    protected override IComponent BuildRow(HeaderRowData<HeaderData, StudentPoints> data, int rowIndex)
    {
        return Label(data.RowData.Student.Name)
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }
}

public class LabPointsSegment : PrototypeSheetSegmentBase<HeaderData, StudentPoints, HeaderData, Lab, LabPoints?>
{
    protected override IComponent BuildHeader(Lab data)
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

    protected override IComponent BuildRow(HeaderRowData<Lab, LabPoints?> data, int rowIndex)
    {
        LabPoints? points = data.RowData;

        var component = points is null
            ? Empty()
            : Label(points.Value.Points, CultureInfo.InvariantCulture);

        component = VStack
        (
            component,
            Label("Хуй :)")
        );

        component = HStack(component, Label("Peq").ScaledBy(3, Axis.Vertical));

        component = VStack
        (
            component,
            HStack(
                Label("LOW"),
                VStack
                (
                    Label("1"),
                    HStack
                    (
                        Label("2"),
                        VStack
                        (
                            Label("3"),
                            HStack
                            (
                                VStack
                                (
                                    Label("4"),
                                    HStack
                                    (
                                        Label("5.1").ScaledBy(3, Axis.Vertical),
                                        VStack
                                        (
                                            Label("6")
                                        )
                                    )
                                ),
                                VStack
                                (
                                    Label("5")
                                )
                            )
                        )
                    )
                )
            )
        );

        return component
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
            .WithTrailingBorder(BorderType.Thin, Color.Black)
            .WithBottomBorder(BorderType.Thin, Color.Black);
    }

    protected override IEnumerable<Lab> SelectHeaderData(HeaderData data)
        => data.Labs;

    protected override LabPoints? SelectRowData(HeaderRowData<Lab, StudentPoints> data)
    {
        var points = data.RowData.LabPoints.SingleOrDefault(x => x.LabId.Equals(data.HeaderData.Id));
        return points.Equals(default) ? null : points;
    }
}