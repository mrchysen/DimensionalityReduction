﻿using ClippingAlgorithms.Models.Points;

namespace ClippingAlgorithms.Clustering;

public class Cluster
{
    private PointD _currentCentroid;
    private PointD? _lastCentroid;

    public List<PointD> Points { get; set; } = new();

    public PointD Centroid 
    {
        get => _currentCentroid;
        set
        {
            _lastCentroid = _currentCentroid;
            _currentCentroid = value;
        } 
    }

    public bool IsCentroidChanged(double epsilon = 0.001d)
    {
        // изменился = новая точка и старая разные или не задана старая точка
        // не изменился = точки одинаковые

        if(_lastCentroid == null) return true;

        return (_lastCentroid.Value - _currentCentroid).Norm() < epsilon;
    }

    public void ComputeNewCentroid()
    {
        double sumX = 0;
        double sumY = 0;

        foreach (PointD point in Points) 
        {
            sumX += point.X;
            sumY += point.Y;
        }

        Centroid = new PointD(sumX / Points.Count, sumY / Points.Count);
    }
}
