namespace Simulator.Maps;

public class BigBounceMap : BigMap
{   
    public BigBounceMap(int sizeX, int sizeY) : base(sizeX, sizeY) { }
    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        return d switch
        {
            Direction.Up => Exist(nextPoint) ? nextPoint : p.Next(Direction.Down),
            Direction.Right => Exist(nextPoint) ? nextPoint : p.Next(Direction.Left),
            Direction.Down => Exist(nextPoint) ? nextPoint : p.Next(Direction.Up),
            Direction.Left => Exist(nextPoint) ? nextPoint : p.Next(Direction.Right),
            _ => default,
        };
    }
    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextPoint = p.NextDiagonal(d);
        return d switch
        {
            Direction.Up => Exist(nextPoint) ? nextPoint : p.NextDiagonal(Direction.Down),
            Direction.Right => Exist(nextPoint) ? nextPoint : p.NextDiagonal(Direction.Left),
            Direction.Down => Exist(nextPoint) ? nextPoint : p.NextDiagonal(Direction.Up),
            Direction.Left => Exist(nextPoint) ? nextPoint : p.NextDiagonal(Direction.Right),
            _ => default,
        };
    }
}
