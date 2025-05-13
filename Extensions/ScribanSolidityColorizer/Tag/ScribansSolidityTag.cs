using Microsoft.VisualStudio.Text.Tagging;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Tag
{
    internal class ScribanSolidityTag : ITag
    {
        public ScribanSolidityTokenTypes Type { get; private set; }

        public ScribanSolidityTag(ScribanSolidityTokenTypes type)
        {
            Type = type;
        }
    }
}
