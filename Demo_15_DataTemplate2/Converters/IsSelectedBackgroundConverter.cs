using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Demo_15_DataTemplate2.Converters;

public class IsSelectedBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is true
            ? Brushes.Gold
            : Brushes.WhiteSmoke;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}