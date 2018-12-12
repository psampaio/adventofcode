using System;

namespace AdventOfCode
{
    public class Day11 : IPuzzle
    {
        public int Day => 11;

        public object RunPart1(string[] input)
        {
            int serialNumber = int.Parse(input[0]);

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

            int x = 0;
            int y = 0;
            int maxLevel = int.MinValue;
            for (int j = 1; j < grid.GetLength(1) - 2; j++)
            {
                for (int i = 1; i < grid.GetLength(0) - 2; i++)
                {
                    int level = grid[i, j] + grid[i + 1, j] + grid[i + 2, j] +
                                grid[i, j + 1] + grid[i + 1, j + 1] + grid[i + 2, j + 1] +
                                grid[i, j + 2] + grid[i + 1, j + 2] + grid[i + 2, j + 2];

                    if (level > maxLevel)
                    {
                        maxLevel = level;
                        x = i;
                        y = j;
                    }
                }
            }

            return $"{x},{y}";
        }

        public object RunPart2(string[] input)
        {
            return null;
        }
    }
}