using System.IO;
using System.Text;

namespace ScottPlotToWeb.HtmlRenders.HtmlElementRenderers;

public class PlotRenderer : BaseElementRenderer
{
    private string _className;

    public PlotRenderer(string? className = null)
    {
        _className = className ?? "basic_plot";
    }

    public override StringBuilder Render(
        ReportElement element,
        StringBuilder? render)
    {
        render = base.Render(element, render);

        var src = element.ImageInfo!.Path;

        string relativePath = Path.GetRelativePath(
    Directory.GetCurrentDirectory(),
        element.ImageInfo!.Path
        );

        render.Append($"""
            <img class="{_className}" src="{relativePath}">
            """);

        return render;
    }
}
