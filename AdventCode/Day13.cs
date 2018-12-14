using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day13
    {
        public static void Problems()
        {
            var input = Utilities.ReadStringArray(13);
            //var input = Utilities.ReadStringArray("Test13.txt");
            Track[,] tracks = new Track[input[0].Length, input.Length];
            List<Cart> carts = new List<Cart>();
            int cartId = 0;
            for (int y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case '+':
                            tracks[x, y] = new Track()
                            {
                                TrackType = TrackType.Intersection
                            };
                            break;
                        case '|':
                            tracks[x, y] = new Track()
                            {
                                TrackType = TrackType.NS
                            };
                            break;
                        case '-':
                            tracks[x, y] = new Track()
                            {
                                TrackType = TrackType.WE
                            };
                            break;
                        case '/':
                            TrackType type;
                            if (x == 0)
                            {
                                type = TrackType.SE;
                            }
                            else if (tracks[x-1,y] != null && (tracks[x - 1, y].TrackType == TrackType.Intersection || tracks[x - 1, y].TrackType == TrackType.WE))
                            {
                                type = TrackType.NW;
                            }
                            else
                            {
                                type = TrackType.SE;
                            }
                            tracks[x, y] = new Track()
                            {
                                TrackType = type
                            };
                            break;
                        case '\\':
                            if (x == 0)
                            {
                                type = TrackType.NE;
                            }
                            else if (tracks[x - 1, y] != null && (tracks[x - 1, y].TrackType == TrackType.Intersection || tracks[x - 1, y].TrackType == TrackType.WE))
                            {
                                type = TrackType.SW;
                            }
                            else
                            {
                                type = TrackType.NE;
                            }
                            tracks[x, y] = new Track()
                            {
                                TrackType = type
                            };
                            break;
                        case '<':
                            carts.Add(new Cart()
                            {
                                ID = cartId++,
                                X = x,
                                Y = y,
                                Direction = 'W'
                            });
                            tracks[x, y] = new Track()
                            {
                                TrackType = TrackType.WE
                            };
                            break;
                        case '>':
                            carts.Add(new Cart()
                            {
                                ID = cartId++,
                                X = x,
                                Y = y,
                                Direction = 'E'
                            });
                            tracks[x, y] = new Track()
                            {
                                TrackType = TrackType.WE
                            };
                            break;
                        case 'v':
                            carts.Add(new Cart()
                            {
                                ID = cartId++,
                                X = x,
                                Y = y,
                                Direction = 'S'
                            });
                            tracks[x, y] = new Track()
                            {
                                TrackType = TrackType.NS
                            };
                            break;
                        case '^':
                            carts.Add(new Cart()
                            {
                                ID = cartId++,
                                X = x,
                                Y = y,
                                Direction = 'N'
                            });
                            tracks[x, y] = new Track()
                            {
                                TrackType = TrackType.NS
                            };
                            break;
                    }
                }
            }

            int ticks = 0;
            while (true)
            {
                carts = carts.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();
                foreach (var cart in carts)
                {
                    if (!cart.Removed)
                    {
                        var track = tracks[cart.X, cart.Y];
                        switch (track.TrackType)
                        {
                            case TrackType.Intersection:
                                string lsr = "";
                                switch (cart.Direction)
                                {
                                    case 'N':
                                        lsr = "WNE";
                                        break;
                                    case 'E':
                                        lsr = "NES";
                                        break;
                                    case 'S':
                                        lsr = "ESW";
                                        break;
                                    case 'W':
                                        lsr = "SWN";
                                        break;
                                }
                                cart.Direction = lsr[cart.Turns % 3];
                                cart.Turns++;
                                break;
                            case TrackType.NE:
                                cart.Direction = (cart.Direction == 'W') ? 'N' : 'E';
                                break;
                            case TrackType.NW:
                                cart.Direction = (cart.Direction == 'E') ? 'N' : 'W';
                                break;
                            case TrackType.SE:
                                cart.Direction = (cart.Direction == 'W') ? 'S' : 'E';
                                break;
                            case TrackType.SW:
                                cart.Direction = (cart.Direction == 'E') ? 'S' : 'W';
                                break;
                        }
                        switch (cart.Direction)
                        {
                            case 'N':
                                cart.Y--;
                                break;
                            case 'E':
                                cart.X++;
                                break;
                            case 'S':
                                cart.Y++;
                                break;
                            case 'W':
                                cart.X--;
                                break;
                        }
                        if (carts.Where(c => c.X == cart.X && c.Y == cart.Y && !c.Removed).Count() > 1)
                        {
                            var otherCart = carts.Single(c => (c.X == cart.X) && (c.Y == cart.Y) && (c.ID != cart.ID) && (!c.Removed));
                            Console.WriteLine($"Cart {cart.ID} collided with {otherCart.ID} at <{cart.X}, {cart.Y}> on tick {ticks}");
                            //Console.WriteLine($"Collision at <{cart.X}, {cart.Y}> on tick {ticks}");
                            carts.Where(c => c.X == cart.X && c.Y == cart.Y).ToList().ForEach(c => c.Removed = true);
                        }
                    }
                }
                var remainingCarts = carts.Where(c => !c.Removed).ToList();
                if (remainingCarts.Count() == 1)
                {
                    Console.WriteLine($"Cart {remainingCarts[0].ID} is the last cart standing. It's at <{remainingCarts[0].X}, {remainingCarts[0].Y}> on tick {ticks}");
                    return;
                }
                ticks++;
            }
        }

        
    }

    public class Track
    {
        public TrackType TrackType { get; set; }
    }

    public class Cart
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Turns { get; set; } = 0;
        public char Direction { get; set; }
        public bool Removed { get; set; } = false;
    }

    public enum TrackType
    {
        Intersection,
        NS,
        WE,
        NW,
        NE,
        SW,
        SE
    }
}
