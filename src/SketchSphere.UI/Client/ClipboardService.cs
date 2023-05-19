using Microsoft.JSInterop;
using SketchSphere.UI.Render.Elements;

namespace SketchSphere.UI.Client;

public class ClipboardService : IClipboardService
{
    private readonly IJSRuntime _jsInterop;

    public ClipboardService(IJSRuntime jsInterop)
    {
        _jsInterop = jsInterop;
    }

    public async ValueTask<Image> GetImage(double x, double y)
    {
        var source = await _jsInterop.InvokeAsync<string>("pasteFromClipboard");
        var imageSize = await _jsInterop.InvokeAsync<Size>("getImageSize", source);
        return new Image(_jsInterop, source, x, y, imageSize.Width, imageSize.Height);
    }

    private record Size(double Width, double Height);
}