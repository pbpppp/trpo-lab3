using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TrpoLab3.Lib;
using TrpoLab5.WebApp.Models;

namespace TrpoLab5.WebApp.Controllers;

public class ConeController : Controller
{
    private static readonly CultureInfo ResultCulture = CultureInfo.GetCultureInfo("ru-RU");
    private readonly ConeCalculator _calculator;

    public ConeController(ConeCalculator calculator)
    {
        _calculator = calculator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var vm = new ConeViewModel();
        Calculate(vm);
        return View(vm);
    }

    [HttpPost]
    public IActionResult Index(IFormCollection collection)
    {
        var vm = new ConeViewModel
        {
            RadiusText = collection["RadiusText"].ToString(),
            HeightText = collection["HeightText"].ToString()
        };

        Validate(vm);

        if (!ModelState.IsValid)
        {
            vm.SlantHeightText = "—";
            vm.SurfaceAreaText = "—";
            return View(vm);
        }

        Calculate(vm);
        return View(vm);
    }

    private void Validate(ConeViewModel vm)
    {
        ValidateValue(vm.RadiusText, nameof(vm.RadiusText), "радиус основания");
        ValidateValue(vm.HeightText, nameof(vm.HeightText), "высоту конуса");
    }

    private void ValidateValue(string text, string propertyName, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            ModelState.AddModelError(propertyName, $"Введите {fieldName}.");
            return;
        }

        if (!TryParse(text, out double value))
        {
            ModelState.AddModelError(propertyName, "Введите число, например 3 или 2,5.");
            return;
        }

        if (!double.IsFinite(value))
        {
            ModelState.AddModelError(propertyName, "Значение должно быть конечным числом.");
            return;
        }

        if (value <= 0)
        {
            ModelState.AddModelError(propertyName, "Значение должно быть больше нуля.");
            return;
        }

        if (value > 1_000_000)
        {
            ModelState.AddModelError(propertyName, "Значение не должно превышать 1 000 000.");
        }
    }

    private static bool TryParse(string text, out double value)
    {
        return double.TryParse(
            text.Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out value);
    }

    private void Calculate(ConeViewModel vm)
    {
        double radius = double.Parse(
            vm.RadiusText.Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture);
        double height = double.Parse(
            vm.HeightText.Replace(',', '.'),
            NumberStyles.Float,
            CultureInfo.InvariantCulture);

        double slantHeight = Math.Sqrt(radius * radius + height * height);
        double surfaceArea = _calculator.SurfaceArea(radius, height);

        vm.SlantHeightText = slantHeight.ToString("F4", ResultCulture);
        vm.SurfaceAreaText = surfaceArea.ToString("F4", ResultCulture);
    }
}
