using ScottPlotToWeb.HtmlRenders.HtmlElementRenderers;
using System.Text;

namespace ScottPlotToWeb.HtmlRenders;

public interface IRenderer
{
    public Task<FileInfo> RenderReportAsync(Report report);
}

public class HtmlRenderer(
    StringBuilder? additionalStyles = null) : IRenderer
{
    private const string TemplatePath = "./HtmlRenders/Tamplate.html";
    private const string RenderedFileName = "Rendered.html";
    private const string BodyMark = "@Body";
    private const string StyleMark = "@Style";

    private StringBuilder _additionalStyles = additionalStyles ?? new();

    private StringBuilder _body = new();

    private readonly HeaderRenderer _headerRenderer = new();
    private readonly TextRenderer _textRenderer = new();
    private readonly PlotRenderer _plotRenderer = new();

    public async Task<FileInfo> RenderReportAsync(Report report)
    {
        foreach (var element in report.Elements)
        {
            var renderer = RendererFactoryByElementType(element);

            renderer.Render(element, _body);
        }

        var templateText = await File.ReadAllTextAsync(TemplatePath);
        var fileSb = new StringBuilder(templateText);

        PasteBodyToFile(fileSb);
        PasteAdditionalStyles(fileSb);

        var fileInfo = new FileInfo(RenderedFileName);

        await SaveRenderedFileAsync(fileSb, fileInfo);

        return fileInfo;
    }

    private async Task SaveRenderedFileAsync(
        StringBuilder fileSb,
        FileInfo fileInfo)
    {
        await File.WriteAllTextAsync(
            fileInfo.FullName,
            fileSb.ToString());
    }

    private void PasteAdditionalStyles(StringBuilder file) =>
        file.Replace(StyleMark, _body.ToString());

    private void PasteBodyToFile(StringBuilder file) =>
        file.Replace(BodyMark, _body.ToString());

    private BaseElementRenderer RendererFactoryByElementType(ReportElement element)
    {
        return element.Type switch
        {
            ElementType.Plot => _plotRenderer,
            ElementType.Text => _textRenderer,
            ElementType.Header => _headerRenderer,
            _ => throw new InvalidDataException(element.Type.ToString())
        };
    }
}
