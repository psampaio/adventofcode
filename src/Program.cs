using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lamar;

namespace AdventOfCode
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Loading Puzzles!");
            
            var container = new Container(x =>
            {
                x.Scan(s =>
                {
                    s.AssembliesFromApplicationBaseDirectory();
                    s.AddAllTypesOf(typeof(IPuzzle));
                });
            });

            var inputDir = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "input"));

            var puzzles = container.GetAllInstances<IPuzzle>().OrderBy(p => p.Day).ToList();
            Console.WriteLine($"Found puzzles until day {puzzles.Count}.");
            bool repeat;
            do
            {
                Console.Write("Please type the day or 'e' to exit: ");
                var line = Console.ReadLine();
                if (int.TryParse(line, out var dayNumber) && dayNumber > 0 && dayNumber <= puzzles.Count)
                {
                    var puzzle = puzzles.Single(p => p.Day == dayNumber);
                    var fileName = Path.Combine(inputDir, $"day{dayNumber}.txt");
                    if (!File.Exists(fileName))
                    {
                        Console.WriteLine($"Input file not found ({fileName})");
                    }
                    else
                    {
                        Console.WriteLine($"Running puzzle for day {dayNumber}");
                        var lines = await File.ReadAllLinesAsync(fileName);
                        Console.WriteLine($"\tPart 1 result: {puzzle.RunPart1(lines)}");
                        Console.WriteLine($"\tPart 2 result: {puzzle.RunPart2(lines)}");
                    }
                    repeat = true;
                }
                else if (line == "e")
                {
                    repeat = false;
                }
                else
                {
                    repeat = true;
                }

            } while (repeat);
        }
    }
}
