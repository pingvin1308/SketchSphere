using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public abstract class DrawingObject
{
    protected DrawingObject(double x, double y)
    {
        X = x;
        Y = y;
        _width = 1;
        _height = 1;
    }

    protected double X { get; }
    protected double Y { get; }

    private int _width;
    private int _height;

    protected int Width
    {
        get => _width;
        private set => _width = value;
    }

    protected int Height
    {
        get => _height;
        private set => _height = value;
    }

    public abstract Task DrawAsync(Context2D context);

    public void SetWidth(double x2)
    {
        Width = (int)(x2 - X);
    }

    public void SetHeight(double y2)
    {
        Height = (int)(y2 - Y);
    }
}