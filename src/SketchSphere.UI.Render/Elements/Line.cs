using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render.Elements;

public sealed class Line : DrawingObject
{
    public Line(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.BeginPathAsync();
        await context.MoveToAsync(Transform.X1, Transform.Y1);
        await context.LineToAsync(Transform.X2, Transform.Y2);
        await context.StrokeAsync();
        await context.ClosePathAsync();
    }

    /// <summary>
    /// https://en.m.wikipedia.org/wiki/Distance_from_a_point_to_a_line#Line%20defined%20by%20two%20points
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override bool IsHit(double x, double y)
    {
        var x1 = Transform.X1;
        var y1 = Transform.Y1;
        var x2 = Transform.X2;
        var y2 = Transform.Y2;

        var step1 = Math.Abs((x2 - x1) * (y1 - y) - (x1 - x) * (y2 - y1));
        var step2 = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        var result = step1 / step2;

        return result < 5;
    }
}