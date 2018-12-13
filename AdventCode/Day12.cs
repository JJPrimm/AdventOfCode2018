using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day12
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(12);
            var rules = input.Select(i => new Rule
            {
                Pattern = i.Substring(0, 5),
                Result =  i.Substring(9, 1)
            });

            string state = "#...#####.#..##...##...#.##.#.##.###..##.##.#.#..#...###..####.#.....#..##..#.##......#####..####...";
            int minPosition = 0;

            for (int generation = 1; generation <= 50000; generation++)
            {
                while (!state.StartsWith("...."))
                {
                    state = "." + state;
                    minPosition--;
                }
                while (!state.EndsWith("...."))
                {
                    state = state + ".";
                }
                var newState = "..";

                for (int index = 0; index < state.Length - 4; index++)
                {
                    string pattern = state.Substring(index, 5);

                    newState = newState + rules.Single(r => r.Pattern == pattern).Result;
                }
                state = newState;
            }
            int result = 0;
            for (int index = 0; index < state.Length; index++)
            {
                result += (state[index] == '#') ? index + minPosition : 0;
            }
            Console.WriteLine($"Problem 12-1: {result}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(12);
            var rules = input.Select(i => new Rule
            {
                Pattern = i.Substring(0, 5),
                Result = i.Substring(9, 1)
            });

            StringBuilder state = new StringBuilder("#...#####.#..##...##...#.##.#.##.###..##.##.#.#..#...###..####.#.....#..##..#.##......#####..####...");
            int minPosition = 0;

            for (double generation = 1; generation <= 50000000000; generation++)
            {
                while (state.ToString(0, 5) != "....#")
                {
                    if (state.ToString(0, 4) != "....")
                    {
                        state.Insert(0, '.');
                        minPosition--;
                    }
                    else
                    {
                        state.Remove(0, 1);
                        minPosition++;
                    }
                }
                while (state.ToString(state.Length - 6, 5) != "#....")
                {
                    if (state.ToString(state.Length - 5, 4) != "....")
                    {
                        state.Append('.');
                    }
                    else
                    {
                        state.Remove(state.Length - 1, 1);
                    }
                }
                var newState = new StringBuilder("..");

                for (int index = 0; index < state.Length - 4; index++)
                {
                    string pattern = state.ToString(index, 5);

                    newState.Append(rules.Single(r => r.Pattern == pattern).Result);
                }
                state.Clear();
                state.Append(newState.ToString());
                if (generation % 100000000 == 0)
                {
                    Console.WriteLine($"Generation {generation} complete. Current state: {state.ToString()}");
                }
            }
            double result = 0;
            for (int index = 0; index < state.Length; index++)
            {
                result += (state[index] == '#') ? index + minPosition : 0;
            }
            Console.WriteLine($"Problem 12-2: {result}");
        }

        public static void Problem2a()
        {
            string state = ".....####.#.....###.#.....####.#.....###.#.....###.#.....###.#....####.#.....###.#....####.#....####.#.....###.#.....####.#...####.#....###.#.....####.#....###.#.....###.#.....####.#....####.#..";
            int minPosition = -95;
            
            double result = 0;
            for (int index = 0; index < state.Length; index++)
            {
                if (state[index] == '#')
                {
                    result += index + minPosition + 50000000000;
                }
            }
            Console.WriteLine($"Problem 12-2: {result}");
        }
    }

    public class Rule
    {
        public string Pattern { get; set; }
        public string Result { get; set; }
    }
}
