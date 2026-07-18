using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Demo_10_SkiaAnimation;

public class MainWindow : Window
{
    private float _posX, _posY;
    private float _speedX, _speedY;
    private float _squareSize = 20;
    private float _canvasWidth = 800;
    private float _canvasHeight = 500;
    private long _lastFrameTime;
    private Stopwatch _stopwatch = new Stopwatch();
    private SKPaint _paint = new SKPaint { Color = SKColors.White, IsAntialias = true };
    private SKElement? _skElement;

    public MainWindow()
    {
        Title = "Demo 10 Skia Animation";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = BuildStartup();

        InitializeAnimation();

        CompositionTarget.Rendering += OnRenderFrame;

        Loaded += async (s, e) => {
            await Task.Delay(1000);
            Content = Build();
        };
    }

    private UIElement BuildStartup()
    {
        return TextBlockX(
            configure: x => {
                x.HorizontalAlignment = HorizontalAlignment.Center;
                x.VerticalAlignment = VerticalAlignment.Center;                
                x.Text = "Initialize...";
            }
        );
    }

    private UIElement Build()
    {
        return GridX(
            children: [
                SKElementX(
                    configure: x => {
                        _skElement = x;
                        x.PaintSurface += OnPaintSurface;
                    }
                )
            ]
        );
    }

    private void InitializeAnimation()
    {
        _posX = Random.Shared.Next(100, 400);
        _posY = Random.Shared.Next(100, 300);

        _speedX = RandomSpeed(400);
        _speedY = RandomSpeed(400);

        // Ensure speed isn't zero to avoid a stuck square
        if (Math.Abs(_speedX) < 50) _speedX = 50 * Math.Sign(_speedX);
        if (Math.Abs(_speedY) < 50) _speedY = 50 * Math.Sign(_speedY);

        _stopwatch.Start();
        _lastFrameTime = _stopwatch.ElapsedMilliseconds;
    }

    private float RandomSpeed(float minSpeed)
    {
        float speed = (float)(Random.Shared.NextDouble() * (1000 - minSpeed) + minSpeed);

        return Random.Shared.Next(2) == 0 ? speed : -speed;
    }

    private void OnRenderFrame(object? sender, EventArgs e)
    {
        long currentTime = _stopwatch.ElapsedMilliseconds;
        float deltaTime = (currentTime - _lastFrameTime) / 1000f;
        _lastFrameTime = currentTime;

        _posX += _speedX * deltaTime;
        _posY += _speedY * deltaTime;

        if (_posX + _squareSize >= _canvasWidth)
        {
            _posX = _canvasWidth - _squareSize;
            _speedX = -Math.Abs(_speedX);
        }
        else if (_posX <= 0)
        {
            _posX = 0;
            _speedX = Math.Abs(_speedX);
        }

        if (_posY + _squareSize >= _canvasHeight)
        {
            _posY = _canvasHeight - _squareSize;
            _speedY = -Math.Abs(_speedY);
        }
        else if (_posY <= 0)
        {
            _posY = 0;
            _speedY = Math.Abs(_speedY);
        }

        _skElement?.InvalidateVisual();
    }

    private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        var info = e.Info;
        _canvasWidth = info.Width;
        _canvasHeight = info.Height;

        canvas.Clear(SKColors.CornflowerBlue);

        canvas.DrawRect(_posX, _posY, _squareSize, _squareSize, _paint);
    }
}
