using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day3
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(3);
            var claims = input.Select(c => new Claim(c));
            int fabricWidth = claims.Select(c => c.LeftMargin + c.Width).Max();
            int fabricHeight = claims.Select(c => c.TopMargin + c.Height).Max();

            int[,] fabric = new int[fabricWidth,fabricHeight];
            int counter = 0;

            foreach(var claim in claims)
            {
                for (int x = claim.LeftMargin; x < claim.LeftMargin + claim.Width; x++)
                {
                    for (int y = claim.TopMargin; y < claim.TopMargin + claim.Height; y++)
                    {
                        fabric[x, y]++;
                        if (fabric[x,y] == 2)
                        {
                            counter++;
                        }
                    }
                }
            }
            Console.WriteLine($"Problem 3-1: {counter}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(3);
            var claims = input.Select(c => new Claim(c));
            int fabricWidth = claims.Select(c => c.LeftMargin + c.Width).Max();
            int fabricHeight = claims.Select(c => c.TopMargin + c.Height).Max();

            int[,] fabric = new int[fabricWidth, fabricHeight];

            foreach (var claim in claims)
            {
                for (int x = claim.LeftMargin; x < claim.LeftMargin + claim.Width; x++)
                {
                    for (int y = claim.TopMargin; y < claim.TopMargin + claim.Height; y++)
                    {
                        fabric[x, y]++;
                    }
                }
            }
            foreach (var claim in claims)
            {
                bool found = true;
                for (int x = claim.LeftMargin; x < claim.LeftMargin + claim.Width; x++)
                {
                    for (int y = claim.TopMargin; y < claim.TopMargin + claim.Height; y++)
                    {
                        if (fabric[x,y] > 1)
                        {
                            found = false;
                        }
                    }
                }
                if (found)
                {
                    Console.WriteLine($"Problem 3-2: Claim #{claim.Id}");
                    return;
                }
            }
            Console.WriteLine($"Problem 3-2 completed without finding an answer.");
        }
    }

    public class Claim
    {
        public int Id { get; set; }
        public int LeftMargin { get; set; }
        public int TopMargin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Claim(string claimString)
        {
            var claimArray = claimString.Split(' ');
            Id = Convert.ToInt32(claimArray[0].Remove(0, 1));
            LeftMargin = Convert.ToInt32(claimArray[2].Split(',')[0]);
            TopMargin = Convert.ToInt32(claimArray[2].Split(',')[1].Trim(':'));
            Width = Convert.ToInt32(claimArray[3].Split('x')[0]);
            Height = Convert.ToInt32(claimArray[3].Split('x')[1]);
        }
    }
}
