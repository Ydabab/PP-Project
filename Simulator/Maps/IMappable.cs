namespace Simulator.Maps;

public interface IMappable
{
    public char Symbol { get; }
    public Point Position { get; }
    void Go(Direction direction);
    void InitMapAndPosition(Map map, Point position);
    public string ToString();
}
