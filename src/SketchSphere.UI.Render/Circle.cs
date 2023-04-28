using Blazor.Extensions.Canvas.Canvas2D;

namespace SketchSphere.UI.Render;

public sealed class Circle : DrawingObject
{
    public Circle(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Canvas2DContext context)
    {
        await context.SetStrokeStyleAsync("#ffffff");
        await context.BeginPathAsync();
        await context.ArcAsync(X, Y, 25, 0, Math.PI * 2);
        await context.StrokeAsync();
    }
}