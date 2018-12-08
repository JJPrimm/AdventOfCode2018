using System;
using System.Linq;

namespace AdventCode
{
    public static class Day2
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(2);
            int pairs = 0;
            int trips = 0;

            foreach(var str in input)
            {
                bool hasPair = false;
                bool hasTrips = false;
                var ary = str.ToCharArray();
                foreach(var ch in ary)
                {
                    if (ary.Where(a => a == ch).Count() == 3)
                    {
                        hasTrips = true;
                    }
                    else if (ary.Where(a => a == ch).Count() == 2)
                    {
                        hasPair = true;
                    }
                    if (hasTrips && hasPair)
                    {
                        break;
                    }
                }
                if (hasPair)
                {
                    pairs++;
                }
                if (hasTrips)
                {
                    trips++;
                }
            }
            Console.WriteLine($"Day1 - 1: {pairs * trips}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(2);
            int stringLength = input.Select(s => s.Length).Min();

            for (int index = 0; index < stringLength; index++)
            {
                var modifiedArray = input.Select(s => s.Remove(index, 1));
                foreach(var modifiedString in modifiedArray)
                {
                    if (modifiedArray.Where(s => s == modifiedString).Count() > 1)
                    {
                        Console.WriteLine($"Matching string is {modifiedString}");
                        return;
                    }
                }
            }
            Console.WriteLine("Problem 2-2 has completed without finding a match");
        }
    }
}
