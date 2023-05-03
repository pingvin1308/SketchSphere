using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Diamond : DrawingObject
{
    public Diamond(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.BeginPathAsync();

        await context.MoveToAsync(X, Y + Height / 2.0);
        await context.LineToAsync(X + Width / 2.0, Y);
        await context.LineToAsync(X + Width, Y + Height / 2.0);
        await context.LineToAsync(X + Width / 2.0, Y + Height);
        await context.LineToAsync(X, Y + Height / 2.0);

        await context.StrokeAsync();
    }
}