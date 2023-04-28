using Blazor.Extensions.Canvas.Canvas2D;

namespace SketchSphere.UI.Render;

public sealed class Rectangle : DrawingObject
{
    public Rectangle(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Canvas2DContext context)
    {
        await context.SetStrokeStyleAsync("#ffffff");
        await context.StrokeRectAsync(X, Y, Width, Height);
    }
}