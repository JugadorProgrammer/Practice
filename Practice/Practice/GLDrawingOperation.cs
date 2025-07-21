using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace Practice
{
    public class GLDrawingOperation : ICustomDrawOperation
    {
        private readonly SKPaint _paint = new();

        public SKSurface? Surface { get; set; }

        public Rect Bounds { get; set; }

        public void Dispose() { }
        public bool HitTest(Point p) => true;
        public bool Equals(ICustomDrawOperation? other) => false;

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature is null)
            {
                return;
            }

            using var lease = leaseFeature.Lease();
            if (lease.SkCanvas is null || Surface is null)
            {
                return;
            }

            Surface.Draw(lease.SkCanvas, 0, 0, _paint);
        }
    }
}
