using SkiaSharp.Views.WPF;

namespace UserExtensions;

public static class WpfxAdditions
{   
    public static SKElement SKElementX(Action<SKElement>? configure = null)
    {
        var element = new SKElement();
        configure?.Invoke(element);
        return element;
    }       
}
