using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public static class Day1
    {
        public static void Problem1()
        {
            var input = Utilities.ReadIntArray(1);

            Console.WriteLine($"Day1 - 1: {input.Sum()}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadIntArray(1);

            var sumList = new List<int>();

            bool matchFound = false;
            int sum = 0;
            int index = 0;
            int counter = 1;
            int newSum = 0;

            while (!matchFound)
            {
                newSum = sum + input[index];
                if (sumList.Contains(newSum))
                {
                    matchFound = true;
                }
                else
                {
                    counter++;
                    index = (index < input.Length - 1) ? index + 1 : 0;
                    sumList.Add(newSum);
                    sum = newSum;
                }
            }
            Console.WriteLine($"Day1 - 2: It took {counter} searches to find the first match... {newSum}");
        }
    }
}
