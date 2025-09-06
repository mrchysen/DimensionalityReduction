using ClippingAlgorithms.Colors;
using ClippingAlgorithms.Models.Polygons;

namespace ClippingAlgorithms.Models.Colors.Extensions;

public static class RandomExtension
{
    public static IEnumerable<Polygon> RandomColors(this IEnumerable<Polygon> polygons)
    {
        foreach (var polygon in polygons)
        {
            polygon.Color = RandomColor.Get();
        }

        return polygons;
    }
}
