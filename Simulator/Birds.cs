using Simulator.Maps;

namespace Simulator;

public class Birds : Animals
{
    public bool CanFly { get; set; } = true;
    public override char Symbol => CanFly ? 'B' : 'b';
    public override string Info
    {
        get
        {
            string flyAbility = CanFly ? "fly+" : "fly-";
            return $"{Description} ({flyAbility}) <{Size}>";
        }
    }
    public Birds() { }
    public Birds(string description = "Unknown Bird", int size = 3, bool canFly = true) : base(description, size)
    {
        CanFly = canFly;
    }

    public override void Go(Direction direction)
    {
        if (Map == null)
        {
            Console.WriteLine("Map is not set. The bird cannot move.");
            return;
        }
        Point nextPosition = CanFly
            ? Map.Next(Map.Next(Position, direction), direction)
            : Map.NextDiagonal(Position, direction); 


        Map.Move(this, Position, nextPosition);
        Position = nextPosition;
    }

}