using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Freedraw : DrawingObject
{
    private List<(double X, double Y)> _points;
    
    public double MaxX => _points.Max(point => point.X);
    public double MinX => _points.Min(point => point.X);
    public double MaxY => _points.Max(point => point.Y);
    public double MinY => _points.Min(point => point.Y);

    public Freedraw(double x, double y) : base(x, y)
    {
        _points = new();
        _points.Add((x, y));
    }
    
    public List<(double X, double Y)> Points => _points;

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

    public override void SetEnd(double x, double y)
    {
        _points.Add((x, y));
        base.SetEnd(x, y);
    }

    public override bool IsHit(double x, double y)
    {
        return x >= MinX && x <= MaxX && y >= MinY && y <= MaxY;
    }

    public override void Move(double offsetX, double offsetY)
    {
        for (var i = 0; i < _points.Count; i++)
        {
            var point = _points[i];
            _points[i] = (point.X + offsetX, point.Y + offsetY);
        }

        base.Move(offsetX, offsetY);
    }
}