using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using TrpoLab3.Lib;
using TrpoLab4.WpfApp.Infrastructure;

namespace TrpoLab4.WpfApp.ViewModels;

public sealed class ConeViewModel : ObservableObject, INotifyDataErrorInfo
{
    private static readonly CultureInfo InputCulture = CultureInfo.InvariantCulture;
    private static readonly CultureInfo ResultCulture = CultureInfo.GetCultureInfo("ru-RU");

    private readonly ConeCalculator _calculator;
    private readonly Dictionary<string, string> _errors = new();

    private string _radiusText = "3";
    private string _heightText = "4";
    private string _slantHeightText = "—";
    private string _surfaceAreaText = "—";

    public ConeViewModel(ConeCalculator calculator)
    {
        _calculator = calculator;
        Recalculate();
    }

    public string RadiusText
    {
        get => _radiusText;
        set
        {
            if (!SetProperty(ref _radiusText, value))
            {
                return;
            }

            Validate();
            Recalculate();
        }
    }

    public string HeightText
    {
        get => _heightText;
        set
        {
            if (!SetProperty(ref _heightText, value))
            {
                return;
            }

            Validate();
            Recalculate();
        }
    }

    public string SlantHeightText
    {
        get => _slantHeightText;
        private set => SetProperty(ref _slantHeightText, value);
    }

    public string SurfaceAreaText
    {
        get => _surfaceAreaText;
        private set => SetProperty(ref _surfaceAreaText, value);
    }

    public string? RadiusError => GetError(nameof(RadiusText));

    public string? HeightError => GetError(nameof(HeightText));

    public bool HasRadiusError => RadiusError is not null;

    public bool HasHeightError => HeightError is not null;

    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        string? error = GetError(propertyName);
        return error is null ? Array.Empty<string>() : new[] { error };
    }

    private string? GetError(string? propertyName)
    {
        if (propertyName is not null && _errors.TryGetValue(propertyName, out string? error))
        {
            return error;
        }

        return null;
    }

    private void Validate()
    {
        SetError(
            nameof(RadiusText),
            ValidateValue(RadiusText, "радиус основания"),
            nameof(RadiusError),
            nameof(HasRadiusError));

        SetError(
            nameof(HeightText),
            ValidateValue(HeightText, "высоту конуса"),
            nameof(HeightError),
            nameof(HasHeightError));
    }

    private static string? ValidateValue(string text, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return $"Введите {fieldName}.";
        }

        string normalized = text.Replace(',', '.');

        if (!double.TryParse(normalized, NumberStyles.Float, InputCulture, out double value))
        {
            return "Введите число, например 3 или 2,5.";
        }

        if (!double.IsFinite(value))
        {
            return "Значение должно быть конечным числом.";
        }

        if (value <= 0)
        {
            return "Значение должно быть больше нуля.";
        }

        if (value > 1_000_000)
        {
            return "Значение не должно превышать 1 000 000.";
        }

        return null;
    }

    private void Recalculate()
    {
        if (HasErrors)
        {
            SlantHeightText = "—";
            SurfaceAreaText = "—";
            return;
        }

        double radius = double.Parse(RadiusText.Replace(',', '.'), NumberStyles.Float, InputCulture);
        double height = double.Parse(HeightText.Replace(',', '.'), NumberStyles.Float, InputCulture);

        double slantHeight = Math.Sqrt(radius * radius + height * height);
        double surfaceArea = _calculator.SurfaceArea(radius, height);

        SlantHeightText = slantHeight.ToString("F4", ResultCulture);
        SurfaceAreaText = surfaceArea.ToString("F4", ResultCulture);
    }

    private void SetError(string propertyName, string? message, string errorPropertyName, string hasErrorPropertyName)
    {
        bool changed;

        if (message is null)
        {
            changed = _errors.Remove(propertyName);
        }
        else if (_errors.TryGetValue(propertyName, out string? current) && current == message)
        {
            changed = false;
        }
        else
        {
            _errors[propertyName] = message;
            changed = true;
        }

        if (!changed)
        {
            return;
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged(errorPropertyName);
        OnPropertyChanged(hasErrorPropertyName);
        OnPropertyChanged(nameof(HasErrors));
    }
}
