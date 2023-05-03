using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Freedraw : DrawingObject
{
    private List<(double X, double Y)> _points;

    public Freedraw(double x, double y) : base(x, y)
    {
        _points = new();
        _points.Add((x, y));
    }

    public override async Task DrawAsync(Context2D context)
    {
        if (_points.Count == 0)
        {
            return;
        }
        await context.BeginPathAsync();

        await context.MoveToAsync(_points[0].X, _points[0].Y);

        for (var i = 1; i < _points.Count; i++)
        {
            var point = _points[i];
            await context.LineToAsync(point.X, point.Y);
        }

        await context.StrokeAsync();
    }

    public void AddPointAsync(double x, double y)
    {
        _points.Add((x, y));
    }
}