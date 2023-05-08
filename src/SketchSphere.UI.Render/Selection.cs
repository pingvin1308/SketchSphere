using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

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
            await context.BeginPathAsync();
            var previousStrokeStyle = await context.StrokeStyleAsync();
            var previousLineWidth = await context.LineWidthAsync();

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
            else
            {
                await context.StrokeRectAsync(
                    x: _selectedObject.X - padding,
                    y: _selectedObject.Y - padding,
                    width: _selectedObject.Width + 2 * padding,
                    height: _selectedObject.Height + 2 * padding);
            }

            if (_selectedObject is Line line)
            {
                await context.StrokeRectAsync(
                    x: line.X2 - padding,
                    y: line.Y2 - padding,
                    width: line.Width + 2 * padding,
                    height: line.Height + 2 * padding);
            }

            await context.StrokeStyleAsync(previousStrokeStyle);
            await context.LineWidthAsync(previousLineWidth);
            await context.ClosePathAsync();
        }
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