using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using SkiaSharp;
using System.Diagnostics;

namespace Practice;

public class GLCanvas : OpenGlControlBase
{
    private static readonly Stopwatch _stopwatch = new();
    private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private SKSurface? _surface;
    private GRContext? _contex;
    private GRBackendRenderTarget? _skiaRenderTarget;
    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        var isNull = _surface is null;

        if (_skiaRenderTarget is null || _contex is null || _surface is null
            || _skiaRenderTarget.Width != Bounds.Width
            || _skiaRenderTarget.Height != Bounds.Height)
        {
            _contex?.Dispose();
            _skiaRenderTarget?.Dispose();

            var gr = GRGlInterface.Create();
            _contex = GRContext.CreateGl(gr);
            _skiaRenderTarget = new GRBackendRenderTarget((int)Bounds.Width,
                                                (int)Bounds.Height,
                                                0,
                                                8,
                                                new GRGlFramebufferInfo((uint)fb, SKColorType.Rgba8888.ToGlSizedFormat()));

            _surface = SKSurface.Create(_contex, _skiaRenderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888);
        }
        _stopwatch.Restart();
        Renderer.RenderImage(_surface.Canvas, Bounds.Width, Bounds.Height);
        _stopwatch.Stop();

        _logger.Log(NLog.LogLevel.Info, $"{_stopwatch.ElapsedTicks}");
        _surface.Canvas.Flush();
        if (isNull)
        {
            ((Control)this).InvalidateVisual();
        }
    }

    private readonly GLDrawingOperation _operation = new();
    public override void Render(DrawingContext context)
    {
        _operation.Bounds = Bounds;
        _operation.Surface = _surface;
        context.Custom(_operation);
    }
}
