using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day14
    {
        public static void Problem1()
        {
            int input = 702831;

            List<int> scoreboard = new List<int>();
            var elf1 = 0;
            var elf2 = 1;
            scoreboard.Add(3);
            scoreboard.Add(7);
            while (scoreboard.Count < input + 10)
            {
                int score = scoreboard[elf1] + scoreboard[elf2];
                if (score >= 10)
                {
                    scoreboard.Add(1);
                    scoreboard.Add(score % 10);
                }
                else
                {
                    scoreboard.Add(score);
                }
                elf1 = (elf1 + 1 + scoreboard[elf1]) % scoreboard.Count;
                elf2 = (elf2 + 1 + scoreboard[elf2]) % scoreboard.Count;
            }
            for (int i = input; i < input + 10; i++)
            {
                Console.Write(scoreboard[i]);
            }
            Console.WriteLine();
        }

        public static void Problem2()
        {
            int input = 702831;

            List<int> scoreboard = new List<int>();
            var elf1 = 0;
            var elf2 = 1;
            scoreboard.Add(3);
            scoreboard.Add(7);
            string last6 = "37";
            while (true)
            {
                int score = scoreboard[elf1] + scoreboard[elf2];
                if (score >= 10)
                {
                    scoreboard.Add(1);
                    last6 = (last6.Length < 6) ? last6 + 1 : (last6 + 1).Substring(1, 6);
                    if (last6 == input.ToString()) break;
                    scoreboard.Add(score % 10);
                    last6 = (last6.Length < 6) ? last6 + score % 10 : (last6 + score % 10).Substring(1, 6);
                    if (last6 == input.ToString()) break;
                }
                else
                {
                    scoreboard.Add(score);
                    last6 = (last6.Length < 6) ? last6 + score : (last6 + score).Substring(1, 6);
                    if (last6 == input.ToString()) break;
                }
                elf1 = (elf1 + 1 + scoreboard[elf1]) % scoreboard.Count;
                elf2 = (elf2 + 1 + scoreboard[elf2]) % scoreboard.Count;
            }
            Console.WriteLine(scoreboard.Count - 6);
        }
    }
}
