using Microsoft.AspNetCore.Components;
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

    public async ValueTask<Image> GetImage(double x, double y, string source)
    {
        var imageSize = await _jsInterop.InvokeAsync<Size>("getImageSize", source);
        return new Image(_jsInterop, source, x, y, imageSize.Width, imageSize.Height);
    }

    public record Size(double Width, double Height);
}

public class PasteMultimediaEventsArgs: EventArgs
{
    public bool IsMultimedia { get; set; }
    public string Data { get; set; }
}

[EventHandler("onpastemultimedia", typeof(PasteMultimediaEventsArgs),
    enableStopPropagation: true, enablePreventDefault: true)]
public static class EventHandlers
{
 
}