namespace RueI.Displays;

using RueI.Extensions;
using RueI.Elements;
using RueI.Displays.Interfaces;

/// <summary>
/// Represents a basic display attached to a <see cref="DisplayCore"/>.
/// </summary>
/// <include file='docs.xml' path='docs/displays/members[@name="display"]/Display/*'/>
public class Display : DisplayBase, IElementContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Display"/> class.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> to assign the display to.</param>
    public Display(ReferenceHub hub, string id=null)
        : base(hub)
    {
        DisplayId = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Display"/> class.
    /// </summary>
    /// <param name="coordinator">The <see cref="DisplayCore"/> to assign the display to.</param>
    public Display(DisplayCore coordinator, string id=null)
        : base(coordinator)
    {
        DisplayId = id;
    }

    /// <summary>
    /// Gets the elements of this display.
    /// </summary>
    public List<Element> Elements { get; } = new();

    public string? DisplayId { get; set; } = null;

    /// <inheritdoc/>
    public override IEnumerable<Element> GetAllElements() => Elements.FilterDisabled();
}