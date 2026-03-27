using System.Globalization;

namespace SpecialOfferApp.Converters;

public sealed class EnumEqualsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || parameter is null)
            return false;

        var valueString = value.ToString();
        var parameterString = parameter.ToString();
        return string.Equals(valueString, parameterString, StringComparison.Ordinal);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is null)
            throw new InvalidOperationException("ConverterParameter is required.");

        if (value is bool b && b)
        {
            var parameterString = parameter.ToString();
            if (parameterString is null)
                throw new InvalidOperationException("ConverterParameter is required.");

            return Enum.Parse(targetType, parameterString);
        }

        return Binding.DoNothing;
    }
}

