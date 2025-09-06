using ClippingAlgorithms.Models.Points;

namespace ClippingAlgorithms.Clustering.Metrics;

public class EuclideanMetric : IMetric<double, PointD>
{
    public double Compute(PointD p1, PointD p2)
        => (p1 - p2).Norm();
}
