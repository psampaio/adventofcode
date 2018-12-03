using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day01 : IPuzzle
    {
        public int Day => 1;

        public async Task<int> RunPart1(string input)
        {
            return (await File.ReadAllLinesAsync(input)).Sum(int.Parse);
        }

        public async Task<int> RunPart2(string input)
        {
            var result = 0;
            var results = new List<int> {0};

            var frequencies = (await File.ReadAllLinesAsync(input)).Select(int.Parse).ToList();
            //var frequencies = new[] {1, -1};
            //var frequencies = new[] { 3, 3, 4, -2, -4 };
            //var frequencies = new[] {-6, 3, 8, 5, -6};
            //var frequencies = new [] {7, 7, -2, -7, -4};
            var foundResult = false;
            do
            {
                foreach (var i in frequencies)
                {
                    result += i;
                    if (results.Contains(result))
                    {
                        foundResult = true;
                        break;
                    }

                    results.Add(result);
                }
            } while (!foundResult);

            return result;
        }
    }
}