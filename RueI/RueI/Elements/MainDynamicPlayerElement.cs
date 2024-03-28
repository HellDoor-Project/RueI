namespace RueI.Elements;

using RueI.Elements.Delegates;
using PluginAPI.Core;
using RueI.Displays;
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
public class MainDynamicPlayerElement : Element
{
    public Dictionary<GetPlayerContent, int> ElementPriority = new Dictionary<GetPlayerContent, int>();
    public List<GetPlayerContent> Elements = new List<GetPlayerContent>();
    /// <summary>
    /// Initializes a new instance of the <see cref="MainDynamicPlayerElement"/> class.
    /// </summary>
    /// <param name="contentGetter">A delegate returning the new content that will be ran every time the display is updated.</param>
    /// <param name="position">The scaled position of the element, where 0 is the bottom of the screen and 1000 is the top.</param>
    public MainDynamicPlayerElement()
        : base(1000)
    {
        ContentGetter = GetContent;
    }
    public bool HasElement(GetPlayerContent element)
    {
        return Elements.Contains(element);
    }

    public void AddElement(GetPlayerContent element, int priority)
    {
        ElementPriority[element] = priority;
        Elements.Add(element);
        Elements.Sort(delegate (
            GetPlayerContent content1,
            GetPlayerContent content2)
        {
            return ElementPriority[content2].CompareTo(ElementPriority[content1]);
        });
    }
    public void RemoveElement(GetPlayerContent element)
    {
        Elements.Remove(element);
    }

    /// <summary>
    /// Gets or sets a method that returns the new content and is called every time the display is updated.
    /// </summary>
    public GetPlayerContent ContentGetter { get; set; }

    public string GetContent(Player player)
    {
        StringBuilder content = new StringBuilder();
        content.SetSize(27);
        if (!DisplayCore.PlayerOffset.TryGetValue(player.UserId, out float offset))
        {
            offset = 60;
        }

        content.SetMargins(offset);
        content.SetAlignment(HintBuilding.AlignStyle.Left);

        string elemsTexts = string.Empty;

        foreach (GetPlayerContent element in Elements)
        {
            string elemText = element(player);

            if (string.IsNullOrEmpty(elemText)) continue;

            elemText = EnsureEnding(elemText);

            elemsTexts += elemText;
        }

        if (string.IsNullOrEmpty(elemsTexts)) return "";

        elemsTexts = GetCorrectLineHeight(elemsTexts);

        content.Append(elemsTexts);

        content.CloseSize();
        return content.ToString();
    }

    /// <inheritdoc/>
    protected internal override ParsedData GetParsedData(DisplayCore core) => Parser.Parse(ContentGetter(Player.Get(core.Hub)), Options);
}