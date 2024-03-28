namespace RueI.Elements;

using PluginAPI.Core;
using RueI.Displays;
using RueI.Elements.Delegates;
using RueI.Extensions.HintBuilding;
using RueI.Parsing;
using RueI.Parsing.Records;
using System.Text;

/// <summary>
/// Represents a non-cached element that evaluates and parses a function when getting its content.
/// </summary>
/// <remarks>
/// The content of this element is re-evaluated by calling a function every time the display is updated. This makes it suitable for scenarios where you need to have information constantly updated. For example, you may use this to display the health of SCPs in an SCP list.
/// </remarks>
public class DynamicHeightPlayerElement : Element
{

    public int Size = 24;
    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicHeightPlayerElement"/> class.
    /// </summary>
    /// <param name="contentGetter">A delegate returning the new content that will be ran every time the display is updated.</param>
    /// <param name="position">The scaled position of the element, where 0 is the bottom of the screen and 1000 is the top.</param>
    public DynamicHeightPlayerElement(GetPlayerContent contentGetter, int size=24)
        : base(1000)
    {
        ContentGetter = GetContent;
        StaticContentGetter = contentGetter;
        Size = size;
    }

    /// <summary>
    /// Gets or sets a method that returns the new content and is called every time the display is updated.
    /// </summary>
    public GetPlayerContent StaticContentGetter { get; set; }
    public GetPlayerContent ContentGetter { get; set; }

    public virtual string GetContent(Player player)
    {
        StringBuilder content = new StringBuilder();
        content.SetSize(Size);

        string elemText = StaticContentGetter(player);

        if (string.IsNullOrEmpty(elemText)) return "";

        elemText = EnsureEnding(elemText);

        elemText = GetCorrectLineHeight(elemText, Size);

        content.Append(elemText);

        content.CloseSize();
        return content.ToString();
    }
    

    /// <inheritdoc/>
    protected internal override ParsedData GetParsedData(DisplayCore core) => Parser.Parse(ContentGetter(Player.Get(core.Hub)), Options);
}