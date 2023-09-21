# FluentSpreadsheets

### FluentSpreadsheets [![badge](https://img.shields.io/nuget/vpre/FluentSpreadsheets?style=flat-square)](https://www.nuget.org/packages/FluentSpreadsheets/)

### FluentSpreadsheets.ClosedXML [![badge](https://img.shields.io/nuget/vpre/FluentSpreadsheets.ClosedXML?style=flat-square)](https://www.nuget.org/packages/FluentSpreadsheets.ClosedXML/)

### FluentSpreadsheets.GoogleSheets [![badge](https://img.shields.io/nuget/vpre/FluentSpreadsheets.GoogleSheets?style=flat-square)](https://www.nuget.org/packages/FluentSpreadsheets.GoogleSheets/)

## Overview

### FluentSpreadsheets library consists of two APIs:

- Component API\
  An API that provides a set of `components`, that can be used for building static UI with sheet cells as building
  blocks,
  as well as base logic for drawing defined component composition on the sheet.
- Table API\
  An API that provides a set of abstractions to define `tables` by using `rows` built from `components`
  and `component sources`.

## Examples

- [Component usage](Examples/FluentSpreadsheets.Examples.Components)
- [Simple able usage](Examples/FluentSpreadsheets.Examples.Tables)
- [Complex table usage](Examples/FluentSpreadsheets.Examples.Students)

## Component API

The base unit of component API is `IComponent` interface, it provides a basic interface to interact with all components.

```csharp
public interface IComponent
{
    Size Size { get; }

    void Accept(IComponentVisitor visitor);
}
```

There are many derived interfaces from `IComponent` used to reflect on component type in `IComponentVisitor`.

All component implementations are internal, so to create an instance of a component you need to use a static
class `ComponentFactory`.

#### Hint

> You can import static members of `ComponentFactory` for cleaner code.
> ```csharp
> using static FluentSpreadsheets.ComponentFactory;
> ```

### Components

Use `.Label` to create a label component. You can pass a `string` to it, or use a generic overload which will
call `ToString`
on the passed object (`IFormattable` overload supported).

```csharp
Label("Hello, World!");
Label(2.2, CultureInfo.InvariantCulture);
```

### Containers

Use `.VStack` & `.HStack` to stack components vertically or horizontally, they will auto scale child components' width
and height respectively.

```csharp
VStack
(
    HStack
    (
        Label("Hello"),
        Label(",")
    ),
    Label("Stacks!")
)
```

The result will be something like this:\
![Stacks](Docs/Media/hello-stacks.png)

Stacks will automatically scale their children so they all will have an equal width/height and fill a rectangle.

### Styles

Use extension methods to style components. \
Styles are cascading! It means that styles applied to container will be inherited by its children (and overriden, if
needed). \

Cascading behaviour does not apply to styling of single component, if you will apply style A on a component, then
style B, the component will have a style equal to style B applied to style A.

```csharp
  VStack
  (
      HStack
      (
          Label("Hello").WithContentAlignment(HorizontalAlignment.Trailing),
          Label(",")
      ),
      Label("Styles!").WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Top)
  ).WithTrailingBorder(BorderType.Thin, Color.Black).WithBottomBorder(BorderType.Thin, Color.Black)
```

> Components are immutable, when you apply a style to a component, it will return a new component with the style applied,
> the object you called a method on will not be changed.

The result will be something like this:\
![Styles](Docs/Media/hello-styles.png)

#### Resizing
Size values are accepted as relative multipliers to default platform's sizes (column width/row height). \

### Output

Code above will only produce component composition stored as objects in memory. To render it on the sheet,
you need to use `IComponentRenderer<T>`.

#### Now supported:

##### Excel output via "ClosedXML" library. (You will need to reference a `FluentSpreadsheets.ClosedXML` NuGet package)
```csharp
var workbook = new XLWorkbook();
var worksheet = workbook.AddWorksheet("Sample");

var helloComponent =
    VStack
    (
        HStack
        (
            Label("Hello")
                .WithContentAlignment(HorizontalAlignment.Trailing)
                .WithTrailingBorder(BorderType.Thin, Color.Black),
            Label(",")
        ),
        Label("Styles!")
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Top)
            .WithTopBorder(BorderType.Thin, Color.Black)
            .WithRowHeight(1.7)
    ).WithBottomBorder(BorderType.Thin, Color.Black).WithTrailingBorder(BorderType.Thin, Color.Black);

var renderer = new ClosedXmlComponentRenderer();
var renderCommand = new ClosedXmlRenderCommand(worksheet, helloComponent);

await renderer.RenderAsync(renderCommand);

workbook.SaveAs("sample.xlsx");
```
##### Google Sheets output via "Google Sheets API v4" library. (You will need to referencea `FluentSpreadsheets.GoogleSheets` NuGet package)
```csharp
var credential = GoogleCredential.FromFile("credentials.json");

var initializer = new BaseClientService.Initializer
{
  HttpClientInitializer = credential
};

var service = new SheetsService(initializer);
var renderer = new GoogleSheetComponentRenderer(service);

var helloComponent =
    VStack
    (
        HStack
        (
            Label("Hello")
                .WithContentAlignment(HorizontalAlignment.Trailing)
                .WithTrailingBorder(BorderType.Thin, Color.Black),
            Label(",")
        ),
        Label("Styles!")
            .WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Top)
            .WithTopBorder(BorderType.Thin, Color.Black)
            .WithRowHeight(1.7)
    ).WithBottomBorder(BorderType.Thin, Color.Black).WithTrailingBorder(BorderType.Thin, Color.Black);

const string spreadsheetId = "SampleSpreadsheetId";
const string title = "SampleTitle";

var renderCommandFactory = new RenderCommandFactory(service);
var renderCommand = await renderCommandFactory.CreateAsync(spreadsheetId, title, helloComponent);

await renderer.RenderAsync(renderCommand);
```

