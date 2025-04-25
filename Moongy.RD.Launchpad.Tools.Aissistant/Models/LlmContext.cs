
using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Models;

public class LlmContext
{
    public string? Model { get; set; }
    public double? Mode { get; set; }
    public ConversationTreeItem? Root { get; set; }
    public List<LlmMessage> Messages => GetMessages(Root);

    private List<LlmMessage> GetMessages(ConversationTreeItem? root)
    {

        if (root == null || root.Message == null) return [];
        if (root.Branches.Count == 0 || !root.Branches.Any(x => x.IsCurrentBranch)) return [root.Message];
        var nextBranch = root.Branches.FirstOrDefault(x => x.IsCurrentBranch);
        var result = new List<LlmMessage>() { root.Message };
        result.AddRange(nextBranch == null ? [] : GetMessages(nextBranch));
        return result;
    }
}

public class ConversationTreeItem
{
    public LlmMessage? Message { get; set; }
    public List<ConversationTreeItem> Branches { get; set; } = [];
    public bool IsCurrentBranch { get; set; }
}
