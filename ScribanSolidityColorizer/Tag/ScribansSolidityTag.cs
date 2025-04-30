using Microsoft.VisualStudio.Text.Tagging;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Tag
{
    internal class ScribanSolidityTag : ITag
    {
        public ScribanSolidityTokenTypes type { get; private set; }

        public ScribanSolidityTag(ScribanSolidityTokenTypes type)
        {
            this.type = type;
        }
    }
}