## Table API

Table API is based on `ITable<T>` interface, where `T` is a type of model, that is used to render a table.

To define a table you need to create a class derived from `RowTable<T>` and
implement `IEnumerable<IRowComponent> RenderRows(T model)` method.

To customize rendered table override `Customize` method in your table class.

```csharp
public readonly record struct CartItem(string Name, decimal Price, int Quantity);

public readonly record struct CartTableModel(IReadOnlyCollection<CartItem> Items);

public class CartTable : RowTable<CartTableModel>, ITableCustomizer
{
    protected override IEnumerable<IRowComponent> RenderRows(CartTableModel model)
    {
        yield return Row
        (
            Label("Product Name").WithColumnWidth(1.7),
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

    public override IComponent Customize(IComponent component)
    {
        return component
            .WithBottomBorder(BorderType.Thin, Color.Black)
            .WithTrailingBorder(BorderType.Thin, Color.Black);
    }
}
```

Use `.Render` method on table instance to create a component from model.

```csharp
var items = new CartItem[]
{
    new CartItem("Water", 10, 10),
    new CartItem("Bread", 20, 10),
    new CartItem("Milk", 30, 10),
    new CartItem("Eggs", 40, 10),
};

var model = new CartTableModel(items);

var table = new CartTable();

var tableComponent = table.Render(model);
```

If you want to customize already scaled component group, you can call a `CustomizedWith` modifier on it. \
(ex: add a common header for a header group), you can see it's usage in a 
[student points table example](Examples/FluentSpreadsheets.Examples.Students/README.md)

```csharp
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
```

## Index and IndexRange labels

As composing components result in component stretching and resizing, there is no deterministic way of addressing 
components using hard coded indices. Library provides label API, labels are computed in rendering process,
so they contain indices and index ranges that are correct relative to stretched and resized components in sheet's grid.

You can get Index or IndexRange of certain component by accessing `Index` or `Range` property accordingly.

### Inline labels

You can apply `WithIndexLabel` or `WithIndexRangeLabel` modifer which will give you out a label as an `out` parameter.

As composing FluentSpreadsheets layouts done as continuous object creation rather then being part of delegate body, 
components share same markup context and labels can be used in any components after defining them as `out` parameter.

```csharp
HStack
(
    Label("Min"),
    Label("Max")
).WithIndexLabel(out var stackLabel)
```

### Label elevation
Even though defining inline labels is pretty handy, they are not applicable in cases, when you need to reference
some components in other **prior** to their definitions. This is when "label elevation" process comes in.

Label elevation is implemented via label proxies, which are created using `LabelProxy` static class 
and it's `Create` and `CreateForRange` methods. They create proxies for index labels and index range labels accordingly.

`WithIndexLabel` and `WithIndexRangeLabel` modifiers have overloads which accept proxies.

```csharp
var studentNameLabel = LabelProxy.CreateForRange();

Label("Student Name")
  .WithColumnWidth(1.7)
  .WithTextColor(Color.Red)
  .WithTextWrapping()
  .WithIndexRangeLabel(studentNameLabel)
```

To retrieve label from proxy, access it's `Label` property

```csharp
Label(_ => $"# - {studentNameLabel.Label.Range}")
```

Additionally, you can assign labels to proxies manually using `AssignLabel` method.

### Limitations

As labels are computed during rendering process, you cannot use them with components that eagerly compute it's content,
because at the time of creating the component, label value is unknown.

For example `Label(string)` method and string interpolation will result in `UnsetLabelException` being thrown.

```csharp
Label($"# - {studentNameLabel.Label.Range}")
```

Luckily, there are index aware overloads for these kind of components, which compute their value lazily, 
only when rendered onto a sheet.

```csharp
Label(_ => $"# - {studentNameLabel.Label.Range}")
```

### IndexLabel with stretched and resized components

When you apply index label on components that has size larger than (1, 1) after scaling, 
the top left index will be assigned to the index label