namespace RueI.Elements;

using RueI.Elements.Enums;
using RueI.Parsing;
using RueI.Parsing.Records;

/// <summary>
/// Represents the base interface for all elements.
/// </summary>
public abstract class Element
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Element"/> class.
    /// </summary>
    /// <param name="position">The position of the element, where 0 represents the bottom of the screen and 1000 represents the top.</param>
    public Element(float position)
    {
        Position = position;
        ParsedData = new(string.Empty, 0);
    }

    /// <summary>
    /// Gets or sets a value indicating whether or not this element is enabled and will show.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the position of the element on a scale from 0-1000, where 0 represents the bottom of the screen and 1000 represents the top.
    /// </summary>
    public float Position { get; set; }

    /// <summary>
    /// Gets or sets the priority of the hint (determining if it shows above another hint).
    /// </summary>
    public int ZIndex { get; set; } = 1;

    /// <summary>
    /// Gets or sets the <see cref="Parser"/> currently in use by this <see cref="Element"/>.
    /// </summary>
    /// <remarks>Implementations should default this to <see cref="Parser.DefaultParser"/>.</remarks>
    public Parser Parser { get; set; } = Parser.DefaultParser;

    /// <summary>
    /// Gets or sets the options for this element.
    /// </summary>
    public ElementOptions Options { get; set; } = ElementOptions.Default;

    /// <summary>
    /// Gets or sets the data used for parsing.
    /// </summary>
    /// <remarks>This contains information used to ensure that multiple elements can be displayed at once. To obtain this, you should almost always use <see cref="Parser.Parse"/>.</remarks>
    protected internal virtual ParsedData ParsedData { get; protected set; }
}