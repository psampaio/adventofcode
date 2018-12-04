using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day01 : IPuzzle
    {
        public int Day => 1;

        public object RunPart1(string[] input)
        {
            return input.Sum(int.Parse);
        }

        public object RunPart2(string[] input)
        {
            int result = 0;
            var results = new Dictionary<int, bool>();

            var frequencies = input.Select(int.Parse).ToList();
            //var frequencies = new[] {1, -1};
            //var frequencies = new[] { 3, 3, 4, -2, -4 };
            //var frequencies = new[] {-6, 3, 8, 5, -6};
            //var frequencies = new [] {7, 7, -2, -7, -4};
            bool foundResult = false;
            do
            {
                foreach (int i in frequencies)
                {
                    result += i;
                    if (results.TryAdd(result, true))
                    {
                        continue;
                    }

                    foundResult = true;
                    break;
                }
            } while (!foundResult);

            return result;
        }
    }
}