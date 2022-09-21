using System.Globalization;
using FluentSpreadsheets.Tables;
using FluentSpreadsheets.Test.Models;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.Test.Tables;

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