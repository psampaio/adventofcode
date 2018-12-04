using System.IO;
using System.Threading.Tasks;

// ReSharper disable StringLiteralTypo

namespace AdventOfCode
{
    public class Day02 : IPuzzle
    {
        public int Day => 2;

        public async Task<int> RunPart1(string input)
        {
            var lines = await File.ReadAllLinesAsync(input);
            //var lines = new[]
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

        public Task<int> RunPart2(string input)
        {
            return Task.FromResult(0);
        }
    }
}