﻿using Simulator.Maps;
namespace Simulator;
public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// IMappables moving on the map.
    /// </summary>
    public List<IMappable> IMappables { get; }

    /// <summary>
    /// Starting positions of mappables.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of mappables moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first mappable, second for second and so on.
    /// When all mappables make moves, 
    /// next move is again for first mappable and so on.
    /// </summary>
    public List<char> Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    private List<Direction> FilteredMoves { get; set; } = new List<Direction>();
    private int _currentMoveIndex = 0;

    /// <summary>
    /// IMappable which will be moving current turn.
    /// </summary>
    public IMappable CurrentMappable => IMappables[_currentMoveIndex % IMappables.Count];

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName => FilteredMoves.Count > _currentMoveIndex ? FilteredMoves[_currentMoveIndex].ToString().ToLower() : string.Empty;

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if mappables' list is empty,
    /// if number of mappables differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<IMappable> mappables, List<Point> positions, List<char> moves)
    {
        if (mappables == null || mappables.Count == 0)
        {
            throw new ArgumentException("List of mappables cannot be empty.");
        }

        if (mappables.Count != positions.Count)
        {
            throw new ArgumentException("Number of mappables must match the number of starting positions.");
        }

        Map = map ?? throw new ArgumentNullException(nameof(map));
        IMappables = mappables;
        Positions = positions;
        Moves = moves ?? throw new ArgumentNullException(nameof(moves));

        FilterMoves();

        for (int i = 0; i < mappables.Count; i++)
        {
            var mappable = mappables[i];
            var position = positions[i];

            if (!map.Exist(position))
            {
                throw new ArgumentException($"Position {position} is outside the bounds of the map.");
            }
            mappable.InitMapAndPosition(map, position);
            map.Add(mappable, position);
        }
    }

    public Simulation()
    {
    }

    /// <summary>
    /// Makes one move of current mappable in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        FilterMoves();
        Direction direction = FilteredMoves[_currentMoveIndex];
        CurrentMappable.Go(direction);
        _currentMoveIndex++;
    }

    private void FilterMoves()
    {
        FilteredMoves = Moves
            .Select(c => DirectionParser.Parse(c.ToString().ToLower()))
            .Where(d => d != null && d.Count > 0)
            .Select(d => d[0])
            .ToList();
    }
}
