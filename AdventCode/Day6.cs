using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public static class Day6
    {

        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(6);
            var coordinates = new List<Coordinate>();

            int id = 1;
            foreach (var line in input)
            {

                coordinates.Add(new Coordinate()
                {
                    ID = id++,
                    X = Convert.ToInt32(line.Split(',')[0]),
                    Y = Convert.ToInt32(line.Split(' ')[1])
                });
            }

            int[,] grid = new int[400,400];

            var xMin = coordinates.Select(c => c.X).Min() - 1;
            var xMax = coordinates.Select(c => c.X).Max() + 1;
            var yMin = coordinates.Select(c => c.Y).Min() - 1;
            var yMax = coordinates.Select(c => c.Y).Max() + 1;
            var exclusions = new List<int>();

            for (int x = xMin; x <= xMax; x++)
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    int closestDistance = 1000;
                    foreach (var coordinate in coordinates)
                    {
                        var distance = Math.Abs(coordinate.Y - y) + Math.Abs(coordinate.X - x);
                        if (distance == closestDistance)
                        {
                            grid[x, y] = 0;
                        }
                        else if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            grid[x, y] = coordinate.ID;
                        }
                    }
                    if (grid[x,y] != 0 && (x == xMin || x == xMax || y == yMin || y == yMax) && !exclusions.Exists(e => e == grid[x, y]))
                    {
                        exclusions.Add(grid[x, y]);
                    }
                    if (grid[x,y] != 0)
                    {
                        coordinates.Single(c => c.ID == grid[x, y]).Count++;
                    }
                }
            }
            int maxSize = coordinates.Where(c => !exclusions.Exists(e => e == c.ID)).Select(c => c.Count).Max();
            Console.WriteLine($"Largest area is {maxSize}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(6);
            var coordinates = new List<Coordinate>();

            int id = 1;
            foreach (var line in input)
            {

                coordinates.Add(new Coordinate()
                {
                    ID = id++,
                    X = Convert.ToInt32(line.Split(',')[0]),
                    Y = Convert.ToInt32(line.Split(' ')[1])
                });
            }

            int[,] grid = new int[400, 400];

            var xMin = coordinates.Select(c => c.X).Min() - 1;
            var xMax = coordinates.Select(c => c.X).Max() + 1;
            var yMin = coordinates.Select(c => c.Y).Min() - 1;
            var yMax = coordinates.Select(c => c.Y).Max() + 1;
            var exclusions = new List<int>();

            int region = 0;
            for (int x = xMin; x <= xMax; x++)
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    grid[x, y] = coordinates.Select(c => Math.Abs(c.Y - y) + Math.Abs(c.X - x)).Sum();
                    region += (grid[x, y] < 10000) ? 1 : 0;
                }
            }
            int maxSize = coordinates.Where(c => !exclusions.Exists(e => e == c.ID)).Select(c => c.Count).Max();
            Console.WriteLine($"Region size is {region}");
        }
    }

    public class Coordinate
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Count { get; set; } = 0;
    }
}
