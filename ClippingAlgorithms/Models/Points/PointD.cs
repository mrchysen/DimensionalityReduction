﻿using System.Drawing;
using System.Text.Json.Serialization;

namespace ClippingAlgorithms.Models.Points;

public partial struct PointD
{
    public double X { get; set; }
    public double Y { get; set; }
    public PointD(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point ToDrawingPoint()
        => new Point((int)X, (int)Y);

    public override bool Equals(object? obj) 
        => obj is PointD && this == (PointD)obj;

    public override int GetHashCode() 
        => X.GetHashCode() ^ Y.GetHashCode();

    public override string ToString() 
        => $"{{{X:#.####} {Y:#.####}}}";

    public double Norm(int power = 1) => Math.Pow(Math.Sqrt(X * X + Y * Y), power);

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public PointD Normilized => new PointD(X / Norm(), Y / Norm());
}
