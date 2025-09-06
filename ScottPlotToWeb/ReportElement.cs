using ScottPlot;

namespace ScottPlotToWeb;


public class ReportElement
{
    public required ElementType Type { get; set; }

    public string? Text { get; set; }

    public SavedImageInfo? ImageInfo { get; set; }

    public string? Header { get; set; }
}

public enum ElementType
{
    Plot,
    Text,
    Header
}