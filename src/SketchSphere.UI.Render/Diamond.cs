using Blazor.Extensions.Canvas.Canvas2D;

namespace SketchSphere.UI.Render;

public sealed class Diamond : DrawingObject
{
    public Diamond(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Canvas2DContext context)
    {
        await context.SetStrokeStyleAsync("#ffffff");
        await context.BeginPathAsync();
        await context.MoveToAsync(X - Width / 2, Y);
        await context.LineToAsync(X, Y - Height / 2);
        await context.LineToAsync(X + Width / 2, Y);
        await context.LineToAsync(X, Y + Height / 2);
        await context.LineToAsync(X - Width / 2, Y);
        await context.StrokeAsync();
    }
}