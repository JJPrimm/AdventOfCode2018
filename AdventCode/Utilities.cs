using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    public static class Utilities
    {
        public static int[] ReadIntArray(int day)
        {
            return File.ReadAllLines($@"..\..\input\Input{day}.txt")
                .Select(s => Convert.ToInt32(s))
                .ToArray();
        }

        public static string[] ReadStringArray(int day)
        {
            return File.ReadAllLines($@"..\..\input\Input{day}.txt")
                .ToArray();
        }

        public static int[] ReadSpaceSeparatedIntArray(int day)
        {
            return File.ReadAllText($@"..\..\input\Input{day}.txt").Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
            //return File.ReadAllText($@"..\..\input\Test8.txt").Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
        }

        public static Dictionary<char, int> CharToInt()
        {
            return new Dictionary<char, int>
            {
                { 'A', 1 },
                { 'B', 2 },
                { 'C', 3 },
                { 'D', 4 },
                { 'E', 5 },
                { 'F', 6 },
                { 'G', 7 },
                { 'H', 8 },
                { 'I', 9 },
                { 'J', 10 },
                { 'K', 11 },
                { 'L', 12 },
                { 'M', 13 },
                { 'N', 14 },
                { 'O', 15 },
                { 'P', 16 },
                { 'Q', 17 },
                { 'R', 18 },
                { 'S', 19 },
                { 'T', 20 },
                { 'U', 21 },
                { 'V', 22 },
                { 'W', 23 },
                { 'X', 24 },
                { 'Y', 25 },
                { 'Z', 26 }
            };
        }
    }
}
