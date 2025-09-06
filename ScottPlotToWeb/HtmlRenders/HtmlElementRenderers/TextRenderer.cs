using System.Text;

namespace ScottPlotToWeb.HtmlRenders.HtmlElementRenderers;

public class TextRenderer : BaseElementRenderer
{
    private string _className = "basic_text";

    public TextRenderer(string? className = null)
    {
        _className = className ?? "basic_text";
    }

    public override StringBuilder Render(
        ReportElement element, 
        StringBuilder? render)
    {
        render = base.Render(element, render);

        render.Append($"""
            <p class="{_className}">{element.Text}</p>
            """);

        return render;
    }
}
