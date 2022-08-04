using System;
using System.Globalization;
using System.IO;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace RocketBlend.Presentation.Avalonia.Converters;

/// <summary>
/// The path to bitmap converter.
/// </summary>
public class PathToBitmapConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        if(value is string rawPath)
        {
            if(string.IsNullOrWhiteSpace(rawPath))
            {
                return null;
            }

            var stream = File.OpenRead(rawPath);
            return Bitmap.DecodeToWidth(stream, 250);
        }

        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}