using Blazor.Extensions.Canvas.Canvas2D;

namespace SketchSphere.UI.Render;

public abstract class DrawingObject
{
    protected DrawingObject(double x, double y)
    {
        X = x;
        Y = y;
    }

    protected double X { get; }
    protected double Y { get; }

    protected int Width => 50;
    protected int Height => 50;

    public abstract Task DrawAsync(Canvas2DContext context);
}