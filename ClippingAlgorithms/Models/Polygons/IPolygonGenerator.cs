using ClippingAlgorithms.Models.Points;

namespace ClippingAlgorithms.Models.Polygons;

public interface IPolygonGenerator
{
    Polygon Generate(bool sortByAngle, bool clockwise, int count);

    List<PointD> GeneratePoints(int count);
}
