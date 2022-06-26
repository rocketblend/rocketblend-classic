using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace RocketBlend.Presentation.Avalonia.Converters;

/// <summary>
/// The inverse enum to boolean converter.
/// </summary>
public class InverseEnumToBooleanConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var actual = System.Convert.ToInt32(value);
        var expected = System.Convert.ToInt32(parameter);

        return actual != expected;
    }

    /// <inheritdoc />
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}