using FluentSpreadsheets.GoogleSheets.Extensions;
using FluentSpreadsheets.GoogleSheets.Factories;
using FluentSpreadsheets.GoogleSheets.Models;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Styles.Text;
using FluentSpreadsheets.Visitors;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Handlers;

internal readonly struct GoogleSheetHandler : IComponentVisitorHandler
{
    private const string UpdateFieldsAll = "*";
    private const string UpdateAlignment = "userEnteredFormat(horizontalAlignment, verticalAlignment)";
    private const string UpdateBackgroundColor = "userEnteredFormat(backgroundColor)";
    private const string UpdateTextStyle = "userEnteredFormat(textFormat(foregroundColor,bold,italic),wrapStrategy)";

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

        if (style.Text is not null)
        {
            var textStyle = style.Text.Value;

            var cellFormat = new CellFormat
            {
                TextFormat = new TextFormat
                {
                    ForegroundColor = textStyle.Color?.ToGoogleColor(),
                    Bold = textStyle.Kind?.HasFlag(TextKind.Bold),
                    Italic = textStyle.Kind?.HasFlag(TextKind.Italic),
                },
                WrapStrategy = textStyle.Wrapping.ToGoogleWrappingStrategy(),
            };

            var request = new Request
            {
                RepeatCell = new RepeatCellRequest
                {
                    Cell = new CellData
                    {
                        UserEnteredFormat = cellFormat,
                    },
                    Range = range.ToGridRange(_id),
                    Fields = UpdateTextStyle,
                },
            };

            StyleRequests.Add(request);
        }
    }

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

    public void WriteString(Index index, string value, bool hasFormula)
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

    public void WriteString<T>(Index index, T context, Func<T, string> valueFactory, bool hasFormula)
    {
        var value = valueFactory.Invoke(context);
        WriteString(index, value, hasFormula);
    }

    public void AdjustRows(int from, int upTo)
        => AdjustDimension(Dimension.Rows, from, upTo);

    public void AdjustColumns(int from, int upTo)
        => AdjustDimension(Dimension.Columns, from, upTo);

    public void SetRowHeight(int from, int upTo, RelativeSize height)
    {
        const int defaultHeight = 21;
        SetDimensionSize(Dimension.Rows, from, upTo, height.Value * defaultHeight);
    }

    public void SetColumnWidth(int from, int upTo, RelativeSize width)
    {
        const int defaultWidth = 120;
        SetDimensionSize(Dimension.Columns, from, upTo, width.Value * defaultWidth);
    }

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

    private void SetDimensionSize(Dimension dimension, int startIndex, int endIndex, double pixelSize)
    {
        var dimensionRange = DimensionRangeFactory.Create(dimension, startIndex, endIndex, _id);
        var dimensionProperties = new DimensionProperties { PixelSize = (int)pixelSize };

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