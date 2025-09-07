using DimensionalityReduction.Models;
using ScottPlot;
using ScottPlotToWeb;

namespace DimensionalityReduction.Plots;

internal class BookingStatusPerDayLinePlot(IEnumerable<CarBooking> carBookings) : PlotBase
{
    private readonly IEnumerable<CarBooking> _carBookings = carBookings;
    private readonly Plot _plot = new();

    public override SavedImageInfo GetImage()
    {
        var completedGrouping = GetCountOfStatusByDate(BookingStatus.Completed);
        var incompletedGrouping = GetCountOfStatusByDate(BookingStatus.Incomplete);
        var noDriverFoundGrouping = GetCountOfStatusByDate(BookingStatus.NoDriverFound);
        var cancelledByCustomerGrouping = GetCountOfStatusByDate(BookingStatus.CancelledByCustomer);
        var cancelledByDriverGrouping = GetCountOfStatusByDate(BookingStatus.CancelledByDriver);

        var sp1 = _plot.Add.Scatter(
            completedGrouping.Select(c => c.Date.ToDateTime(new TimeOnly())).ToArray(),
            completedGrouping.Select(c => c.Count).ToArray());
        sp1.LegendText = "Completed";

        var sp2 = _plot.Add.Scatter(
            incompletedGrouping.Select(c => c.Date.ToDateTime(new TimeOnly())).ToArray(),
            incompletedGrouping.Select(c => c.Count).ToArray()); ;
        sp2.LegendText = "Incomplete";

        var sp3 = _plot.Add.Scatter(
            noDriverFoundGrouping.Select(c => c.Date.ToDateTime(new TimeOnly())).ToArray(),
            noDriverFoundGrouping.Select(c => c.Count).ToArray()); ;
        sp3.LegendText = "NoDriverFound";

        var sp4 = _plot.Add.Scatter(
            cancelledByCustomerGrouping.Select(c => c.Date.ToDateTime(new TimeOnly())).ToArray(),
            cancelledByCustomerGrouping.Select(c => c.Count).ToArray()); ;
        sp4.LegendText = "CancelledByCustomer";

        var sp5 = _plot.Add.Scatter(
            cancelledByDriverGrouping.Select(c => c.Date.ToDateTime(new TimeOnly())).ToArray(),
            cancelledByDriverGrouping.Select(c => c.Count).ToArray()); ;
        sp5.LegendText = "CancelledByDriver";

        _plot.Axes.DateTimeTicksBottom();
        _plot.ShowLegend();

        return _plot.SavePng("demo.png", Width, Height);
    }

    private CountOfStatusByDate[] GetCountOfStatusByDate(BookingStatus status) =>
        _carBookings
            .GroupBy(x => x.Date)
            .Select(g => new CountOfStatusByDate
            (
                g.Key,
                g.Count(c => c.Status == status)
            ))
            .OrderBy(x => x.Date)
            .ToArray();
}

public record CountOfStatusByDate(DateOnly Date, int Count);