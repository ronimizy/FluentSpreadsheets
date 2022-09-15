using FluentSpreadsheets.GoogleSheets.Extensions;
using FluentSpreadsheets.GoogleSheets.Factories;
using FluentSpreadsheets.GoogleSheets.Models;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Handlers;

internal readonly struct GoogleSheetHandler : IComponentVisitorHandler
{
    private const string UpdateFieldsAll = "*";
    private const string UpdateAlignment = "userEnteredFormat(horizontalAlignment, verticalAlignment)";
    private const string UpdateBackgroundColor = "userEnteredFormat(backgroundColor)";

    private readonly int _id;
    private readonly string _name;

    public GoogleSheetHandler(int id, string title)
    {
        _id = id;
        _name = SheetNameFactory.Create(title);

        StyleRequests = new List<Request>();
        ValueRanges = new List<ValueRange>();
    }

    public IList<Request> StyleRequests { get; }
    public IList<ValueRange> ValueRanges { get; }

    public void AdjustColumns(int from, int upTo)
        => AdjustDimension(Dimension.Columns, from, upTo);

    public void AdjustRows(int from, int upTo)
        => AdjustDimension(Dimension.Rows, from, upTo);

    public void MergeRange(IndexRange range)
    {
        var mergeCellsRequest = new Request
        {
            MergeCells = new MergeCellsRequest
            {
                Range = range.ToGridRange(_id),
                MergeType = MergeType.All,
            },
        };

        StyleRequests.Add(mergeCellsRequest);
    }

    public void SetColumnWidth(int from, int upTo, int width)
        => SetDimensionSize(Dimension.Columns, from, upTo, width);

    public void FreezeRows(int count)
    {
        var properties = new SheetProperties
        {
            GridProperties = new GridProperties
            {
                FrozenRowCount = count,
            },
            SheetId = _id,
        };

        var request = new Request
        {
            UpdateSheetProperties = new UpdateSheetPropertiesRequest
            {
                Properties = properties,
                Fields = "gridProperties.frozenRowCount",
            },
        };

        StyleRequests.Add(request);
    }

    public void FreezeColumns(int count)
    {
        var properties = new SheetProperties
        {
            GridProperties = new GridProperties
            {
                FrozenColumnCount = count,
            },
            SheetId = _id,
        };

        var request = new Request
        {
            UpdateSheetProperties = new UpdateSheetPropertiesRequest
            {
                Properties = properties,
                Fields = "gridProperties.frozenColumnCount",
            },
        };

        StyleRequests.Add(request);
    }

    public void SetRowHeight(int from, int upTo, int height)
        => SetDimensionSize(Dimension.Columns, from, upTo, height);

    public void StyleRange(Style style, IndexRange range)
    {
        if (style.Alignment is not null)
        {
            var cellData = new CellData
            {
                UserEnteredFormat = new CellFormat
                {
                    HorizontalAlignment = style.Alignment.Value.Horizontal?.ToGoogleSheetsAlignment(),
                    VerticalAlignment = style.Alignment.Value.Vertical?.ToGoogleSheetsAlignment(),
                },
            };

            var request = new Request
            {
                RepeatCell = new RepeatCellRequest
                {
                    Cell = cellData,
                    Range = range.ToGridRange(_id),
                    Fields = UpdateAlignment,
                },
            };

            StyleRequests.Add(request);
        }

        if (style.Border is not null)
        {
            var request = new Request
            {
                UpdateBorders = new UpdateBordersRequest
                {
                    Top = style.Border.Value.Top?.ToGoogleSheetsBorder(),
                    Bottom = style.Border.Value.Bottom?.ToGoogleSheetsBorder(),
                    Left = style.Border.Value.Leading?.ToGoogleSheetsBorder(),
                    Right = style.Border.Value.Trailing?.ToGoogleSheetsBorder(),
                    Range = range.ToGridRange(_id),
                },
            };

            StyleRequests.Add(request);
        }

        if (style.Fill is not null)
        {
            var cellData = new CellData
            {
                UserEnteredFormat = new CellFormat
                {
                    BackgroundColor = style.Fill.Value.ToGoogleColor(),
                },
            };

            var request = new Request
            {
                RepeatCell = new RepeatCellRequest
                {
                    Cell = cellData,
                    Range = range.ToGridRange(_id),
                    Fields = UpdateBackgroundColor,
                },
            };

            StyleRequests.Add(request);
        }
    }

    public void WriteString(Index index, string value)
    {
        var valueRange = new ValueRange
        {
            Values = new List<IList<object>>
            {
                new List<object> { value },
            },
            Range = index.ToGoogleSheetsIndex(_name),
        };

        ValueRanges.Add(valueRange);
    }

    private void AdjustDimension(Dimension dimension, int startIndex, int endIndex)
    {
        var dimensionRange = DimensionRangeFactory.Create(dimension, startIndex, endIndex, _id);

        var adjustDimensionRequest = new Request
        {
            AutoResizeDimensions = new AutoResizeDimensionsRequest
            {
                Dimensions = dimensionRange,
            },
        };

        StyleRequests.Add(adjustDimensionRequest);
    }

    private void SetDimensionSize(Dimension dimension, int startIndex, int endIndex, int pixelSize)
    {
        var dimensionRange = DimensionRangeFactory.Create(dimension, startIndex, endIndex, _id);
        var dimensionProperties = new DimensionProperties { PixelSize = pixelSize };

        var setSizeRequest = new Request
        {
            UpdateDimensionProperties = new UpdateDimensionPropertiesRequest
            {
                Properties = dimensionProperties,
                Range = dimensionRange,
                Fields = UpdateFieldsAll,
            },
        };

        StyleRequests.Add(setSizeRequest);
    }
}