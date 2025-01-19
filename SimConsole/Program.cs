using Simulator;
using Simulator.Maps;

namespace SimConsole;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        BigTorusMap map = new(8, 6);
        List<IMappable> mappables = new() { new Orc("Gorbag"), new Elf("Elandor"), new Animals("Rabbits", 8), new Birds("Eagle", 14, true), new Birds("Ostrich", 2, false) };
        List<Point> points = new() { new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4) };
        List<char> moves = new List<char> {'U'};

        Simulation simulation = new(map, mappables, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);

        //while (!simulation.Finished)
        //{
        //    mapVisualizer.Draw();
        //    Console.WriteLine("\nPress any key to make a move...");
        //    Console.ReadKey(true);
        //    simulation.Turn();
        //    Console.Clear();
        //}

        //mapVisualizer.Draw();
        //Console.WriteLine("\nSimulation finished!");

        SimulationHistory history = new(simulation);
        LogVisualizer logVisualizer = new(history);
        logVisualizer.Draw(4);
        Console.ReadKey(true);
        logVisualizer.Draw(9);
    }
}
