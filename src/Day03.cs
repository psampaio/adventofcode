using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day03 : IPuzzle
    {
        public int Day => 3;

        public object RunPart1(string[] lines)
        {
            //lines = new []
            //{
            //    "#1 @ 1,3: 4x4",
            //    "#2 @ 3,1: 4x4",
            //    "#3 @ 5,5: 2x2"
            //};

            int fabricSize = 1000;
            var fabric = new int[fabricSize, fabricSize];
            var regex = new Regex(@"#(\d*) @ (\d*),(\d*): (\d*)x(\d*)");
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                var id = int.Parse(match.Groups[1].Value);
                var left = int.Parse(match.Groups[2].Value);
                var top = int.Parse(match.Groups[3].Value);
                var width = int.Parse(match.Groups[4].Value);
                var height = int.Parse(match.Groups[5].Value);

                for (int i = left; i < left + width; i++)
                {
                    for (int j = top; j < top + height; j++)
                    {
                        fabric[i, j]++;
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < fabricSize; i++)
            {
                for (int j = 0; j < fabricSize; j++)
                {
                    if (fabric[i, j] > 1)
                    {
                        sum++;
                    }
                }
            }
            
            return sum;
        }

        public object RunPart2(string[] lines)
        {
            return null;
        }
    }
}