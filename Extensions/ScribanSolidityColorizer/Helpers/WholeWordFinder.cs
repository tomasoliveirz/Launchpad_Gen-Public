using System.Collections.Generic;
using System;

namespace ScribanSolidityColorizer.Helpers
{
    internal static class WholeWordFinder
    {
        public static IEnumerable<int> Find(string text, string word)
        {
            var matches = new List<int>();
            var index = 0;
            while ((index = text.IndexOf(word, index, StringComparison.Ordinal)) != -1)
            {
                bool isStartBoundary = (index == 0) || !char.IsLetterOrDigit(text[index - 1]);
                bool isEndBoundary = (index + word.Length >= text.Length) || !char.IsLetterOrDigit(text[index + word.Length]);

                if (isStartBoundary && isEndBoundary)
                    matches.Add(index);

                index += word.Length;
            }
            return matches;
        }
    }
}
