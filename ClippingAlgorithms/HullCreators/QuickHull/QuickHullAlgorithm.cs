﻿using ClippingAlgorithms.Models.Lines;
using ClippingAlgorithms.Models.Points;
using ClippingAlgorithms.Models.Polygons;
using ClippingAlgorithms.PointsOrderers;

namespace ClippingAlgorithms.HullCreators.QuickHull;

public class QuickHullAlgorithm : IConvexHullCreator
{
    public List<PointD> convexHullPoints = new();
    public HashSet<PointD> visited = new();

    public Polygon CreateHull(List<PointD> points)
    {
        if (points.Count == 0)
            return new();

        convexHullPoints.Clear();
        visited.Clear();

        PointD max_p = points.Aggregate(PointDHelper.MaxPointD);
        PointD min_p = points.Aggregate(PointDHelper.MinPointD);

        convexHullPoints.Add(max_p);
        convexHullPoints.Add(min_p);
        visited.Add(max_p);
        visited.Add(min_p);

        var line = new Line(min_p, max_p);

        MakeForHalfPlane(line, RightPredicate, false, points);
        MakeForHalfPlane(line, LeftPredicate, true, points);

        return new Polygon(convexHullPoints.OrderClockwise().ToList()); 
    }

    public void MakeForHalfPlane(Line line, Predicate<double> predicate, bool leftPlane, List<PointD> points)
    {
        var currentPoints = LeftPoints(line, points, predicate);

        if (currentPoints.Count == 0)
            return;

        var maxPoint = currentPoints.Aggregate((PointD p1, PointD p2) => line.MinDistance(p1) >= line.MinDistance(p2) ? p1 : p2);
        
        convexHullPoints.Add(maxPoint);
        visited.Add(maxPoint);

        AddInnerPointToVisited(line.Point1, line.Point2, maxPoint, currentPoints);

        var line1 = new Line(line.Point1, maxPoint);
        var line2 = new Line(line.Point2, maxPoint);

        if (leftPlane)
        {
            MakeForHalfPlane(line1, LeftPredicate, true, currentPoints);
            MakeForHalfPlane(line2, RightPredicate, false, currentPoints);
        }
        else // right
        {
            MakeForHalfPlane(line1, RightPredicate, false, currentPoints);
            MakeForHalfPlane(line2, LeftPredicate, true, currentPoints);
        }
    }

    public void AddInnerPointToVisited(PointD p1, PointD p2, PointD p3, List<PointD> points)
    {
        foreach (var point in points)
        {
            if(HullHelperUtils.IsPointInTriangle(p1,p2,p3, point))
            {
                visited.Add(point);
            }
        }
    }

    private List<PointD> LeftPoints(Line line, List<PointD> points, Predicate<double> leftOrRightPredicate)
    {
        List<PointD> leftPoints = new();

        foreach (var point in points)
        {
            if (!visited.Contains(point))
            {
                if(leftOrRightPredicate(line.DirectingVector * (point - line.Point1)))
                    leftPoints.Add(point);
            }
        }

        return leftPoints;
    }

    private Predicate<double> RightPredicate => (value) => value < 0; // правая полуплоскость
    private Predicate<double> LeftPredicate => (value) => value > 0; // левая полуплоскось
}
