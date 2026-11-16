using NUnit.Framework;
using TrpoLab3.Lib;

namespace TrpoLab3.Tests
{
    [TestFixture]
    public class ConeCalculatorTests
    {
        [Test]
        public void SurfaceArea_Radius3Height4_Returns24Pi()
        {
            var calculator = new ConeCalculator();

            const double expected = 24 * Math.PI;
            double actual = calculator.SurfaceArea(3, 4);

            Assert.AreEqual(expected, actual, 1e-9);
        }

        [Test]
        public void SurfaceArea_Radius5Height12_Returns90Pi()
        {
            var calculator = new ConeCalculator();

            const double expected = 90 * Math.PI;
            double actual = calculator.SurfaceArea(5, 12);

            Assert.AreEqual(expected, actual, 1e-9);
        }

        [Test]
        public void SurfaceArea_Radius1Height1_ReturnsPiTimesOnePlusSqrtTwo()
        {
            var calculator = new ConeCalculator();

            double expected = Math.PI * (1 + Math.Sqrt(2));
            double actual = calculator.SurfaceArea(1, 1);

            Assert.AreEqual(expected, actual, 1e-9);
        }

        [Test]
        public void SurfaceArea_Radius2Point5Height6_Returns22Point5Pi()
        {
            var calculator = new ConeCalculator();

            const double expected = 22.5 * Math.PI;
            double actual = calculator.SurfaceArea(2.5, 6);

            Assert.AreEqual(expected, actual, 1e-9);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void SurfaceArea_InvalidRadius_Throws(double radius)
        {
            var calculator = new ConeCalculator();

            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => calculator.SurfaceArea(radius, 2));

            Assert.AreEqual("radius", exception.ParamName);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void SurfaceArea_InvalidHeight_Throws(double height)
        {
            var calculator = new ConeCalculator();

            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => calculator.SurfaceArea(2, height));

            Assert.AreEqual("height", exception.ParamName);
        }
    }
}
