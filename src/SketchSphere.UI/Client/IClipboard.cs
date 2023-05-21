using SketchSphere.UI.Render.Elements;

namespace SketchSphere.UI.Client;

public interface IClipboardService
{
    ValueTask<Image> GetImage(double x, double y, string source);
}