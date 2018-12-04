using System.IO;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable StringLiteralTypo

namespace AdventOfCode
{
    public class Day02 : IPuzzle
    {
        public int Day => 2;

        public object RunPart1(string[] lines)
        {
            //lines = new []
            //{
            //    "abcdef",
            //    "bababc",
            //    "abbcde",
            //    "abcccd",
            //    "aabcdd",
            //    "abcdee",
            //    "ababab",
            //};

            var appearsTwoTimes = 0;
            var appearsThreeTimes = 0;

            foreach (var line in lines)
            {
                var letterCountPerLine = new int[26];
                foreach (var c in line)
                {
                    letterCountPerLine[c - 97]++;
                }

                var incrementTwoCount = false;
                var incrementThreeCount = false;
                foreach (var c in letterCountPerLine)
                {
                    if (c == 2)
                    {
                        incrementTwoCount = true;
                    }

                    if (c == 3)
                    {
                        incrementThreeCount = true;
                    }
                }

                if (incrementTwoCount)
                {
                    appearsTwoTimes++;
                }

                if (incrementThreeCount)
                {
                    appearsThreeTimes++;
                }
            }

            return appearsTwoTimes * appearsThreeTimes;
        }

        public object RunPart2(string[] lines)
        {
            //lines = new []
            //{
            //    "abcde",
            //    "fghij",
            //    "klmno",
            //    "pqrst",
            //    "fguij",
            //    "axcye",
            //    "wvxyz"
            //};

            var lineLength = lines[0].Length;

            for (var outer = 0; outer < lines.Length - 1; outer++)
            {
                var line = lines[outer];
                for (var inner = outer + 1; inner < lines.Length; inner++)
                {
                    var nextLine = lines[inner];
                    var result = new StringBuilder();
                    for (var i = 0; i < line.Length; i++)
                    {
                        var sameLetter = line[i] == nextLine[i];
                        if (sameLetter)
                        {
                            result.Append(line[i]);
                        }
                    }

                    if (result.Length == lineLength - 1)
                    {
                        return result.ToString();
                    }
                }
            }

            return null;
        }
    }
}