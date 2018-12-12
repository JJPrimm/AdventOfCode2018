using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day11
    {
        static int input = 5235;

        public static void Problem1()
        {
            var grid = new List<FuelCell>();

            for (int x = 1; x <= 300; x++)
            {
                for (int y = 1; y <= 300; y++)
                {
                    var fuelCell = new FuelCell()
                    {
                        x = x,
                        y = y,
                        GridSerialNumber = input
                    };
                    fuelCell.SetPowerLevel();
                    grid.Add(fuelCell);
                    grid.Where(g => g.x <= x && g.x >= x - 2 && g.y <= y && g.y >= y - 2)
                        .ToList()
                        .ForEach(fc => fc.Square += fuelCell.PowerLevel);
                }
            }
            var maxSquareValue = grid.Max(g => g.Square);
            var maxSquare = grid.FirstOrDefault(g => g.Square == maxSquareValue);

            Console.WriteLine($"<{maxSquare.x},{maxSquare.y}>");
        }

        public static void Problem2()
        {
            var grid = new List<FuelCell>();

            for (int x = 1; x <= 300; x++)
            {
                for (int y = 1; y <= 300; y++)
                {
                    var fuelCell = new FuelCell()
                    {
                        x = x,
                        y = y,
                        GridSerialNumber = input
                    };
                    fuelCell.SetPowerLevel();
                    grid.Add(fuelCell);
                    Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine("Grid Complete");

            Square maxSquare = new Square();
            for (int x = 1; x <= 298; x++)
            {
                for (int y = 1; y <= 298; y++)
                {
                    var maxSize = (x > y) ? 301 - x : 301 - y;

                    for (int size = 3; size <= maxSize; size++)
                    {
                        var powerLevel = grid.Where(g => (g.x >= x && g.x < x + size) && (g.y >= y && g.y < y + size)).Select(fc => fc.PowerLevel).Sum();
                        if (powerLevel > maxSquare.PowerLevel)
                        {
                            maxSquare.x = x;
                            maxSquare.y = y;
                            maxSquare.Size = size;
                            maxSquare.PowerLevel = powerLevel;
                        }
                    }
                    Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine($"<{maxSquare.x},{maxSquare.y},{maxSquare.Size}");
        }
    }

    public class Square
    {
        public int x { get; set; }
        public int y { get; set; }
        public int Size { get; set; }
        public int PowerLevel { get; set; }
    }

    public class FuelCell
    {
        public int x { get; set; }
        public int y { get; set; }
        public int GridSerialNumber { get; set; }
        public int RackID { get { return x + 10; } }
        public int Square { get; set; }
        public int PowerLevel { get; set; }
        
        public void SetPowerLevel()
        {           
            PowerLevel = RackID * y;
            PowerLevel += GridSerialNumber;
            PowerLevel *= RackID;
            PowerLevel = ((PowerLevel % 1000) - (PowerLevel % 100)) / 100;
            PowerLevel -= 5;
        }
    }
}
