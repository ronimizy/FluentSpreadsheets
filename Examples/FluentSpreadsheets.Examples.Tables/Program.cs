using System.Drawing;
using System.Globalization;
using ClosedXML.Excel;
using FluentSpreadsheets;
using FluentSpreadsheets.ClosedXML.Rendering;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Tables;
using static FluentSpreadsheets.ComponentFactory;

CartItem[] items =
{
    new CartItem("Water", 10, 10),
    new CartItem("Bread", 20, 10),
    new CartItem("Milk", 30, 10),
    new CartItem("Eggs", 40, 10),
};

var model = new CartTableModel(items);

var table = new CartTable();

var tableComponent = table.Render(model);

var workbook = new XLWorkbook();
var worksheet = workbook.AddWorksheet("Cart");

var renderer = new ClosedXmlComponentRenderer();
var renderCommand = new ClosedXmlRenderCommand(worksheet, tableComponent);

await renderer.RenderAsync(renderCommand);

workbook.SaveAs("cart.xlsx");

public readonly record struct CartItem(string Name, decimal Price, int Quantity);

public readonly record struct CartTableModel(IReadOnlyCollection<CartItem> Items);

public class CartTable : RowTable<CartTableModel>, ITableCustomizer
{
    protected override IEnumerable<IRowComponent> RenderRows(CartTableModel model)
    {
        yield return Row
        (
            Label("Product Name").WithColumnWidth(20),
            Label("Price"),
            Label("Quantity")
        );

        foreach (var item in model.Items)
        {
            yield return Row
            (
                Label(item.Name),
                Label(item.Price, CultureInfo.InvariantCulture),
                Label(item.Quantity)
            );
        }
    }

    public IComponentSource Customize(IComponent component)
    {
        return component
            .WithBottomBorder(BorderType.Thin, Color.Black)
            .WithTrailingBorder(BorderType.Thin, Color.Black);
    }
}