using ClippingAlgorithms.Models.Points;
using ClippingAlgorithms.Models.Polygons;

namespace ClippingAlgorithms.HullCreators;

public interface IConvexHullCreator
{
    public Polygon CreateHull(List<PointD> points);
}
