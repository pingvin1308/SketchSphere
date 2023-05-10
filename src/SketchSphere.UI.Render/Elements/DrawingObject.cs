using Excubo.Blazor.Canvas.Contexts;
using SketchSphere.UI.Render.Elements.Components;

namespace SketchSphere.UI.Render.Elements;

public abstract class DrawingObject
{
    private readonly Guid _id = Guid.NewGuid();
    public Transform Transform { get; }

    public DrawingObject(double x, double y)
    {
        Transform = new Transform(x, y);
    }
  
    public abstract Task DrawAsync(Context2D context);
    public abstract bool IsHit(double x, double y);

    public virtual void SetEnd(double x, double y)
    {
        Transform.SetEnd(x, y);
    }

    public virtual void Move(double offsetX, double offsetY)
    {
        Transform.Move(offsetX, offsetY);
    }

    public override bool Equals(object? obj)
    {
        return obj is DrawingObject other && _id == other._id;
    }
    
    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}