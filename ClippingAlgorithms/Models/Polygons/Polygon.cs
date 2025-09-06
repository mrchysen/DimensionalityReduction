using ClippingAlgorithms.Colors;
using ClippingAlgorithms.Models.Colors;
using ClippingAlgorithms.Models.Points;
using System.Text.Json.Serialization;

namespace ClippingAlgorithms.Models.Polygons;

public class Polygon
{
    public Polygon() { }

    public Polygon(List<PointD> points)
    {
        Points = points;
        Color = RandomColor.Get();
    }

    public Polygon(List<PointD> points, CoreColor color)
    {
        Points = points;
        Color = color;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int Count => Points.Count;

    public List<PointD> Points { get; set; } = new List<PointD>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public CoreColor Color { get; set; } = new();
}
