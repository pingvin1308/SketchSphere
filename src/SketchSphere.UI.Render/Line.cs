using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Line : DrawingObject
{
    private double _x2;
    private double _y2;

    public Line(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.BeginPathAsync();
        await context.MoveToAsync(X, Y);
        await context.LineToAsync(_x2, _y2);
        await context.StrokeAsync();
    }

    public void SetEndAsync(double x, double y)
    {
        _x2 = x;
        _y2 = y;
    }
}