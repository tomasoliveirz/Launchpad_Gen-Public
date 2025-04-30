using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribanSolidityColorizer.Helpers
{
    internal static class CommentFinder
    {
        public static IEnumerable<(int start, int length)> Find(string text)
        {
            var results = new List<(int, int)>();
            int position = 0;

            while (position < text.Length)
            {
                if (position + 1 < text.Length && text[position] == '/' && text[position + 1] == '/')
                {
                    int start = position;
                    position += 2;

                    while (position < text.Length && text[position] != '\n')
                    {
                        position++;
                    }

                    results.Add((start, position - start));
                }
                else if (position + 1 < text.Length && text[position] == '/' && text[position + 1] == '*')
                {
                    int start = position;
                    position += 2;

                    while (position + 1 < text.Length)
                    {
                        if (text[position] == '*' && text[position + 1] == '/')
                        {
                            position += 2;
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
