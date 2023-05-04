using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public class Selection : DrawingObject
{
    private DrawingObject[] _selectedObjects;

    public Selection(double x, double y) : base(x, y)
    {
        _selectedObjects = Array.Empty<DrawingObject>();
    }

    public bool IsSelected => _selectedObjects.Any();

    public override async Task DrawAsync(Context2D context)
    {
        const int padding = 8;

        var selectedObject = _selectedObjects.FirstOrDefault();
        if (selectedObject != null)
        {
            await context.BeginPathAsync();
            var previousStrokeStyle = await context.FillAndStrokeStyles.StrokeStyleAsync();
            var previousLineWidth = await context.LineWidthAsync();

            await context.LineWidthAsync(1);
            await context.StrokeStyleAsync("#00FFE8");

            if (selectedObject is Freedraw freedraw)
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
                    x: selectedObject.X - padding,
                    y: selectedObject.Y - padding,
                    width: selectedObject.Width + 2 * padding,
                    height: selectedObject.Height + 2 * padding);
            }

            if (selectedObject is Line line)
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

    public void Set(params DrawingObject[] drawingObjects)
    {
        _selectedObjects = drawingObjects;
    }

    public void Clear()
    {
        _selectedObjects = Array.Empty<DrawingObject>();
    }

    public override void Move(double offsetX, double offsetY)
    {
        var selectedObject = _selectedObjects.FirstOrDefault();
        if (selectedObject == null)
        {
            return;
        }

        selectedObject.Move(offsetX, offsetY);
    }
}