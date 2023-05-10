using Excubo.Blazor.Canvas.Contexts;

namespace SketchSphere.UI.Render.Elements;

public class SelectionBox : DrawingObject
{
    private readonly HashSet<DrawingObject> _selectedObjects;
    private bool _isFinished = false;

    public double X1 => Math.Min(Transform.X1, Transform.X2);
    public double Y1 => Math.Min(Transform.Y1, Transform.Y2);

    public double X2 => Math.Max(Transform.X1, Transform.X2);
    public double Y2 => Math.Max(Transform.Y1, Transform.Y2);

    public SelectionBox(double x, double y) : base(x, y)
    {
        _selectedObjects = new HashSet<DrawingObject>();
    }

    public bool IsSelected => _selectedObjects.Any();
    public bool IsSelectedAndFinished => _selectedObjects.Any() && _isFinished;

    public override async Task DrawAsync(Context2D context)
    {
        await context.SaveAsync();
        await context.LineWidthAsync(2);
        await context.StrokeStyleAsync("#00FFE8");

        if (_isFinished == false)
        {
            await context.StrokeRectAsync(X1, Y1, Transform.Width, Transform.Height);
        }

        if (_selectedObjects.Any())
        {
            await context.LineWidthAsync(1);

            const int padding = 8;

            var (minX, minY, maxX, maxY) = GetBounds();
            var width = maxX - minX;
            var height = maxY - minY;
            await context.StrokeRectAsync(
                x: minX - padding,
                y: minY - padding,
                width: width + 2 * padding,
                height: height + 2 * padding);
        }

        await context.RestoreAsync();
    }

    public void FinishDrawing()
    {
        _isFinished = true;
        var (minX, minY, maxX, maxY) = GetBounds();
        Transform.SetStart(minX, minY);
        Transform.SetEnd(maxX, maxY);
    }

    public override bool IsHit(double x, double y)
    {
        return x >= X1 && y >= Y1 && x <= X2 && y <= Y2;
    }

    public void AddIntersected(IReadOnlyCollection<DrawingObject> drawingObjects)
    {
        foreach (var drawingObject in drawingObjects)
        {
            if (IsHit(drawingObject.Transform.X1, drawingObject.Transform.Y1))
            {
                _selectedObjects.Add(drawingObject);
            }
        }
    }

    public override void Move(double offsetX, double offsetY)
    {
        Console.WriteLine($"Moving selection box by {offsetX}, {offsetY}");
        foreach (var selectedObject in _selectedObjects)
        {
            selectedObject.Move(offsetX, offsetY);
        }

        base.Move(offsetX, offsetY);
    }

    public void Clear()
    {
        _selectedObjects.Clear();
    }

    private (double X1, double Y1, double X2, double Y2) GetBounds()
    {
        var minX = _selectedObjects
            .Select(selectedObject =>
            {
                return selectedObject switch
                {
                    Freedraw freedraw => freedraw.Points.Min(point => point.X),
                    Line line => Math.Min(line.Transform.X1, line.Transform.X2),
                    _ => selectedObject.Transform.X1
                };
            })
            .Order()
            .First();

        var maxX = _selectedObjects
            .Select(selectedObject =>
            {
                return selectedObject switch
                {
                    Freedraw freedraw => freedraw.Points.Max(point => point.X),
                    Line line => Math.Max(line.Transform.X1, line.Transform.X2),
                    _ => selectedObject.Transform.X2
                };
            })
            .OrderDescending()
            .First();

        var minY = _selectedObjects
            .Select(selectedObject =>
            {
                return selectedObject switch
                {
                    Freedraw freedraw => freedraw.Points.Min(point => point.Y),
                    Line line => Math.Min(line.Transform.Y1, line.Transform.Y2),
                    _ => selectedObject.Transform.Y1
                };
            })
            .Order()
            .First();


        var maxY = _selectedObjects
            .Select(selectedObject =>
            {
                return selectedObject switch
                {
                    Freedraw freedraw => freedraw.Points.Max(point => point.Y),
                    Line line => Math.Max(line.Transform.Y1, line.Transform.Y2),
                    _ => selectedObject.Transform.Y2
                };
            })
            .OrderDescending()
            .First();

        return (minX, minY, maxX, maxY);
    }
}