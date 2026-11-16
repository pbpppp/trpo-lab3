using System;

namespace TrpoLab3.Lib
{
    public class ConeCalculator
    {
        public double SurfaceArea(double radius, double height)
        {
            double slantHeight = Math.Sqrt(radius * radius + height * height);
            return Math.PI * radius * (radius + slantHeight);
        }
    }
}
