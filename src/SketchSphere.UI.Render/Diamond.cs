using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Diamond : DrawingObject
{
    public double X1 => Math.Min(Transform.X1, Transform.X2);
    public double Y1 => Math.Min(Transform.Y1, Transform.Y2);

    public double X2 => Math.Max(Transform.X1, Transform.X2);
    public double Y2 => Math.Max(Transform.Y1, Transform.Y2);

    public Diamond(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.BeginPathAsync();

        await context.MoveToAsync(X1, Y1 + Transform.Height / 2.0);
        await context.LineToAsync(X1 + Transform.Width / 2.0, Y1);
        await context.LineToAsync(X2, Y1 + Transform.Height / 2.0);
        await context.LineToAsync(X1 + Transform.Width / 2.0, Y2);
        await context.LineToAsync(X1, Y1 + Transform.Height / 2.0);

        await context.StrokeAsync();
    }

    public override bool IsHit(double x, double y)
    {
        return x >= X1 && x <= X2 && y >= Y1 && y <= Y2;
    }
}