﻿using FluentSpreadsheets.GoogleSheets.Models;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Factories;

public static class DimensionRangeFactory
{
    public static DimensionRange Create(Dimension dimension, int startIndex, int endIndex, int sheetId)
    {
        return new DimensionRange
        {
            Dimension = dimension,
            StartIndex = startIndex,
            EndIndex = endIndex,
            SheetId = sheetId
        };
    }
}