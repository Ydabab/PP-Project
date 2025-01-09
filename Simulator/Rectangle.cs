namespace Simulator;

public class Rectangle
{
    public readonly int X1, Y1, X2, Y2;

    public Rectangle(int x1, int y1, int x2, int y2)
    {
        if (x1 > x2)
        {
            (x1, x2) = (x2, x1);
        }
        if (y1 > y2)
        {
            (y1, y2) = (y2, y1);
        }
        if (x1 == x2 || y1 == y2)
        {
            throw new ArgumentException("Punkty są współliniowe - chudy prostąkąt");
        }
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }
    public Rectangle(Point p1, Point p2) : this(p1.X, p1.Y, p2.X, p2.Y) 
    {
        if (p1.X == p2.X || p1.Y == p2.Y)
        {
            throw new ArgumentException("Punkty są współliniowe - chudy prostokąt");
        }
    }
    public bool Contains(Point point)
    {
        return point.X >= X1 && point.Y >= Y1 && point.X <= X2 && point.Y <= Y2;
    }
    public override string ToString()
    {
        return $"({X1}, {Y1}); ({X2}, {Y2})";
    }
}