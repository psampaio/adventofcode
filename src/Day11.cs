using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day11 : IPuzzle
    {
        private const int GridSize = 300;
        private static int[][] cachedGrid;
        private static IEnumerable<Result> cachedResults;

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

        private static int[][] CreateGrid(int serialNumber)
        {
            if (cachedGrid != null)
            {
                return cachedGrid;
            }

            cachedGrid = new int[GridSize][];
            for (int j = 0; j < GridSize; j++)
            {
                var subGrid = new int[GridSize];
                for (int i = 0; i < GridSize; i++)
                {
                    int id = i + 1 + 10;
                    int level = id * (j + 1);
                    level += serialNumber;
                    level = level * id;
                    level = level < 100 ? 0 : Math.Abs(level / 100 % 10);

                    subGrid[i] = level - 5;
                }

                cachedGrid[j] = subGrid;
            }

            return cachedGrid;
        }

        private static IEnumerable<Result> CalculateLevel(int[][] grid)
        {
            if (cachedResults != null)
            {
                return cachedResults;
            }

            var results = new List<Result>();

            Result previousSizeResult = null;
            for (int size = 0; size < GridSize; size++)
            {
                int iLength = GridSize - size;
                int jLength = GridSize - size;
                var result = new Result
                {
                    Size = size + 1,
                    Grid = new int[jLength][]
                };

                int x = int.MinValue;
                int y = int.MinValue;
                int maxLevel = int.MinValue;
                for (int j = 0; j < jLength; j++)
                {
                    var subGrid = new int[iLength];
                    for (int i = 0; i < iLength; i++)
                    {
                        int level;

                        if (previousSizeResult == null)
                        {
                            level = grid[j][i];
                        }
                        else
                        {
                            level = previousSizeResult.Grid[j][i];
                            for (int delta = i; delta < i + size + 1; delta++)
                            {
                                level += grid[j + size][delta];
                            }

                            for (int delta = j; delta < j + size; delta++)
                            {
                                level += grid[delta][i + size];
                            }
                        }

                        subGrid[i] = level;
                        if (level > maxLevel)
                        {
                            maxLevel = level;
                            x = i + 1;
                            y = j + 1;
                        }
                    }

                    result.Grid[j] = subGrid;
                }

                result.Level = maxLevel;
                result.X = x;
                result.Y = y;

                results.Add(result);
                previousSizeResult = result;
            }

            cachedResults = results;
            return results;
        }

        private class Result
        {
            public int[][] Grid { get; set; }

            public int Level { get; set; }

            public int Size { get; set; }

            public int X { get; set; }

            public int Y { get; set; }
        }
    }
}