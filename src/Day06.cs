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
//            input = new[]
//            {
//                "1, 1",
//                "1, 6",
//                "8, 3",
//                "3, 4",
//                "5, 5",
//                "8, 9"
//            };

            var points = input.Select(Point.FromLine).ToList();
            int gridSize = Math.Max(points.Max(p => p.X), points.Max(p => p.Y)) +1;
            var grid = new Distance[gridSize, gridSize];

            foreach (var point in points)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        int deltaX = Math.Abs(point.X - i);
                        int deltaY = Math.Abs(point.Y - j);
                        int currentDistance = deltaX + deltaY;
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

            foreach (var invalidPoint in GetPointsWithInfiniteArea(grid))
            {
                points.Remove(invalidPoint);
            }

            var areaCount = new Dictionary<Point, int>();
            foreach (var point in points)
            {
                areaCount.Add(point, 0);
            }
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
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

        private static IEnumerable<Point> GetPointsWithInfiniteArea(Distance[,] grid)
        {
            var pointsWithInfiniteArea = new List<Point>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var point = grid[i, 0].Point;
                if (point == null)
                {
                    continue;
                }

                if (!pointsWithInfiniteArea.Contains(point))
                {
                    pointsWithInfiniteArea.Add(point);
                }

                point = grid[i, grid.GetLength(0) - 1].Point;
                if (point == null)
                {
                    continue;
                }

                if (!pointsWithInfiniteArea.Contains(point))
                {
                    pointsWithInfiniteArea.Add(point);
                }
            }

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var point = grid[0, i].Point;
                if (point == null)
                {
                    continue;
                }

                if (!pointsWithInfiniteArea.Contains(point))
                {
                    pointsWithInfiniteArea.Add(point);
                }

                point = grid[grid.GetLength(0) - 1, i].Point;
                if (point == null)
                {
                    continue;
                }

                if (!pointsWithInfiniteArea.Contains(point))
                {
                    pointsWithInfiniteArea.Add(point);
                }
            }

            return pointsWithInfiniteArea;
        }

        public object RunPart2(string[] input)
        {
            return null;
        }

        private class Distance
        {
            public int Value { get; set; }
            public Point Point { get; set; }
        }

        private class Point
        {
            private static readonly Regex linePattern = new Regex(@"(\d*), (\d*)");

            public int Y { get; private set; }

            public int X { get; private set; }

            public static Point FromLine(string line)
            {
                var match = linePattern.Match(line);
                var point = new Point
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value)
                };
                return point;
            }
        }
    }
}