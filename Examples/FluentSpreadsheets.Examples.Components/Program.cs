using System.Drawing;
using FluentSpreadsheets;
using FluentSpreadsheets.Styles;
using static FluentSpreadsheets.ComponentFactory;

var helloStacks = VStack
(
    HStack
    (
        Label("Hello"),
        Label(",")
    ),
    Label("Stacks!")
);

var helloStyles = VStack
(
    HStack
    (
        Label("Hello").WithContentAlignment(HorizontalAlignment.Trailing),
        Label(",")
    ),
    Label("Styles!").WithContentAlignment(HorizontalAlignment.Center, VerticalAlignment.Top)
).WithTrailingBorder(BorderType.Thin, Color.Black).WithBottomBorder(BorderType.Thin, Color.Black);