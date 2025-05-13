using System;
using System.Collections.Generic;

namespace ScribanSolidityColorizer.Helpers
{
    internal static class ControlBlockFinder
    {
        public static IEnumerable<(int start, int length)> Find(string text, string open, string close)
        {
            var results = new List<(int, int)>();
            var index = 0;
            while ((index = text.IndexOf(open, index, StringComparison.Ordinal)) != -1)
            {
                var end = text.IndexOf(close, index + open.Length, StringComparison.Ordinal);
                if (end > index)
                {
                    results.Add((index, end - index + close.Length));
                    index = end + close.Length;
                }
                else
                {
                    break;
                }
            }
            return results;
        }
    }
}
