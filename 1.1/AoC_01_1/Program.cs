using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC_01_1
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
                var result = (await File.ReadAllLinesAsync(args[0])).Sum(int.Parse);
                Console.WriteLine($"Resulting frequency is {result}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}