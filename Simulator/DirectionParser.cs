namespace Simulator;
public static class DirectionParser
{
    public static List<Direction> Parse(string input)
    {
        var directions = new List<Direction>();

        foreach (var ch in input.ToUpper())
        {
            if (ch == 'U')
            {
                directions.Add(Direction.Up);
            }
            else if (ch == 'R')
            {
                directions.Add(Direction.Right);
            }
            else if (ch == 'D')
            {
                directions.Add(Direction.Down);
            }
            else if (ch == 'L')
            {
                directions.Add(Direction.Left);
            }
        }

        return directions;
    }
}
