using Simulator.Maps;

namespace Simulator;
public abstract class Creature : IMappable
{
    public Map? Map { get; private set; }
    public Point Position { get; private set; }
    private int level = 1;
    private string name = "Unknown";

    public string Name
    {
        get => name;
        init
        {
            name = Validator.Shortener(value, 3, 25, '#');
            name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }
    }
    public int Level
    {
        get => level;
        init => level = Validator.Limiter(value, 1, 10);
    }
    public abstract int Power { get; }

    public void InitMapAndPosition(Map map, Point position) 
    {
        Map = map;
        Position = position;
    }
    public Creature() { }
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level >= 1 ? level : 1;
    }

    public string Greeting() => $"Hi, I'm {Name}, my level is {Level}.";
    public int Upgrade() => level < 10 ? ++level : level;
    public void Go(Direction direction)
    {
        if (Map == null)
            return; // Jeśli stwór nie ma mapy, nic nie robimy.

        Point nextPosition = Map.Next(Position, direction);
        Map.Move((IMappable)this, Position, nextPosition); // Przemieszczanie stworów
        Position = nextPosition;
    }


    public abstract string Info { get; }
    public abstract char Symbol { get; }

    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";


}