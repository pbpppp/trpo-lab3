using System;

namespace TrpoLab3.Lib
{
    public class ConeCalculator
    {
        public double SurfaceArea(double radius, double height)
        {
            if (radius <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(radius),
                    radius,
                    "Радиус основания должен быть больше нуля.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    height,
                    "Высота конуса должна быть больше нуля.");
            }

            double slantHeight = Math.Sqrt(radius * radius + height * height);
            return Math.PI * radius * (radius + slantHeight);
        }
    }
}
