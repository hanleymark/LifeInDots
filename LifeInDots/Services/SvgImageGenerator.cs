using LifeInDots.Models;
using System.Globalization;
using System.Text;

namespace LifeInDots.Services;

public class SvgImageGenerator
{
    public string Generate(CreateImageRequest request)
    {
        var days = Enumerable.Range(0, request.EndDate.DayNumber - request.StartDate.DayNumber + 1)
            .Select(offset => request.StartDate.AddDays(offset))
            .ToList();

        // Calculate columns and rows based on the aspect ratio and number of dots
        var (cols, rows) = CalculateGrid(days.Count, request.AspectRatio);

        const int dotSize = 10;
        const int gap = 4;
        int svgWidth = cols * (dotSize + gap);
        int svgHeight = rows * (dotSize + gap);

        var sb = new StringBuilder();
        sb.AppendLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{svgWidth}\" height=\"{svgHeight}\" viewBox=\"0 0 {svgWidth} {svgHeight}\">");
        sb.AppendLine("  <rect width=\"100%\" height=\"100%\" fill=\"white\"/>");

        for (int i = 0; i < days.Count; i++)
        {
            int col = i % cols;
            int row = i / cols;

            int cx = col * (dotSize + gap) + dotSize / 2;
            int cy = row * (dotSize + gap) + dotSize / 2;

            var day = days[i];
            string colour = "lightgrey"; // default dot colour

            var matchingEvent = request.Events.FirstOrDefault(e =>
                day >= e.StartDate && day <= (e.EndDate ?? e.StartDate));

            if (matchingEvent != null)
            {
                var colourHex = request.Palette
                    .FirstOrDefault(p => p.Description == matchingEvent.ColourDescription)?.Hex;

                if (!string.IsNullOrWhiteSpace(colourHex))
                {
                    colour = colourHex;
                }
            }

            sb.AppendLine($"  <circle cx=\"{cx}\" cy=\"{cy}\" r=\"{dotSize / 2}\" fill=\"{colour}\">");

            if (matchingEvent != null)
            {
                var title = $"{matchingEvent.Description} – {day:yyyy-MM-dd}";
                sb.AppendLine($"    <title>{System.Security.SecurityElement.Escape(title)}</title>");
            }
            sb.AppendLine("  </circle>");
        }

        sb.AppendLine("</svg>");
        return sb.ToString();
    }

    private (int cols, int rows) CalculateGrid(int dotCount, string aspectRatio)
    {
        var (w, h) = ParseAspectRatio(aspectRatio);
        var ratio = (double)w / h;

        int rows = (int)Math.Ceiling(Math.Sqrt(dotCount / ratio));
        int cols = (int)Math.Ceiling(dotCount / (double)rows);

        return (cols, rows);
    }

    private (int w, int h) ParseAspectRatio(string aspectRatio)
    {
        var parts = aspectRatio.Split(':');
        if (parts.Length != 2 ||
            !int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int w) ||
            !int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int h) ||
            w <= 0 || h <= 0)
        {
            throw new ArgumentException("AspectRatio must be in the format W:H (e.g. 16:9)");
        }

        return (w, h);
    }
}
