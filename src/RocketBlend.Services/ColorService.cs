using System.Drawing;
using RocketBlend.Services.Abstractions;

namespace RocketBlend.Services;

/// <summary>
/// The color service.
/// </summary>
public class ColorService : IColorService
{
    private readonly Random _rnd = new();

    /// <inheritdoc />
    public string RandomColor()
    {
        return ColorTranslator.ToHtml(Color.FromArgb(this._rnd.Next(256), this._rnd.Next(256), this._rnd.Next(256))).ToString();
    }
}
