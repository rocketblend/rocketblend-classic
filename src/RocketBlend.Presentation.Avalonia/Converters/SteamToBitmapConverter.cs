using System;
using System.Globalization;
using System.IO;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace RocketBlend.Presentation.Avalonia.Converters;

/// <summary>
/// The steam to bitmap converter.
/// </summary>
public class StreamToBitmapConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        if(value is Stream rawStream)
        {
            if(rawStream is null)
            {
                return null;
            }

            return Bitmap.DecodeToWidth(rawStream, 250);
        }

        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}