using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Ellipse : DrawingObject
{
    public Ellipse(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.StrokeStyleAsync("#ffffff");
        await context.BeginPathAsync();

        var radiusX = Width / 2.0;
        var radiusY = Height / 2.0;
        await context.EllipseAsync(
            x: X + radiusX,
            y: Y + radiusY,
            radius_x: Math.Abs(radiusX),
            radius_y: Math.Abs(radiusY),
            rotation: 0,
            start_angle: 0,
            end_angle: 2 * Math.PI);
        await context.StrokeAsync();
    }
}