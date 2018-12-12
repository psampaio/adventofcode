using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day11 : IPuzzle
    {
        public int Day => 11;

        public object RunPart1(string[] input)
        {
            int serialNumber = int.Parse(input[0]);
            var grid = CreateGrid(serialNumber);
            var resultList = CalculateLevel(grid);
            var result = resultList.Single(r => r.Size == 3);
            return $"{result.X},{result.Y}";
        }

        public object RunPart2(string[] input)
        {
            int serialNumber = int.Parse(input[0]);
            var grid = CreateGrid(serialNumber);
            var resultList = CalculateLevel(grid);
            var result = resultList.Aggregate((r1, r2) => r1.Level > r2.Level ? r1 : r2);
            return $"{result.X},{result.Y},{result.Size}";
        }

        private static int[,] CreateGrid(int serialNumber)
        {
            int gridSize = 300;
            var grid = new int[gridSize + 1, gridSize + 1];
            for (int j = 1; j < grid.GetLength(1); j++)
            {
                for (int i = 1; i < grid.GetLength(0); i++)
                {
                    int id = i + 10;
                    int level = id * j;
                    level += serialNumber;
                    level = level * id;
                    level = level < 100 ? 0 : Math.Abs(level / 100 % 10);

                    grid[i, j] = level - 5;
                }
            }

            return grid;
        }

        private static IEnumerable<Result> CalculateLevel(int[,] grid)
        {
            var results = new List<Result>();

            int gridSize = 300;
            for (int size = 0; size < gridSize; size++)
            {
                var result = new Result
                {
                    Size = size + 1,
                    Grid = new int[gridSize - size, gridSize - size]
                };

                int x = int.MinValue;
                int y = int.MinValue;
                int maxLevel = int.MinValue;
                for (int j = 0; j < result.Grid.GetLength(1); j++)
                {
                    for (int i = 0; i < result.Grid.GetLength(0); i++)
                    {
                        try
                        {
                            int level;

                            if (size == 0)
                            {
                                level = grid[i, j];
                            }
                            else
                            {
                                var previousSizeResult = results.ElementAt(size - 1);
                                level = previousSizeResult.Grid[i, j];
                                for (int delta = i; delta < i + size + 1; delta++)
                                {
                                    level += grid[delta, j + size];
                                }

                                for (int delta = j; delta < j + size; delta++)
                                {
                                    level += grid[i + size, delta];
                                }
                            }

                            result.Grid[i, j] = level;
                        
                            if (level > maxLevel)
                            {
                                maxLevel = level;
                                x = i;
                                y = j;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }

                result.Level = maxLevel;
                result.X = x;
                result.Y = y;

                results.Add(result);
            }

            return results;
        }

        private class Result
        {
            public int[,] Grid { get; set; }

            public int Level { get; set; }

            public int Size { get; set; }

            public int X { get; set; }

            public int Y { get; set; }
        }
    }
}