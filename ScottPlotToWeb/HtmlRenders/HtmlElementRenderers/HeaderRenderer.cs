using System.Text;

namespace ScottPlotToWeb.HtmlRenders.HtmlElementRenderers;

public class HeaderRenderer : BaseElementRenderer
{
    private string _className;

    public HeaderRenderer(string? className = null)
    {
        _className = className ?? "basic_text";
    }

    public override StringBuilder Render(
        ReportElement element,
        StringBuilder? render)
    {
        render = base.Render(element, render);

        render.Append($"""
            <h1 class="{_className}">{element.Header}</h1>
            """);

        return render;
    }
}
