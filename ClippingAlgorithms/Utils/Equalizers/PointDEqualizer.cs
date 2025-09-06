﻿using ClippingAlgorithms.Models.Points;
using ClippingAlgorithms.Settings;

namespace ClippingAlgorithms.Utils.Equalizers;

public class PointDEqualizer : IEqualizer<PointD>
{
    private double _epsilon { get; set; }

    public PointDEqualizer(IAccuracySettings? accuracySettings = null)
    {
        accuracySettings = accuracySettings ?? new CoreAccuracySetting();

        _epsilon = accuracySettings.GetAccuracy;
    }

    public bool IsEquals(PointD obj1, PointD obj2)
		=> Math.Abs(obj1.X - obj2.X) < _epsilon && Math.Abs(obj1.Y - obj2.Y) < _epsilon;
}
