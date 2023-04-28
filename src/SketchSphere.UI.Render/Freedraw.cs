using Blazor.Extensions.Canvas.Canvas2D;

namespace SketchSphere.UI.Render;

public sealed class Freedraw : DrawingObject
{
    private List<(double X, double Y)> _points;

    public Freedraw(double x, double y) : base(x, y)
    {
        _points = new();
        _points.Add((x, y));
    }

    public override async Task DrawAsync(Canvas2DContext context)
    {
        if (_points.Count == 0)
        {
            return;
        }

        await context.MoveToAsync(_points[0].X, _points[0].Y);

        for (var i = 1; i < _points.Count; i++)
        {
            var point = _points[i];
            await context.LineToAsync(point.X, point.Y);
        }

        await context.StrokeAsync();
    }

    public async Task AddPointAsync(Canvas2DContext context, double x, double y)
    {
        if (_points.Count == 1)
        {
            await context.MoveToAsync(_points[0].X, _points[0].Y);
        }

        await context.LineToAsync(x, y);
        await context.StrokeAsync();

        _points.Add((x, y));
    }
}