using ScottPlot;

namespace ScottPlotToWeb;

public class Report
{
    private List<ReportElement> _elements = [];

    public Report AddPlot(SavedImageInfo info)
    {
        _elements.Add(new()
        {
            Type = ElementType.Plot,
            ImageInfo = info
        });

        return this;
    }

    public Report AddText(string text)
    {
        _elements.Add(new()
        {
            Type = ElementType.Text,
            Text = text
        });

        return this;
    }

    public Report AddHeader(string header)
    {
        _elements.Add(new()
        {
            Type = ElementType.Header,
            Header = header
        });

        return this;
    }

    public IEnumerable<ReportElement> Elements => _elements;
}
