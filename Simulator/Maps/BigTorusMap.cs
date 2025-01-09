namespace Simulator.Maps;

public class BigTorusMap : BigMap
{
    public BigTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
    }

    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        return d switch
        {
            Direction.Up => Exist(nextPoint) ? nextPoint : new Point(p.X, 0),
            Direction.Right => Exist(nextPoint) ? nextPoint : new Point(0, p.Y),
            Direction.Down => Exist(nextPoint) ? nextPoint : new Point(p.X, SizeY - 1),
            Direction.Left => Exist(nextPoint) ? nextPoint : new Point(SizeX - 1, p.Y),
            _ => default,
        };
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextPoint = p.NextDiagonal(d);
        return d switch
        {
            Direction.Up => Exist(nextPoint) ? nextPoint : new Point((p.X + 1) % SizeX, (p.Y + 1) % SizeY),
            Direction.Right => Exist(nextPoint) ? nextPoint : new Point((p.X + 1) % SizeX, (p.Y - 1 + SizeY) % SizeY),
            Direction.Down => Exist(nextPoint) ? nextPoint : new Point((p.X - 1 + SizeX) % SizeX, (p.Y - 1 + SizeY) % SizeY),
            Direction.Left => Exist(nextPoint) ? nextPoint : new Point((p.X - 1 + SizeX) % SizeX, (p.Y + 1) % SizeY),
            _ => default,
        };
    }
}
