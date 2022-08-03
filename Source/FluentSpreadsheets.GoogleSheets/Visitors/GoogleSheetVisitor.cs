using FluentSpreadsheets.GoogleSheets.Extensions;
using FluentSpreadsheets.GoogleSheets.Models;
using FluentSpreadsheets.GoogleSheets.Tools;
using FluentSpreadsheets.Visitors;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Visitors;

public class GoogleSheetVisitor : ComponentVisitorBase
{
    private const string UpdateFieldsAll = "*";

    private readonly GoogleSheetEditor _editor;

    private readonly int _sheetId;
    private readonly string _sheetName;

    public GoogleSheetVisitor(GoogleSheetArguments creationArgs, Index index, Style style = default)
        : base(index, style)
    {
        (SheetsService sheetService, string spreadSheetId, _sheetId, string title) = creationArgs;

        _editor = new GoogleSheetEditor(sheetService, spreadSheetId);
        _sheetName = SheetNameProvider.GetSheetName(title);
    }

    protected override Task AdjustColumnsAsync(int from, int upTo, CancellationToken cancellationToken)
        => AdjustDimensionAsync(Dimension.Columns, from, upTo, cancellationToken);

    protected override Task AdjustRowsAsync(int from, int upTo, CancellationToken cancellationToken)
        => AdjustDimensionAsync(Dimension.Rows, from, upTo, cancellationToken);

    protected override async Task MergeRangeAsync(IndexRange range, CancellationToken cancellationToken)
    {
        var mergeCellsRequest = new Request
        {
            MergeCells = new MergeCellsRequest
            {
                Range = range.ToGridRange(_sheetId),
                MergeType = MergeType.All
            }
        };

        await _editor.ExecuteBatchUpdateAsync(mergeCellsRequest, cancellationToken);
    }

    protected override Task SetColumnWidthAsync(int from, int upTo, int width, CancellationToken cancellationToken)
        => SetDimensionSizeAsync(Dimension.Columns, from, upTo, width, cancellationToken);

    protected override Task SetRowHeightAsync(int from, int upTo, int height, CancellationToken cancellationToken)
        => SetDimensionSizeAsync(Dimension.Columns, from, upTo, height, cancellationToken);

    protected override async Task StyleRangeAsync(Style style, IndexRange range, CancellationToken cancellationToken)
    {
        var cellData = new CellData
        {
            UserEnteredFormat = new CellFormat
            {
                HorizontalAlignment = style.Alignment.Horizontal.ToGoogleSheetsAlignment(),
                VerticalAlignment = style.Alignment.Vertical.ToGoogleSheetsAlignment()
            }
        };

        var setAlignmentRequest = new Request
        {
            RepeatCell = new RepeatCellRequest
            {
                Cell = cellData,
                Range = range.ToGridRange(_sheetId),
                Fields = UpdateFieldsAll
            }
        };

        var updateBordersRequest = new Request
        {
            UpdateBorders = new UpdateBordersRequest
            {
                Top = style.Border.Top.ToGoogleSheetsBorder(),
                Bottom = style.Border.Bottom.ToGoogleSheetsBorder(),
                Left = style.Border.Leading.ToGoogleSheetsBorder(),
                Right = style.Border.Trailing.ToGoogleSheetsBorder(),
                Range = range.ToGridRange(_sheetId)
            }
        };

        var requests = new List<Request>
        {
            setAlignmentRequest,
            updateBordersRequest
        };

        await _editor.ExecuteBatchUpdateAsync(requests, cancellationToken);
    }

    protected override async Task WriteStringAsync(Index index, string value, CancellationToken cancellationToken)
    {
        var valueRange = new ValueRange
        {
            Values = new List<IList<object>>
            {
                new List<object> { value }
            },
            Range = index.ToGoogleSheetsIndex(_sheetName)
        };

        await _editor.UpdateValuesAsync(valueRange, cancellationToken);
    }

    private async Task AdjustDimensionAsync(
        Dimension dimension,
        int startIndex,
        int endIndex,
        CancellationToken cancellationToken)
    {
        var dimensionRange = GetDimensionRange(dimension, startIndex, endIndex);

        var adjustRowsRequest = new Request
        {
            AutoResizeDimensions = new AutoResizeDimensionsRequest
            {
                Dimensions = dimensionRange
            }
        };

        await _editor.ExecuteBatchUpdateAsync(adjustRowsRequest, cancellationToken);
    }

    private async Task SetDimensionSizeAsync(
        Dimension dimension,
        int startIndex,
        int endIndex,
        int pixelSize,
        CancellationToken cancellationToken)
    {
        var dimensionRange = GetDimensionRange(dimension, startIndex, endIndex);

        var dimensionProperties = new DimensionProperties
        {
            PixelSize = pixelSize
        };

        var setSizeRequest =  new Request
        {
            UpdateDimensionProperties = new UpdateDimensionPropertiesRequest
            {
                Properties = dimensionProperties,
                Range = dimensionRange,
                Fields = UpdateFieldsAll
            }
        };

        await _editor.ExecuteBatchUpdateAsync(setSizeRequest, cancellationToken);
    }

    private DimensionRange GetDimensionRange(Dimension dimension, int startIndex, int endIndex)
    {
        return new DimensionRange
        {
            Dimension = dimension,
            StartIndex = startIndex,
            EndIndex = endIndex,
            SheetId = _sheetId
        };
    }
}