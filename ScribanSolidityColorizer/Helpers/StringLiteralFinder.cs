using System.Collections.Generic;
namespace ScribanSolidityColorizer.Helpers
{
    internal static class StringLiteralFinder
    {
        public static IEnumerable<(int start, int length)> Find(string text)
        {
            var results = new List<(int, int)>();
            int position = 0;
            while (position < text.Length)
            {
                if (text[position] == '"')
                {
                    int start = position++;
                    while (position < text.Length)
                    {
                        if (text[position] == '"' && text[position - 1] != '\\')
                        {
                            position++;
                            break;
                        }
                        position++;
                    }
                    results.Add((start, position - start));
                }
                else
                {
                    position++;
                }
            }
            return results;
        }
    }
}
