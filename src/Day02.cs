using System.Text;

// ReSharper disable StringLiteralTypo

namespace AdventOfCode
{
    public class Day02 : IPuzzle
    {
        public int Day => 2;

        public object RunPart1(string[] input)
        {
            int appearsTwoTimes = 0;
            int appearsThreeTimes = 0;

            foreach (string line in input)
            {
                var letterCountPerLine = new int[26];
                foreach (char c in line)
                {
                    letterCountPerLine[c - 97]++;
                }

                bool incrementTwoCount = false;
                bool incrementThreeCount = false;
                foreach (int c in letterCountPerLine)
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

        public object RunPart2(string[] input)
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

            int lineLength = input[0].Length;

            for (int outer = 0; outer < input.Length - 1; outer++)
            {
                string line = input[outer];
                for (int inner = outer + 1; inner < input.Length; inner++)
                {
                    string nextLine = input[inner];
                    var result = new StringBuilder();
                    for (int i = 0; i < line.Length; i++)
                    {
                        bool sameLetter = line[i] == nextLine[i];
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