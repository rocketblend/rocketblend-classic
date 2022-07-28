using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace RocketBlend.Presentation.Avalonia.Views.Controls;

/// <summary>
/// The circular progress bar.
/// </summary>
public partial class CircularProgressBar : UserControl
{
    /// <summary>
    /// The offset.
    /// </summary>
    private const int Offset = 2;

    private bool _isInitialized;
    private double _value;
    private ArcSegment _progressArcSegment;
    private ArcSegment _remainingArcSegment;
    private TextBlock _progressTextBlock;

    /// <summary>
    /// Gets the full radius.
    /// </summary>
    private double FullRadius => this.Radius + this.StrokeThickness;

    public static readonly DirectProperty<CircularProgressBar, double> ValueProperty
        = AvaloniaProperty.RegisterDirect<CircularProgressBar, double>(nameof(Value),
            o => o.Value,
            (o, v) => o.Value = v);

    public static readonly StyledProperty<double> MinValueProperty
        = AvaloniaProperty.Register<CircularProgressBar, double>(nameof(MinValue));

    public static readonly StyledProperty<double> MaxValueProperty
        = AvaloniaProperty.Register<CircularProgressBar, double>(nameof(MaxValue));

    public static readonly StyledProperty<double> RadiusProperty
        = AvaloniaProperty.Register<CircularProgressBar, double>(nameof(Radius));

    public static readonly StyledProperty<double> StrokeThicknessProperty
        = AvaloniaProperty.Register<CircularProgressBar, double>(nameof(StrokeThickness));

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public double Value
    {
        get => this._value;
        set
        {
            if (this.SetAndRaise(ValueProperty, ref this._value, value))
            {
                this.Refresh();
            }
        }
    }

    /// <summary>
    /// Gets or sets the min value.
    /// </summary>
    public double MinValue
    {
        get => this.GetValue(MinValueProperty);
        set => this.SetValue(MinValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the max value.
    /// </summary>
    public double MaxValue
    {
        get => this.GetValue(MaxValueProperty);
        set => this.SetValue(MaxValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the radius.
    /// </summary>
    public double Radius
    {
        get => this.GetValue(RadiusProperty);
        set => this.SetValue(RadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke thickness.
    /// </summary>
    public double StrokeThickness
    {
        get => this.GetValue(StrokeThicknessProperty);
        set => this.SetValue(StrokeThicknessProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CircularProgressBar"/> class.
    /// </summary>
    public CircularProgressBar()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Ons the apply template.
    /// </summary>
    /// <param name="e">The e.</param>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        this._progressTextBlock = this.FindControl<TextBlock>("ProgressTextBlock");

        var progressPath = this.FindControl<Path>("ProgressPath");
        var remainingPath = this.FindControl<Path>("RemainingPath");

        this.Width = this.Height = remainingPath.Width =
            remainingPath.Height = progressPath.Width = progressPath.Height = 2 * (this.FullRadius + Offset);
        remainingPath.StrokeThickness = progressPath.StrokeThickness = this.StrokeThickness;

        var progressPathFigure = ((PathGeometry)progressPath.Data).Figures.Single();
        var remainingPathFigure = ((PathGeometry)remainingPath.Data).Figures.Single();
        progressPathFigure.StartPoint = remainingPathFigure.StartPoint = new Point(Offset + this.FullRadius, Offset);
        if (progressPathFigure.Segments is null || remainingPathFigure.Segments is null)
        {
            return;
        }

        this._progressArcSegment = (ArcSegment)progressPathFigure.Segments.Single();
        this._remainingArcSegment = (ArcSegment)remainingPathFigure.Segments.Single();
        this._progressArcSegment.Size = this._remainingArcSegment.Size = new Size(this.FullRadius, this.FullRadius);

        this._isInitialized = true;

        this.Refresh();
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

    /// <summary>
    /// Refreshes the.
    /// </summary>
    private void Refresh()
    {
        if (!this._isInitialized)
        {
            return;
        }

        var angleInRadians = 2 * Math.PI * (this.Value - this.MinValue) / (this.MaxValue - this.MinValue);
        var x = Offset + this.FullRadius * (1 + Math.Sin(angleInRadians));
        var y = Offset + this.FullRadius * (1 - Math.Cos(angleInRadians));
        var point = new Point(x, y);

        this._remainingArcSegment.Point = this._progressArcSegment.Point = point;
        var isLargeArc = angleInRadians > Math.PI;
        this._progressArcSegment.IsLargeArc = isLargeArc;
        this._remainingArcSegment.IsLargeArc = !isLargeArc;
        this._progressTextBlock.Text = ((int)this.Value).ToString();
    }
}