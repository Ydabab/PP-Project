using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;

namespace SimWeb.Pages
{
    public class SimulationModel : PageModel
    {
        private static Simulation _simulation;
        private static int _currentTurn = 0;
        private static List<char> moves = new List<char>();
        List<IMappable> creatures = new List<IMappable> {
                                    new Orc("Gorbag"),
                                    new Elf("Elandor"),
                                    new Animals("Rabbits", 23),
                                    new Birds("Eagles", 3),
                                    new Birds("Ostriches", 15, false) };
        public Dictionary<Point, List<char>>? Symbols { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int CurrentTurn => _currentTurn;
        public int CurrentRound => _currentTurn / creatures.Count;

        public List<List<CreatureAtPoint>> MapGrid { get; set; } = new();

        public void OnGet()
        {
            BigBounceMap map = new(8, 6);
            List<Point> points = new List<Point>
                            {
                                new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4)
                            };
            _simulation = new Simulation(map, creatures, points, moves);
            SizeX = map.SizeX;
            SizeY = map.SizeY;
            UpdateSymbols();
            GenerateMapGrid();
        }

        public IActionResult OnPostRight()
        {
            moves.Add('R');
            _currentTurn++;
            _simulation.Turn();
            UpdateSymbols();
            GenerateMapGrid();
            return Page();
        }

        public IActionResult OnPostLeft()
        {
            moves.Add('L');
            _currentTurn++;
            _simulation.Turn();
            UpdateSymbols();
            GenerateMapGrid();
            return Page();
        }
        public IActionResult OnPostUp()
        {
            moves.Add('D');
            _currentTurn++;
            _simulation.Turn();
            UpdateSymbols();
            GenerateMapGrid();
            return Page();
        }
        public IActionResult OnPostDown()
        {
            moves.Add('U');
            _currentTurn++;
            _simulation.Turn();
            UpdateSymbols();
            GenerateMapGrid();
            return Page();
        }

        private void UpdateSymbols()
        {
            Symbols = new Dictionary<Point, List<char>>();
            foreach (var mappable in _simulation.IMappables)
            {
                if (!Symbols.ContainsKey(mappable.Position))
                {
                    Symbols[mappable.Position] = new List<char>();
                }
                Symbols[mappable.Position].Add(mappable.Symbol);
            }

            SizeX = _simulation.Map.SizeX;
            SizeY = _simulation.Map.SizeY;
        }

        private void GenerateMapGrid()
        {
            MapGrid.Clear();

            for (int row = 5; row >= 0; row--)
            {
                var rowGrid = new List<CreatureAtPoint>();

                for (int col = 0; col < 8; col++)
                {
                    var point = new Point(col, row);
                    var creaturesAtPoint = Symbols?.FirstOrDefault(s => s.Key.Equals(point)).Value;

                    rowGrid.Add(new CreatureAtPoint
                    {
                        Point = point,
                        Creatures = creaturesAtPoint
                    });
                }

                MapGrid.Add(rowGrid);
            }
        }

        public string GetImageSource(List<char> creatures)
        {
            bool isNight = CurrentRound % 4 < 2;
            
            if (creatures.Contains('E'))
                return "<img src='/images/Elf.png' alt='Elf' />";
            if (creatures.Contains('O'))
                return "<img src='/images/Orc.png' alt='Orc' />";

            if (isNight)
            {
                return "<img src='/images/Skeleton.png' alt='Skeleton' />";
            }
            else
            {
                if (creatures.Contains('A'))
                    return "<img src='/images/Rabbit.png' alt='Rabbit' />";
                if (creatures.Contains('B'))
                    return "<img src='/images/Eagle.png' alt='Eagle' />";
                if (creatures.Contains('b'))
                    return "<img src='/images/Ostrich.png' alt='Ostrich' />";
            }
            return "";
        }

        public class CreatureAtPoint
        {
            public Point Point { get; set; }
            public List<char>? Creatures { get; set; }
        }
    }
}