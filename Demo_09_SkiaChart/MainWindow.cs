using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Windows;

namespace Demo_09_SkiaChart;

public class MainWindow : Window
{
    private const int N = 1024;
    private double[] samples = new double[N];
    private const double Fs = 1024.0;
    private const double Ts = 1.0 / Fs;

    public MainWindow()
    {
        Title = "Demo 09 Skia Chart";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 400;
        Content = Build();

        GenerateChart();
    }

    private UIElement Build()
    {
        return GridX(
            children: [
                SKElementX(
                    configure: x => {
                        x.PaintSurface += OnPaintSurface;
                    }
                )
            ]
        );
    }

    private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        var info = e.Info;

        canvas.Clear(SKColors.White);

        using var paint = new SKPaint
        {
            Color = SKColors.BlueViolet,
            StrokeWidth = 2,
            IsAntialias = true,
            Style = SKPaintStyle.Stroke
        };

        float width = info.Width;
        float height = info.Height;

        float tMin = 0f;
        float tMax = (float)(N * Ts);  // 1 second

        float yMin = -1f;
        float yMax = 1f;

        SKPoint[] points = new SKPoint[N];

        for (int i = 0; i < N; i++)
        {
            float t = (float)(i * Ts);
            float y = (float)samples[i];

            float xPixel = (t - tMin) / (tMax - tMin) * width;
            float yPixel = height - ((y - yMin) / (yMax - yMin) * height);

            points[i] = new SKPoint(xPixel, yPixel);
        }

        canvas.DrawPoints(SKPointMode.Polygon, points, paint);
    }
        
    private void GenerateChart()
    {
        // Simple cosine, 4 Hz --> 4 cycles in 1 second
        const double f = 4.0;

        for (int i = 0; i < N; i++)
        {
            double t = i * Ts;
            double phase = 2.0 * Math.PI * f * t;

            samples[i] = Math.Cos(phase);
        }
    }
}
