using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Rectangle : DrawingObject
{
    public Rectangle(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.StrokeStyleAsync("#ffffff");
        await context.StrokeRectAsync(X, Y, Width, Height);
    }
}