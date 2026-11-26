using System.ComponentModel.DataAnnotations;

namespace TrpoLab5.WebApp.Models;

public class ConeViewModel
{
    [Required(ErrorMessage = "Введите радиус основания.")]
    [RegularExpression(@"^\s*-?\d+([.,]\d+)?\s*$", ErrorMessage = "Введите число, например 3 или 2,5.")]
    [Display(Name = "Радиус основания, r")]
    public string RadiusText { get; set; } = "3";

    [Required(ErrorMessage = "Введите высоту конуса.")]
    [RegularExpression(@"^\s*-?\d+([.,]\d+)?\s*$", ErrorMessage = "Введите число, например 3 или 2,5.")]
    [Display(Name = "Высота конуса, h")]
    public string HeightText { get; set; } = "4";

    public string SlantHeightText { get; set; } = "—";

    public string SurfaceAreaText { get; set; } = "—";
}
