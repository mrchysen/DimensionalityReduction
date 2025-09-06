using System.Diagnostics;

namespace ScottPlotToWeb.Launchers;

public class BrowserLauncher
{
    public Process? Launch(FileInfo htmlFileInfo)
    {
        if (!htmlFileInfo.Exists)
        {
            throw new FileNotFoundException($"File not found: {htmlFileInfo.FullName}");
        }

        if (htmlFileInfo.Extension.ToLower() != ".html")
        {
            throw new ArgumentException("File extension must be .html");
        }

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = htmlFileInfo.FullName,
                UseShellExecute = true
            };

            return Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error while openning html file in browser: {ex.Message}", ex);
        }
    }
}
