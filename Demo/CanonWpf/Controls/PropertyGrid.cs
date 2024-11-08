namespace CanonWpf.Controls;

public class PropertyGrid : DataGrid
{
    static PropertyGrid()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGrid), new FrameworkPropertyMetadata(typeof(PropertyGrid)));
    }

    public PropertyGrid()
    {
        this.Columns.Add(new DataGridTextColumn() { Header = "Name", Binding = new Binding("Id") { Converter = new EnumToNameConverter() } });
        this.Columns.Add(new DataGridTextColumn() { Header = "Description", Binding = new Binding("Id") { Converter = new EnumToDescriptionConverter() } });
        this.Columns.Add(new DataGridTextColumn() { Header = "Id", Binding = new Binding("Id") { Converter = new EnumToHexConverter() } });
        this.Columns.Add(new DataGridTextColumn() { Header = "Param", Binding = new Binding("Param") });
        this.Columns.Add(new DataGridTextColumn() { Header = "Type", Binding = new Binding("DataType") });
        this.Columns.Add(new DataGridTextColumn() { Header = "Value", Binding = new Binding("ValueString") });
    }
}
