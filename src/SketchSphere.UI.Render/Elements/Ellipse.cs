using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render.Elements;

public sealed class Ellipse : DrawingObject
{
    public double X1 => Math.Min(Transform.X1, Transform.X2);
    public double Y1 => Math.Min(Transform.Y1, Transform.Y2);

    public double X2 => Math.Max(Transform.X1, Transform.X2);
    public double Y2 => Math.Max(Transform.Y1, Transform.Y2);
    
    public Ellipse(double x, double y) : base(x, y)
    {
    }

    public override bool IsHit(double x, double y)
    {
        return x >= X1 && x <= X2 && y >= Y1 && y <= Y2;
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.StrokeStyleAsync("#ffffff");
        await context.BeginPathAsync();

        var radiusX = (X2 - X1) / 2.0;
        var radiusY = (Y2 - Y1) / 2.0;
        await context.EllipseAsync(
            x: X1 + radiusX,
            y: Y1 + radiusY,
            radius_x: Math.Abs(radiusX),
            radius_y: Math.Abs(radiusY),
            rotation: 0,
            start_angle: 0,
            end_angle: 2 * Math.PI);
        await context.StrokeAsync();
    }
}