using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using System.Diagnostics;

namespace Practice
{
    public class SkiaDrawingOperation : ICustomDrawOperation
    {
        private static readonly Stopwatch _stopwatch = new();
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public Rect Bounds { get; set; }

        public void Dispose()
        {
        }

        public bool Equals(ICustomDrawOperation? other) => false;

        public bool HitTest(Point p) => true;

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature is null)
            {
                return;
            }

            using var lease = leaseFeature.Lease();
            if (lease.SkCanvas is null)
            {
                return;
            }

            _stopwatch.Restart();
            Renderer.RenderImage(lease.SkCanvas, Bounds.Width, Bounds.Height);
            _stopwatch.Stop();
            _logger.Log(NLog.LogLevel.Debug, $"{_stopwatch.ElapsedMilliseconds}");
        }
    }
}
