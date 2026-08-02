using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UserExtensions;

public static class Wpfx
{
    public static Grid GridX(Action<Grid>? configure = null, UIElement[]? children = null)
    {
        var grid = new Grid();

        var items = children ?? Array.Empty<UIElement>();

        for (int i = 0; i < items.Length; i++)
        {
            var child = items[i];

            if (child == null)
                continue;

            var hasExplicitRow = child.ReadLocalValue(Grid.RowProperty) != DependencyProperty.UnsetValue;

            var hasExplicitColumn = child.ReadLocalValue(Grid.ColumnProperty) != DependencyProperty.UnsetValue;

            if (!hasExplicitRow && !hasExplicitColumn)
            {
                Grid.SetRow(child, i);
            }

            grid.Children.Add(child);
        }

        configure?.Invoke(grid);

        return grid;
    }

    public static RowDefinition RowDefinitionX(GridUnitType type = GridUnitType.Star, double height = 1)
    {
        return new RowDefinition
        {
            Height = GridLengthX(type, height)
        };
    }

    public static ColumnDefinition ColumnDefinitionX(GridUnitType type = GridUnitType.Star, double width = 1)
    {
        return new ColumnDefinition
        {
            Width = GridLengthX(type, width)
        };
    }

    public static GridLength GridLengthX(GridUnitType type, double value)
    {
        return type switch
        {
            GridUnitType.Auto => GridLength.Auto,
            GridUnitType.Pixel => new GridLength(value, GridUnitType.Pixel),
            GridUnitType.Star => new GridLength(value, GridUnitType.Star),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }

    public static void AddRowDefinitionX(this Grid grid, GridUnitType type = GridUnitType.Star, double height = 1)
    {
        grid.RowDefinitions.Add(RowDefinitionX(type, height));
    }

    public static void AddColumnDefinitionX(this Grid grid, GridUnitType type = GridUnitType.Star, double width = 1)
    {
        grid.ColumnDefinitions.Add(ColumnDefinitionX(type, width));
    }

    public static StackPanel StackPanelX(Action<StackPanel>? configure = null, UIElement[]? children = null)
    {
        var stackPanel = new StackPanel();

        foreach (var child in children ?? Array.Empty<UIElement>())
        {
            stackPanel.Children.Add(child);
        }

        configure?.Invoke(stackPanel);

        return stackPanel;
    }

    public static Border BorderX(Action<Border>? configure = null, UIElement? child = null)
    {
        var border = new Border
        {
            Child = child
        };

        configure?.Invoke(border);
        return border;
    }

    public static Viewbox ViewboxX(Action<Viewbox>? configure = null, UIElement? child = null)
    {
        var viewbox = new Viewbox
        {
            Child = child
        };

        configure?.Invoke(viewbox);
        return viewbox;
    }

    private static T FrameworkElementX<T>(Action<T>? configure = null) where T : FrameworkElement, new()
    {
        var element = new T();
        configure?.Invoke(element);
        return element;
    }

    public static Button ButtonX(Action<Button>? configure = null) => FrameworkElementX(configure);

    public static TextBox TextBoxX(Action<TextBox>? configure = null) => FrameworkElementX(configure);

    public static TextBlock TextBlockX(Action<TextBlock>? configure = null) => FrameworkElementX(configure);

    public static CheckBox CheckBoxX(Action<CheckBox>? configure = null) => FrameworkElementX(configure);

    public static ComboBox ComboBoxX(Action<ComboBox>? configure = null) => FrameworkElementX(configure);

    public static Slider SliderX(Action<Slider>? configure = null) => FrameworkElementX(configure);

    public static Label LabelX(Action<Label>? configure = null) => FrameworkElementX(configure);

    public static Image ImageX(Action<Image>? configure = null) => FrameworkElementX(configure);

    public static Ellipse EllipseX(Action<Ellipse>? configure = null) => FrameworkElementX(configure);

    public static Rectangle RectangleX(Action<Rectangle>? configure = null) => FrameworkElementX(configure);

    public static Line LineX(Action<Line>? configure = null) => FrameworkElementX(configure);

    public static Polygon PolygonX(Action<Polygon>? configure = null) => FrameworkElementX(configure);

    public static Polyline PolylineX(Action<Polyline>? configure = null) => FrameworkElementX(configure);

    public static Path PathX(Action<Path>? configure = null) => FrameworkElementX(configure);

    public static ProgressBar ProgressBarX(Action<ProgressBar>? configure = null) => FrameworkElementX(configure);

    public static RadioButton RadioButtonX(Action<RadioButton>? configure = null) => FrameworkElementX(configure);

    public static ToggleButton ToggleButtonX(Action<ToggleButton>? configure = null) => FrameworkElementX(configure);

    public static RepeatButton RepeatButtonX(Action<RepeatButton>? configure = null) => FrameworkElementX(configure);

    public static PasswordBox PasswordBoxX(Action<PasswordBox>? configure = null) => FrameworkElementX(configure);

    public static RichTextBox RichTextBoxX(Action<RichTextBox>? configure = null) => FrameworkElementX(configure);

    public static DatePicker DatePickerX(Action<DatePicker>? configure = null) => FrameworkElementX(configure);

    public static Calendar CalendarX(Action<Calendar>? configure = null) => FrameworkElementX(configure);

    public static ListBox ListBoxX(Action<ListBox>? configure = null) => FrameworkElementX(configure);

    public static ListView ListViewX(Action<ListView>? configure = null) => FrameworkElementX(configure);

    public static TreeView TreeViewX(Action<TreeView>? configure = null) => FrameworkElementX(configure);

    public static TabControl TabControlX(Action<TabControl>? configure = null) => FrameworkElementX(configure);

    public static Menu MenuX(Action<Menu>? configure = null) => FrameworkElementX(configure);

    public static ToolBar ToolBarX(Action<ToolBar>? configure = null) => FrameworkElementX(configure);

    public static StatusBar StatusBarX(Action<StatusBar>? configure = null) => FrameworkElementX(configure);

    public static Separator SeparatorX(Action<Separator>? configure = null) => FrameworkElementX(configure);

    public static ScrollViewer ScrollViewerX(Action<ScrollViewer>? configure = null) => FrameworkElementX(configure);

    public static InkCanvas InkCanvasX(Action<InkCanvas>? configure = null) => FrameworkElementX(configure);

    public static MediaElement MediaElementX(Action<MediaElement>? configure = null) => FrameworkElementX(configure);
           
    public static GridSplitter GridSplitterX(Action<GridSplitter>? configure = null) => FrameworkElementX(configure);

    public static CornerRadius CornerRadiusX(double uniformRadius)
    {
        return new CornerRadius(uniformRadius);
    }

    public static CornerRadius CornerRadiusX(double topLeft, double topRight, double bottomRight, double bottomLeft)
    {
        return new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
    }

    public static Thickness ThicknessX(double length)
    {
        return new Thickness(length);
    }

    public static Thickness ThicknessX(double left, double top, double right, double bottom)
    {
        return new Thickness(left, top, right, bottom);
    }

    public static Point PointX(double x, double y)
    {
        return new Point(x, y);
    }

    public static ScaleTransform ScaleTransformX(double scaleX, double scaleY)
    {
        return new ScaleTransform(scaleX, scaleY);
    }
        
    public static Brush BrushFromStringX(string str)
    {
        Color color = (Color)ColorConverter.ConvertFromString(str);
        Brush brush = new SolidColorBrush(color);
        if (brush.CanFreeze) 
            brush.Freeze();
        return brush;
    }

    public static Brush BrushX(byte red, byte green, byte blue)
    {
        Color color = Color.FromRgb(red, green, blue);
        Brush brush = new SolidColorBrush(color);
        return brush;
    }

    public static string PathStringX<T, TValue>(Expression<Func<T, TValue>> expr)
    {
        if (expr == null) throw new ArgumentNullException(nameof(expr));

        var stack = new Stack<string>();
        var currentExpression = expr.Body;

        while (currentExpression is MemberExpression member)
        {
            stack.Push(member.Member.Name);
            currentExpression = member.Expression!;
        }

        if (stack.Count == 0)
            throw new ArgumentException("Expression must be a member access expression like 'x => x.Property.SubProperty'.", nameof(expr));

        return string.Join(".", stack);
    }

    public static PropertyPath PropertyPathX(string path)
    {
        return new PropertyPath(path);
    }

    public static Binding BindingX(string path)
    {
        var binding = new Binding(path);
        return binding;
    }

    public static Binding BindingX(Action<Binding>? configure = null)
    {
        var binding = new Binding();
        configure?.Invoke(binding);
        return binding;
    }

    public static MultiBinding MultiBindingX(Action<MultiBinding>? configure = null)
    {
        var multiBinding = new MultiBinding();
        configure?.Invoke(multiBinding);
        return multiBinding;
    }


    //------------------------------------------------------------
    // STYLE

    public static Style StyleX<T>(Style? basedOn = null, Setter[]? setters = null) where T : Control
    {
        var style = new Style(typeof(T), basedOn);

        if (setters != null)
        {
            foreach (var s in setters)
                style.Setters.Add(
                    s.TargetName == null
                        ? new Setter(s.Property, s.Value)
                        : new Setter(s.Property, s.Value, s.TargetName)
                );
        }

        return style;
    }
        
    public static Setter SetterX(DependencyProperty property, object value, string? targetName = null)
    {
        var setter = new Setter(property, value, targetName);

        return setter;
    }

    public static Trigger TriggerX(DependencyProperty property, object value, Setter[]? setters = null)
    {
        var trigger = new Trigger 
        { 
            Property = property,
            Value = value 
        };

        if (setters != null)
        {
            foreach (var s in setters)
                trigger.Setters.Add(
                    s.TargetName == null
                        ? new Setter(s.Property, s.Value)
                        : new Setter(s.Property, s.Value, s.TargetName)
                );
        }

        return trigger;
    }

    public static ControlTemplate ControlTemplateX<T>(FrameworkElementFactory visualTree, Trigger[]? triggers = null) where T : Control
    {
        var template = new ControlTemplate(typeof(T));
        template.VisualTree = visualTree;

        if (triggers != null)
        {
            foreach (var t in triggers)
                template.Triggers.Add(t);
        }

        return template;
    }

    // Use inside ControlTemplates
    public static TemplateBindingExtension TemplateBindingX(DependencyProperty property)
    {
        return new TemplateBindingExtension(property);
    }

    public static FrameworkElementFactory FrameworkElementFactoryX<T>(string? name = null, Setter[]? setters = null, FrameworkElementFactory[]? children = null) where T : FrameworkElement
    {
        var factory = new FrameworkElementFactory(typeof(T));

        if (name != null)
            factory.Name = name;

        if (setters != null)
        {
            foreach (var s in setters)
                factory.SetValue(s.Property, s.Value);
        }

        if (children != null)
        {
            foreach (var c in children)
                factory.AppendChild(c);
        }

        return factory;
    }
}
