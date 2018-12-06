using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day06 : IPuzzle
    {
        public int Day => 6;

        public object RunPart1(string[] input)
        {
            var points = input.Select(Point.FromLine).ToList();
            int gridSize = Math.Max(points.Max(p => p.X), points.Max(p => p.Y)) + 1;
            var grid = new Distance[gridSize, gridSize];

            foreach (var point in points)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        int currentDistance = Math.Abs(point.X - i) + Math.Abs(point.Y - j);
                        if (grid[i, j] == null)
                        {
                            grid[i, j] = new Distance
                            {
                                Value = currentDistance,
                                Point = point
                            };
                        }
                        else if (grid[i, j].Value > currentDistance)
                        {
                            grid[i, j].Point = point;
                            grid[i, j].Value = currentDistance;
                        }
                        else if (grid[i, j].Value == currentDistance)
                        {
                            grid[i, j].Point = null;
                        }
                    }
                }
            }

            for (int i1 = 0; i1 < grid.GetLength(0); i1++)
            {
                grid[i1, 0].Point?.RemoveFrom(points);
                grid[i1, grid.GetLength(1) - 1].Point?.RemoveFrom(points);
                grid[0, i1].Point?.RemoveFrom(points);
                grid[grid.GetLength(0) - 1, i1].Point?.RemoveFrom(points);
            }

            var areaCount = points.ToDictionary(p => p, _ => 0);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var point = grid[i, j].Point;
                    if (point != null && areaCount.ContainsKey(point))
                    {
                        areaCount[point]++;
                    }
                }
            }

            return areaCount.Values.Max();
        }

        public object RunPart2(string[] input)
        {
            var points = input.Select(Point.FromLine).ToList();
            int gridSize = Math.Max(points.Max(p => p.X), points.Max(p => p.Y)) + 1;
            var grid = new int[gridSize, gridSize];

            foreach (var point in points)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        grid[i, j] += Math.Abs(point.X - i) + Math.Abs(point.Y - j);
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] < 10000)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        private class Distance
        {
            public int Value { get; set; }
            public Point Point { get; set; }
        }

        private class Point
        {
            private static readonly Regex LinePattern = new Regex(@"(\d*), (\d*)");

            public int Y { get; private set; }

            public int X { get; private set; }

            public static Point FromLine(string line)
            {
                var match = LinePattern.Match(line);
                var point = new Point
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value)
                };
                return point;
            }

            public void RemoveFrom(ICollection<Point> points)
            {
                points.Remove(this);
            }
        }
    }
}