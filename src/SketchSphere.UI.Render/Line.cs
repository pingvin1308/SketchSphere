using System.Numerics;
using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public sealed class Line : DrawingObject
{
    public double X2 { get; private set; }
    public double Y2 { get; private set; }

    public Line(double x, double y) : base(x, y)
    {
    }

    public override async Task DrawAsync(Context2D context)
    {
        await context.BeginPathAsync();
        await context.MoveToAsync(X, Y);
        await context.LineToAsync(X2, Y2);
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
        var x1 = X;
        var y1 = Y;
        var x2 = X2;
        var y2 = Y2;

        var step1 = Math.Abs((x2 - x1) * (y1 - y) - (x1 - x) * (y2 - y1));
        var step2 = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        var result = step1 / step2;

        return result < 5;
    }

    public void SetEndAsync(double x, double y)
    {
        X2 = x;
        Y2 = y;
    }

    public override void Move(double offsetX, double offsetY)
    {
        X2 += offsetX;
        Y2 += offsetY;
        base.Move(offsetX, offsetY);
    }
}