using Excubo.Blazor.Canvas.Contexts;
using Microsoft.JSInterop;

namespace SketchSphere.UI.Render.Elements;

public class Image : DrawingObject
{
    private readonly IJSRuntime _js;
    private string _source = string.Empty;
    
    public Image(
        IJSRuntime js, 
        string source, 
        double x, 
        double y, 
        double width, 
        double height) : base(x, y)
    {
        _js = js;
        _source = source;
        Transform.SetEnd(x + width, y + height);
    }

    public override async Task DrawAsync(Context2D context)
    {
        await _js.InvokeVoidAsync("eval", $"myimage = getImage('{_source}')");
        await context.DrawImageAsync(
            image: "myimage",
            dx: Transform.X1,
            dy: Transform.Y1,
            dwidth: Transform.Width,
            dheight: Transform.Height);
    }

    public override bool IsHit(double x, double y)
    {
        return x >= Transform.X1 && x <= Transform.X2 && y >= Transform.Y1 && y <= Transform.Y2;
    }
}