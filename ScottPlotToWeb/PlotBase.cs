using ScottPlot;

namespace ScottPlotToWeb;

public abstract class PlotBase
{
    public const int Width = 960;

    public const int Height = 540;

    public abstract SavedImageInfo GetImage();
}
