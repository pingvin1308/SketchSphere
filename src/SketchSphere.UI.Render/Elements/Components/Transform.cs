namespace SketchSphere.UI.Render.Elements.Components;

public sealed class Transform
{
    private double _x1;
    private double _y1;

    private double _x2;
    private double _y2;
    
    public double X1 => _x1;
    public double Y1 => _y1;

    public double X2 => _x2;
    public double Y2 => _y2;
    
    public double Width => Math.Abs(X2 - X1);
    public double Height => Math.Abs(Y2 - Y1);


    public Transform(double x, double y)
    {
        _x1 = x;
        _y1 = y;
    }

    public void SetEnd(double x, double y)
    {
        _x2 = x;
        _y2 = y;
    }
    
    public void SetStart(double x, double y)
    {
        _x1 = x;
        _y1 = y;
    }
    
    public void Move(double offsetX, double offsetY)
    {
        _x1 += offsetX;
        _y1 += offsetY;
        _x2 += offsetX;
        _y2 += offsetY;
    }
}