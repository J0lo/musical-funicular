using BattleShips.State;
using ConsoleTables;

namespace BattleShips.Display;

public class ShipStatus
{
    public void Display(List<Ship> ships)
    {
        Console.WriteLine("Ships status:");
        
        var table = new ConsoleTable("Ship#", "Ship Length", "Remaining HP", "Status");

        for (var i = 0; i < ships.Count; i++) {
            var ship = ships[i];
            table.AddRow(i+1, ship?.Coordinates?.Count, ship?.HitPoints, ship?.State.GetDescription());
        }

        table.Write();
    }
}