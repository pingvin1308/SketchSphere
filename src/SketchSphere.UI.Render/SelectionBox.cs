using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render;

public class SelectionBox : DrawingObject
{
    private readonly HashSet<DrawingObject> _selectedObjects;

    public SelectionBox(double x, double y) : base(x, y)
    {
        _selectedObjects = new HashSet<DrawingObject>();
    }
    
    public bool IsSelected => _selectedObjects.Any();

    public override async Task DrawAsync(Context2D context)
    {
        var previousStrokeStyle = await context.StrokeStyleAsync();
        var previousLineWidth = await context.LineWidthAsync();
        await context.LineWidthAsync(2);
        await context.StrokeStyleAsync("#00FFE8");

        if (_selectedObjects.Any())
        {
            await context.LineDashOffsetAsync(2);

            const int padding = 8;

            var sortedObjects = _selectedObjects
                .Select(selectedObject => (selectedObject.X, selectedObject.Y))
                .ToArray();

            var maxX = _selectedObjects
                .Select(selectedObject => selectedObject.X + selectedObject.Width)
                .OrderDescending()
                .First();

            var maxY = _selectedObjects
                .Select(selectedObject => selectedObject.Y + selectedObject.Height)
                .OrderDescending()
                .First();

            var minX = sortedObjects.Min(point => point.X);
            var minY = sortedObjects.Min(point => point.Y);

            var width = maxX - minX;
            var height = maxY - minY;
            await context.StrokeRectAsync(
                x: minX - padding,
                y: minY - padding,
                width: width + 2 * padding,
                height: height + 2 * padding);
        }
        else
        {
            await context.StrokeRectAsync(X, Y, Width, Height);
        }

        await context.StrokeStyleAsync(previousStrokeStyle);
        await context.LineWidthAsync(previousLineWidth);
    }

    public void AddIntersected(IReadOnlyCollection<DrawingObject> drawingObjects)
    {
        foreach (var drawingObject in drawingObjects)
        {
            if (IsHit(drawingObject.X, drawingObject.Y))
            {
                var result = _selectedObjects.Add(drawingObject);
                if (result)
                {
                    Console.WriteLine($"Added {drawingObject.GetType().Name} to selection.");
                }
            }
        }
    }

    public void Clear()
    {
        _selectedObjects.Clear();
    }
}