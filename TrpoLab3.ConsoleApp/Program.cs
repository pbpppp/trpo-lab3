using System;
using System.Globalization;
using TrpoLab3.Lib;

namespace TrpoLab3.ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Лабораторная работа № 3. Вариант 14");
            Console.WriteLine("Площадь поверхности круглого конуса");
            Console.WriteLine("Формула: S = π·r·(r + √(r² + h²))");
            Console.WriteLine();

            double radius = ReadPositiveNumber("Введите радиус основания r: ");
            double height = ReadPositiveNumber("Введите высоту конуса h: ");

            var calculator = new ConeCalculator();
            double slantHeight = Math.Sqrt(radius * radius + height * height);
            double area = calculator.SurfaceArea(radius, height);

            Console.WriteLine();
            Console.WriteLine($"Образующая l = √(r² + h²) = {slantHeight:F4}");
            Console.WriteLine($"Площадь поверхности S = π·r·(r + l) = {area:F4}");
            Console.WriteLine();

            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }

        private static double ReadPositiveNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (double.TryParse(
                        input?.Replace(',', '.'),
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double value)
                    && double.IsFinite(value)
                    && value > 0)
                {
                    return value;
                }

                Console.WriteLine("Ошибка: введите положительное число (например, 3 или 2,5).");
            }
        }
    }
}
