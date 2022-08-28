namespace FluentSpreadsheets.TableScaling;

internal class ColumnTableScaler : Scaler
{
    private static readonly Lazy<ColumnTableScaler> LazyInstance = new Lazy<ColumnTableScaler>(() => new ColumnTableScaler());

    private ColumnTableScaler() { }
    
    public static ColumnTableScaler Instance => LazyInstance.Value;

    protected override void ValidateCustomization(IComponent component, IComponent customizedComponent)
    {
        if (component.Size.Height.Equals(customizedComponent.Size.Height))
            return;
        
        throw new InvalidOperationException("Customized component height must be equal to the component height.");
    }

    protected override Axis GetAxis()
        => Axis.Vertical;

    protected override IComponent MergeComponents(IEnumerable<IBaseComponent> componentSources)
        => ComponentFactory.VStack(componentSources);

    protected override int SelectDimension(IComponent component)
        => component.Size.Height;
}