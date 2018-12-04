using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day04 : IPuzzle
    {
        private readonly Regex guardIdPattern = new Regex(@"#(\d*)");
        private readonly Regex timePattern = new Regex(@"\[(\d*)-(\d*)-(\d*) (\d*):(\d*)\] (.*)$");

        public int Day => 4;

        public object RunPart1(string[] input)
        {
//            input = new[]
//            {
//                "[1518-11-01 00:00] Guard #10 begins shift",
//                "[1518-11-01 00:05] falls asleep",
//                "[1518-11-01 00:25] wakes up",
//                "[1518-11-01 00:30] falls asleep",
//                "[1518-11-01 00:55] wakes up",
//                "[1518-11-01 23:58] Guard #99 begins shift",
//                "[1518-11-02 00:40] falls asleep",
//                "[1518-11-02 00:50] wakes up",
//                "[1518-11-03 00:05] Guard #10 begins shift",
//                "[1518-11-03 00:24] falls asleep",
//                "[1518-11-03 00:29] wakes up",
//                "[1518-11-04 00:02] Guard #99 begins shift",
//                "[1518-11-04 00:36] falls asleep",
//                "[1518-11-04 00:46] wakes up",
//                "[1518-11-05 00:03] Guard #99 begins shift",
//                "[1518-11-05 00:45] falls asleep",
//                "[1518-11-05 00:55] wakes up"
//            };

            Array.Sort(input, StringComparer.InvariantCulture);

            var guards = GetGuardSummary(input);
            var guard = guards.Values.OrderByDescending(g => g.Sleep.Sum()).First();
            int maxSleepMinute = CalculateMaxSleepMinute(guard);
            return guard.Id * maxSleepMinute;
        }

        public object RunPart2(string[] input)
        {
//            input = new[]
//            {
//                "[1518-11-01 00:00] Guard #10 begins shift",
//                "[1518-11-01 00:05] falls asleep",
//                "[1518-11-01 00:25] wakes up",
//                "[1518-11-01 00:30] falls asleep",
//                "[1518-11-01 00:55] wakes up",
//                "[1518-11-01 23:58] Guard #99 begins shift",
//                "[1518-11-02 00:40] falls asleep",
//                "[1518-11-02 00:50] wakes up",
//                "[1518-11-03 00:05] Guard #10 begins shift",
//                "[1518-11-03 00:24] falls asleep",
//                "[1518-11-03 00:29] wakes up",
//                "[1518-11-04 00:02] Guard #99 begins shift",
//                "[1518-11-04 00:36] falls asleep",
//                "[1518-11-04 00:46] wakes up",
//                "[1518-11-05 00:03] Guard #99 begins shift",
//                "[1518-11-05 00:45] falls asleep",
//                "[1518-11-05 00:55] wakes up"
//            };

            Array.Sort(input, StringComparer.InvariantCulture);

            var guards = GetGuardSummary(input);
            var guard = guards.Values.OrderByDescending(g => g.Sleep.Max()).First();
            int maxSleepMinute = CalculateMaxSleepMinute(guard);

            return guard.Id * maxSleepMinute;
        }

        private static int CalculateMaxSleepMinute(Guard guard)
        {
            int maxSleep = 0;
            int maxSleepMinute = -1;
            for (int i = 0; i < guard.Sleep.Length; i++)
            {
                if (guard.Sleep[i] <= maxSleep)
                {
                    continue;
                }

                maxSleep = guard.Sleep[i];
                maxSleepMinute = i;
            }

            return maxSleepMinute;
        }

        private Dictionary<int, Guard> GetGuardSummary(IEnumerable<string> input)
        {
            var guards = new Dictionary<int, Guard>();
            Guard guard = null;

            foreach (string line in input)
            {
                var timeMatch = timePattern.Match(line);

                int hour = int.Parse(timeMatch.Groups[5].Value);
                string action = timeMatch.Groups[6].Value;

                char c = action[0];
                if (c == 'G')
                {
                    var guardIdMatch = guardIdPattern.Match(action);

                    int guardId = int.Parse(guardIdMatch.Groups[1].Value);
                    if (guards.TryGetValue(guardId, out guard))
                    {
                        continue;
                    }

                    guard = new Guard
                    {
                        Id = guardId
                    };
                    guards.Add(guardId, guard);
                }
                else if (c == 'f')
                {
                    for (int i = hour; i < guard.Sleep.Length; i++)
                    {
                        guard.Sleep[i]++;
                    }
                }
                else if (c == 'w')
                {
                    for (int i = hour; i < guard.Sleep.Length; i++)
                    {
                        guard.Sleep[i]--;
                    }
                }
            }

            return guards;
        }


        private class Guard
        {
            public Guard()
            {
                Sleep = new int[60];
            }

            public int Id { get; set; }
            public int[] Sleep { get; }
        }
    }
}