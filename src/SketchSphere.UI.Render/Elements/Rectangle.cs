using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render.Elements;

public sealed class Rectangle : DrawingObject
{
    public Rectangle(double x, double y) : base(x, y)
    {
    }
    
    public double X1 => Math.Min(Transform.X1, Transform.X2);
    public double Y1 => Math.Min(Transform.Y1, Transform.Y2);

    public double X2 => Math.Max(Transform.X1, Transform.X2);
    public double Y2 => Math.Max(Transform.Y1, Transform.Y2);

    
    public override async Task DrawAsync(Context2D context)
    {
        await context.StrokeRectAsync(X1, Y1, (X2 - X1), (Y2 - Y1));
    }

    public override bool IsHit(double x, double y)
    {
        return x >= X1 && x <= X2 && y >= Y1 && y <= Y2;
    }
}