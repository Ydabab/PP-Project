using SimConsole;
using Simulator.Maps;

namespace Simulator;

public class MapVisualizer
{
    private readonly Map _map;

    public MapVisualizer(Map map)
    {
        _map = map;
    }

    public void Draw()
    {
        DrawMap(_map);
    }

    private void DrawMap(Map map)
    {
        Console.Write(Box.TopLeft);
        for (int x = 0; x < map.SizeX; x++)
        {
            Console.Write(Box.Horizontal);
            if (x < map.SizeX - 1) Console.Write(Box.TopMid);
        }
        Console.WriteLine(Box.TopRight);
        for (int y = map.SizeY - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < map.SizeX; x++)
            {
                var creatures = map.At(new Point(x, y));
                if (creatures.Count > 1)
                {
                    Console.Write('X');
                }
                else if (creatures.Count == 1)
                {
                    var creature = creatures[0];
                    Console.Write($"{creature.Symbol}");
                }
                else
                {
                    Console.Write(' ');
                }
                if (x < map.SizeX - 1)
                {
                    Console.Write(Box.Vertical);
                }
            }
            Console.WriteLine(Box.Vertical);
            if (y > 0)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < map.SizeX; x++)
                {
                    Console.Write(Box.Horizontal);
                    if (x < map.SizeX - 1) Console.Write(Box.Cross);
                }
                Console.WriteLine(Box.MidRight);
            }
        }
        Console.Write(Box.BottomLeft);
        for (int x = 0; x < map.SizeX; x++)
        {
            Console.Write(Box.Horizontal);
            if (x < map.SizeX - 1) Console.Write(Box.BottomMid);
        }
        Console.WriteLine(Box.BottomRight);
    }
}