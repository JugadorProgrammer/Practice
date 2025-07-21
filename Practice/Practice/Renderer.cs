using SkiaSharp;
using System;

namespace Practice
{
    public static class Renderer
    {
        private static readonly Random _random = new();

        public static void RenderImage(SKCanvas canvas, double width, double height, int lines = 1_000)
        {
            canvas.Clear(SKColors.Navy);

            var path = new SKPath();

            for (int i = 0; i < lines; ++i)
            {
                var x0 = (float)(_random.NextDouble() * width);
                var y0 = (float)(_random.NextDouble() * width);

                var x1 = (float)(_random.NextDouble() * width);
                var y1 = (float)(_random.NextDouble() * width);

                var x2 = (float)(_random.NextDouble() * height);
                var y2 = (float)(_random.NextDouble() * height);

                path.CubicTo(x0, y0, x1, y1, x2, y2);
                if (_random.Next() % 2 == 0)
                {
                    path.Close();
                }
            }

            var r = (byte)(100 * _random.NextDouble());
            var g = (byte)(100 * _random.NextDouble());
            var b = (byte)(100 * _random.NextDouble());
            var a = (byte)(128 + 100 * _random.NextDouble());
            canvas.DrawPath(path, new()
            {
                Color = new SKColor(r, g, b, a),
                StrokeWidth = 1
            });
        }
    }
}
