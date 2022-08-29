namespace FluentSpreadsheets.TableScaling;

internal class RowTableScaler : Scaler
{
    private static readonly Lazy<RowTableScaler> LazyInstance = new Lazy<RowTableScaler>(() => new RowTableScaler());

    private RowTableScaler() { }

    public static RowTableScaler Instance => LazyInstance.Value;
    
    protected override void ValidateCustomization(IComponent component, IComponent customizedComponent)
    {
        if (component.Size.Width.Equals(customizedComponent.Size.Width))
            return;

        throw new InvalidCustomizationException();
    }

    protected override Axis GetAxis()
        => Axis.Horizontal;

    protected override IComponent MergeComponents(IEnumerable<IBaseComponent> components)
        => ComponentFactory.HStack(components);

    protected override int SelectDimension(IComponent component)
        => component.Size.Width;
}