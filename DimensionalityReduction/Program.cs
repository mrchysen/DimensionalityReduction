using DimensionalityReduction.DataParser;
using DimensionalityReduction.Models;
using ScottPlot;
using ScottPlotToWeb;
using ScottPlotToWeb.HtmlRenders;
using ScottPlotToWeb.Launchers;

IParser<CarBooking> parser = new CsvParser();

//using var stream = new StreamReader("./Data/ncr_ride_bookings.csv");

//var bookings = parser.Parse(stream).ToList();

Plot myPlot = new();

double[] dataX = { 1, 2, 3, 4, 5 };
double[] dataY = { 1, 4, 9, 16, 25 };

myPlot.Add.Scatter(dataX, dataY);

var report = new Report()
    .AddHeader("Отчёт 2025")
    .AddPlot(myPlot.SavePng("demo1.png", 400, 300))
    .AddText("Тут тип текст: " + string.Join(" ", dataX))
    .AddPlot(myPlot.SavePng("demo2.png", 1000, 800));

IRenderer renderer = new HtmlRenderer();

var fileInfo = await renderer.RenderReportAsync(report);

var browserProccess = new BrowserLauncher()
    .Launch(fileInfo);

if (browserProccess is null)
    throw new NullReferenceException("No browser process");

await browserProccess.WaitForExitAsync();
