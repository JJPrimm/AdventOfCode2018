using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day10
    {
        public static void Problems()
        {
            var input = Utilities.ReadStringArray(10);

            var points = input.Select(i => new Point()
            {
                x = Convert.ToInt32(i.Substring(10,6).Trim()),
                y = Convert.ToInt32(i.Substring(18, 6).Trim()),
                xVelocity = Convert.ToInt32(i.Substring(36, 2).Trim()),
                yVelocity = Convert.ToInt32(i.Substring(40, 2).Trim()),
            }).ToList();

            var stop = false;
            var ticks = 0;
            while (!stop)
            {
                points.ForEach(p => p.Tick());
                ticks++;
                if (ticks % 1000 == 0)
                {
                    Console.WriteLine($"{ticks} ticks have passed.");
                }

                foreach (var point in points)
                {
                    point.ProxScore = points
                        .Where(p => (p.y == point.y && ((p.x == point.x + 1) || (p.x == point.x - 1)))
                        || (p.x == point.x && ((p.y == point.y + 1) || (p.y == point.y - 1))))
                        .Count();
                }
                if (points.Sum(p => p.ProxScore) > 100)
                {
                    ShowGrid(points);
                    Console.WriteLine($"{ticks} seconds have passed.");
                    Console.Write("Continue? (y/n) ");
                    stop = Console.ReadKey().KeyChar == 'n';
                    Console.WriteLine();
                }
            }
        }

        public static void ShowGrid(List<Point> points)
        {
            var xMin = points.Min(p => p.x);
            var xRange = points.Max(p => p.x) - xMin;
            var yMin = points.Min(p => p.y);
            var yRange = points.Max(p => p.y) - yMin;

            var xTotal = 0;
            while (xTotal <= xRange)
            {
                for (int y = 0; y <= yRange; y++)
                {
                    StringBuilder line = new StringBuilder();
                    for (int x = xTotal; (x < xTotal + 200); x++)
                    {
                        if (points.Exists(p => (p.x == x + xTotal + xMin) && (p.y == y + yMin)))
                        {
                            line.Append('#');
                        }
                        else
                        {
                            line.Append('.');
                        }
                    }
                    Console.WriteLine(line.ToString());
                }
                xTotal += 200;
                Console.WriteLine("******************************************************************************************************************************");
            }
        }
    } 

    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int xVelocity { get; set; }
        public int yVelocity { get; set; }
        public int ProxScore { get; set; }

        public void Tick()
        {
            x += xVelocity;
            y += yVelocity;
        }
    }
}
