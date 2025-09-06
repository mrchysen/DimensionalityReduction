using ClippingAlgorithms.Models.Points;
using ClippingAlgorithms.Models.Polygons;

namespace ClippingAlgorithms.HullCreators;

public interface INonconvexCreator
{
    public Polygon CreateHull(List<PointD> points, IConvexHullCreator? convexHullCreator = null);
}
