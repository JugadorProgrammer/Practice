using Avalonia.Controls;
using Avalonia.Media;

namespace Practice
{
    public class SkiaCanvas : Control
    {
        private readonly SkiaDrawingOperation _operation = new();
        public override void Render(DrawingContext context)
        {
            _operation.Bounds = Bounds;
            context.Custom(_operation);
        }
    }
}
