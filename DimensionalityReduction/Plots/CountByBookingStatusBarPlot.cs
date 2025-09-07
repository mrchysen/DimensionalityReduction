using DimensionalityReduction.Models;
using ScottPlot;
using ScottPlotToWeb;

namespace DimensionalityReduction.Plots;

public class CountByBookingStatusBarPlot(IEnumerable<CarBooking> carBookings) : PlotBase
{
    private readonly IEnumerable<CarBooking> _carBookings = carBookings;
    private readonly Plot _plot = new();

    public override SavedImageInfo GetImage()
    {
        var values = _carBookings
            .GroupBy(x => x.Status)
            .Select(g => new
            {
                Status = g.Key?.ToString() ?? "Unknown",
                Count = g.Count()
            })
            .OrderBy(x => x.Count)
            .ToArray();

        var barPlot = _plot.Add.Bars(values.Select(c => (double)c.Count).ToArray());

        foreach (var (Bar, Count) in barPlot.Bars.Zip(values.Select(c => c.Count)))
        {
            Bar.Label = Count.ToString();
        }

        Tick[] ticks = values
            .Select((grouping, i) => new Tick(i, grouping.Status))
            .ToArray();

        _plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
        _plot.Axes.Bottom.MajorTickStyle.Length = 0;

        _plot.Axes.Margins(bottom: 0);

        return _plot.SavePng(
            Guid.NewGuid().ToString(), 
            Width, 
            Height);
    }
}
