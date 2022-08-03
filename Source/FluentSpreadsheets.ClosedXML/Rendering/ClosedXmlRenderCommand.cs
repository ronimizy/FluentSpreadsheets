using ClosedXML.Excel;
using FluentSpreadsheets.Rendering;

namespace FluentSpreadsheets.ClosedXML.Rendering;

public record struct ClosedXmlRenderCommand(IXLWorksheet Worksheet, IComponent Component) : IRenderCommand;