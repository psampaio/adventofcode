using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC_01_2
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Loading input");

            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Input file not found (${fileName})");
            }
            else
            {
                var result = 0;
                var results = new List<int> {0};

                var frequencies = (await File.ReadAllLinesAsync(args[0])).Select(int.Parse).ToList();
                //var frequencies = new[] {1, -1};
                //var frequencies = new[] { 3, 3, 4, -2, -4 };
                //var frequencies = new[] {-6, 3, 8, 5, -6};
                //var frequencies = new [] {7, 7, -2, -7, -4};
                bool foundResult = false;
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

                Console.WriteLine($"Resulting frequency is {result}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}