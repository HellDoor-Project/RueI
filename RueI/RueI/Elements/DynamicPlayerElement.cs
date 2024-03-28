namespace RueI.Elements;

using PluginAPI.Core;
using RueI.Displays;
using RueI.Elements.Delegates;
using RueI.Parsing;
using RueI.Parsing.Records;

/// <summary>
/// Represents a non-cached element that evaluates and parses a function when getting its content.
/// </summary>
/// <remarks>
/// The content of this element is re-evaluated by calling a function every time the display is updated. This makes it suitable for scenarios where you need to have information constantly updated. For example, you may use this to display the health of SCPs in an SCP list.
/// </remarks>
public class DynamicPlayerElement : Element
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicPlayerElement"/> class.
    /// </summary>
    /// <param name="contentGetter">A delegate returning the new content that will be ran every time the display is updated.</param>
    /// <param name="position">The scaled position of the element, where 0 is the bottom of the screen and 1000 is the top.</param>
    public DynamicPlayerElement(GetPlayerContent contentGetter, float position)
        : base(position)
    {
        ContentGetter = contentGetter;
    }

    /// <summary>
    /// Gets or sets a method that returns the new content and is called every time the display is updated.
    /// </summary>
    public GetPlayerContent ContentGetter { get; set; }

    /// <inheritdoc/>
    protected internal override ParsedData GetParsedData(DisplayCore core) => Parser.Parse(ContentGetter(Player.Get(core.Hub)), Options);
}