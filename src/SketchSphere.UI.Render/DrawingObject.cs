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

    public double X { get; private set; }
    public double Y { get; private set; }

    private int _width;
    private int _height;

    public int Width
    {
        get => _width;
        private set => _width = value;
    }

    public int Height
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

    public virtual bool IsHit(double x, double y)
    {
        return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
    }
    
    public virtual void Move(double offsetX, double offsetY)
    {
        X += offsetX;
        Y += offsetY;
    }
}