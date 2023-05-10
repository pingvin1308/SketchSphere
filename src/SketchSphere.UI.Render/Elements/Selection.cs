using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render.Elements;

public class Selection : DrawingObject
{
    private DrawingObject? _selectedObject;

    public Selection(double x, double y) : base(x, y)
    {
    }

    public bool IsSelected => _selectedObject != null;

    public override async Task DrawAsync(Context2D context)
    {
        const int padding = 8;

        if (_selectedObject != null)
        {
            await context.SaveAsync();

            await context.BeginPathAsync();
            await context.LineWidthAsync(1);
            await context.StrokeStyleAsync("#00FFE8");

            if (_selectedObject is Freedraw freedraw)
            {
                var width = freedraw.MaxX - freedraw.MinX;
                var height = freedraw.MaxY - freedraw.MinY;
                await context.StrokeRectAsync(
                    x: freedraw.MinX - padding,
                    y: freedraw.MinY - padding,
                    width: width + 2 * padding,
                    height: height + 2 * padding);
            }
            else if (_selectedObject is Line line)
            {
                const double width = 10;
                
                await context.StrokeRectAsync(
                    x: line.Transform.X1 - padding,
                    y: line.Transform.Y1 - padding,
                    width: width + padding,
                    height: width + padding);
                
                await context.StrokeRectAsync(
                    x: line.Transform.X2 - padding,
                    y: line.Transform.Y2 - padding,
                    width: width + padding,
                    height: width + padding);
            }
            else
            {
                await context.StrokeRectAsync(
                    x: _selectedObject.Transform.X1 - padding,
                    y: _selectedObject.Transform.Y1 - padding,
                    width: _selectedObject.Transform.Width + 2 * padding,
                    height: _selectedObject.Transform.Height + 2 * padding);
            }

            await context.ClosePathAsync();
            await context.RestoreAsync();
        }
    }

    public override bool IsHit(double x, double y)
    {
        return x >= Transform.X1 && x <= Transform.X2 && y >= Transform.Y1 && y <= Transform.Y2;
    }

    public void Set(DrawingObject drawingObjects)
    {
        _selectedObject = drawingObjects;
    }

    public void Clear()
    {
        _selectedObject = null;
    }

    public override void Move(double offsetX, double offsetY)
    {
        if (_selectedObject == null)
        {
            return;
        }

        _selectedObject.Move(offsetX, offsetY);
    }
}