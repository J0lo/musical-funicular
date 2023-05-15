using System.Text.RegularExpressions;
using BattleShips.Shared;

namespace BattleShips.State;
public class Coordinates
{
    public int XAxis { get; set; }
    public int YAxis { get; set; }

    public Coordinates() {}

    public Coordinates (string? coordinates) {
        if(!string.IsNullOrWhiteSpace(coordinates)) {
            var match = Regex.Match(coordinates, Consts.CoordinatesRegex);

            XAxis = (int)(match.Groups[1].Value.First()-Consts.YDimensionMinimumValue);
            YAxis = int.Parse(match.Groups[2].Value) - 1;
        }
    }
}
