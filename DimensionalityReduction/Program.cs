using DimensionalityReduction.DataParser;
using DimensionalityReduction.Models;
using DimensionalityReduction.Plots;
using ScottPlotToWeb;
using ScottPlotToWeb.HtmlRenders;
using ScottPlotToWeb.Launchers;

IParser<CarBooking> parser = new CsvParser();

using var stream = new StreamReader("./Data/ncr_ride_bookings.csv");

var bookings = parser.Parse(stream).ToList();

var report = new Report()
    .AddHeader("Отчёт 2025")
    .AddText("Количество по статусам")
    .AddPlot(new CountByBookingStatusBarPlot(bookings))
    .AddText("Количество статусов по дням")
    .AddPlot(new BookingStatusPerDayLinePlot(bookings));

IRenderer renderer = new HtmlRenderer();

var fileInfo = await renderer.RenderReportAsync(report);

var browserProccess = new BrowserLauncher()
    .Launch(fileInfo);

if (browserProccess is null)
    throw new NullReferenceException("No browser process");

await browserProccess.WaitForExitAsync();
