using ScottPlot;
using ScottPlotToWeb;
using ScottPlot.Statistics;

namespace DimensionalityReduction.Models;

public class DailyTripHistogram(IEnumerable<CarBooking> carBookings, DateOnly date) : PlotBase
{
    private readonly IEnumerable<CarBooking> _carBookings = carBookings;
    private readonly Plot _plot = new();
    private readonly DateOnly _date = date;

    public override SavedImageInfo GetImage()
    {
        var dayTrip = _carBookings
            .Where(c => c.Date.Equals(_date))
            .OrderBy(c => c.Date)
            .Select(c => c.Time)
            .Select(c => c.ToTimeSpan().TotalDays)
            .OrderBy(c => c)
            .ToArray();

        var hist = Histogram.WithBinSize(2, dayTrip);

        var barPlot = _plot.Add.Bars(hist.Bins, hist.Counts);

        // Size each bar slightly less than the width of a bin
        foreach (var bar in barPlot.Bars)
        {
            bar.Size = hist.FirstBinSize * .8;
        }

        // Customize plot style
        _plot.Axes.Margins(bottom: 0);
        _plot.YLabel("One day number of trips");
        _plot.XLabel("Time");

        return _plot.SavePng(
            Guid.NewGuid().ToString(),
            Width,
            Height);
    }
}
