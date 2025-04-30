using System.Collections.Generic;

namespace ScribanSolidityColorizer.Helpers
{
    internal static class NumberLiteralFinder
    {
      
         public static IEnumerable<(int start, int length)> Find(string text)
        {
            var results = new List<(int, int)>();
            int position = 0;

            while (position < text.Length)
            {
                if (char.IsDigit(text[position]))
                {
                    int start = position;
                    bool hasUnderscore = false;
                    bool hasExponent = false;

                    position++;
                    while (position < text.Length)
                    {
                        var c = text[position];
                        if (char.IsDigit(c) || (c == '_' && !hasExponent))
                        {
                            if (c == '_') hasUnderscore = true;
                            position++;
                        }
                        else if ((c == 'e' || c == 'E') && !hasExponent)
                        {
                            hasExponent = true;
                            position++;
                            if (position < text.Length &&
                                (text[position] == '+' || text[position] == '-'))
                                position++;
                        }
                        else break;
                    }

                    int length = position - start;
                    if (IsStandalone(text, start, length))
                        results.Add((start, length));
                }
                else if (position + 1 < text.Length &&
                         text[position] == '0' &&
                         (text[position + 1] == 'x' || text[position + 1] == 'X'))
                {
                    int start = position;
                    position += 2;
                    while (position < text.Length &&
                           (char.IsDigit(text[position]) ||
                            (text[position] >= 'a' && text[position] <= 'f') ||
                            (text[position] >= 'A' && text[position] <= 'F')))
                    {
                        position++;
                    }

                    int length = position - start;
                    if (IsStandalone(text, start, length))
                        results.Add((start, length));
                }
                else
                {
                    position++;
                }
            }

            return results;
        }

        /// <summary>
        /// Returns true if the span [start..start+length) is not directly adjacent to any letter.
        /// </summary>
        private static bool IsStandalone(string text, int start, int length)
        {
            bool leftOk = start == 0 || !char.IsLetter(text[start - 1]);
            bool rightOk = (start + length >= text.Length) || !char.IsLetter(text[start + length]);
            return leftOk && rightOk;
        }
    }
}
