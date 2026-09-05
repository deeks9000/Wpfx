using System.Globalization;
using System.Net.Cache;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Demo_15_DataTemplate2.Converters;

public class ImageCacheConverter : IValueConverter
{
    public static ImageCacheConverter Instance { get; } = new();

    private readonly Dictionary<string, BitmapImage> _cache = new();

    public event EventHandler<string>? DownloadCompleted;

    private ImageCacheConverter() { }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string url || string.IsNullOrWhiteSpace(url))
            return null!;

        if (_cache.TryGetValue(url, out var cached))
        {
            System.Diagnostics.Debug.WriteLine($"CACHE HIT  : {url}");
            return cached;
        }

        System.Diagnostics.Debug.WriteLine($"CACHE MISS ...fetching : {url}");

        var bitmap = new BitmapImage();

        bitmap.BeginInit();
        bitmap.UriSource = new Uri(url);
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);
        bitmap.EndInit();

        _cache[url] = bitmap;

        return bitmap;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}