using Blazor.Extensions.Canvas.Canvas2D;

namespace SketchSphere.UI.Render;

public sealed class Line : DrawingObject
{
    private double _x2;
    private double _y2;

    public Line(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Canvas2DContext context)
    {
        await context.BeginPathAsync();
        await context.MoveToAsync(X, Y);
        await context.LineToAsync(_x2, _y2);
        await context.StrokeAsync();
    }

    public async Task SetEndAsync(Canvas2DContext context, double x, double y)
    {
        _x2 = x;
        _y2 = y;
        await DrawAsync(context);
    }
}