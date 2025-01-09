namespace Simulator.Maps;

public class SmallSquareMap : SmallMap
{
    public SmallSquareMap(int size) : base(size, size)
    {
    }

    public override Point Next(Point p, Direction d)
    {
        Point nextPoint = p.Next(d);
        return Exist(nextPoint) ? nextPoint : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point nextPoint = p.NextDiagonal(d);
        return Exist(nextPoint) ? nextPoint : p;
    }
}
