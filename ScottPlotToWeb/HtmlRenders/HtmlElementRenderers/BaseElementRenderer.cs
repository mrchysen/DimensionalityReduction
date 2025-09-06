using System.Text;

namespace ScottPlotToWeb.HtmlRenders.HtmlElementRenderers;

public abstract class BaseElementRenderer
{
    public virtual StringBuilder Render(ReportElement element, StringBuilder? render)
    {
        return render ?? new StringBuilder();
    }
}
